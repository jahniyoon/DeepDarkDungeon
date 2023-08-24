using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour                     //�߿�! navmesh�� static������Ʈ�� bake �����ϴ�!
{
    public enum Type { A, B, C, D, E };       //enum Ÿ�� ������
    public Type enemyType;              //�װ��� ������ ����

    public int maxHealth;
    public int curHealth;
    public Transform target;
    public BoxCollider meleeArea;          //���� ���� ����
    public GameObject bullet;
    public GameObject magicBall;
    public bool isChase;
    public bool isAttack;                 //�Ϲ��� ���� ����

    public Rigidbody rigid;
    //public SphereCollider sphereCollider;
    public BoxCollider boxCollider;
    //Material mat;
    public MeshRenderer[] meshs;  //�ǰ� ����Ʈ�� ��� ���׸����

    public NavMeshAgent nav;    //���� ���ӽ����̽� ai�߰��ؾ��Ѵ�

    public Animator anim;


   

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();  // Material�� MeshRenderer�� �����;ߵȴ�
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        

        if (enemyType != Type.D)
            Invoke("ChaseStart", 2);
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Update()
    {
        if (nav.enabled && enemyType != Type.D )       //navi�� Ȱ��ȭ�Ǿ���������
        {


            nav.SetDestination(target.position);     //SetDestination ������ ��ǥ ��ġ ���� �Լ� 
            nav.isStopped = !isChase;     //isStopped�� ����Ͽ� �Ϻ��ϰ� ���ߵ���
        }
       

    }
    //// if(isChase)
    //    {
    //        nav.SetDestination(target.position);   �� ������ ��ǥ�� �Ҿ�����°Ŷ� �̵��� ������...
    //    }

    //�Ϲ�������


    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;      //�������� navAgent �̵��� �������� �ʵ��� ���� �߰�
            rigid.angularVelocity = Vector3.zero;    //����ȸ���ӵ� 0����
        }

    }

    //�Ϲ��� ����
    void Targeting()
    {
        if (enemyType != Type.D)
        {
            float targetRadius = 0;
            float targetRange = 0;                   //sphererCast�� ������, ���̸� ���� ����

            switch (enemyType)
            {
                case Type.A:
                    targetRadius = 1.5f;
                    targetRange = 3f;
                    break;
                case Type.B:                //������ ����
                    targetRadius = 1f;
                    targetRange = 12f;
                    break;
                case Type.C:
                    targetRadius = 0.5f;   //�β�
                    targetRange = 25f;     //����
                    break;
                case Type.E:
                    targetRadius = 0.5f;   //�β�
                    targetRange = 25f;     //����
                    break;

            }

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
            targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));    //�ڽ��� ��ġ, ������, ��� ����, ���� ��� �Ÿ�      

            //rayHit ������ �����Ͱ� ������ ���� �ڷ�ƾ �����Ѵ�
            if (rayHits.Length > 0 && !isAttack)  //���� ���̰� �������̸� �������� �ʴ´�
            {
                StartCoroutine(Attack());
            }
        }

    }

    IEnumerator Attack()
    {
        //���� ������ �� ����, �ִϸ��̼ǰ� �Բ� ���ݹ��� Ȱ��ȭ
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);




        switch (enemyType)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;  //���ݹ��� Ȱ��ȭ

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;  //���ݹ��� ��Ȱ��ȭ

                yield return new WaitForSeconds(1f);
                break;
            case Type.B:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse);          //AddForce ���� ����
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                yield return new WaitForSeconds(2f);  //���������� 2�ʰ� ����
                break;
            case Type.C:
                yield return new WaitForSeconds(0.5f);
                GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 20;

                yield return new WaitForSeconds(2f);
                break;
            case Type.E:
                yield return new WaitForSeconds(0.5f);
                GameObject instantMagicBall = Instantiate(magicBall, transform.position, transform.rotation);

                


                break;

        }



        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }


    void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();

    }

    private void OnTriggerEnter(Collider other)
    {
       
        
            //if(other.tag.Equals("Player"))
            //{
            //    sphereCollider.enabled = false;
            //    Transform target = other.GetComponent<Transform>();
                
            //    findPlayer = true;
            //    Debug.Log("�÷��̾� ��ġ ������");


            //}
        
        if (other.tag.Equals("Melee"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;   //�˹�

            StartCoroutine(OnDamage(reactVec, false));
        }
        else if (other.tag.Equals("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);

            StartCoroutine(OnDamage(reactVec, false));
        }
    }

    public void HitByGrenade(Vector3 explosionPos)
    {
        curHealth -= 100;                        //����ź ���� ��ġ
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnDamage(reactVec, true));

    }

    IEnumerator OnDamage(Vector3 reactVec, bool isGrenade)
    {
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }
        }
        else
        {
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.gray;
            }
            gameObject.layer = 14;
            isChase = false;

            nav.enabled = false;      //��� ���׼� �����ϱ� ���ؼ�

            anim.SetTrigger("doDie");

            if (isGrenade)
            {
                reactVec = reactVec.normalized;
                reactVec += Vector3.up * 3;

                rigid.freezeRotation = false;     //freeze rotation x,yüũ �س��� false��
                rigid.AddForce(reactVec * 5, ForceMode.Impulse);    //�ݴ�������� ���� ��������
                rigid.AddTorque(reactVec * 15, ForceMode.Impulse);

            }
            else
            {
                reactVec = reactVec.normalized;
                reactVec += Vector3.up;                             //���� �ݴ�������� ����
                rigid.AddForce(reactVec * 5, ForceMode.Impulse);    //�ݴ�������� ���� ��������
            }

            if (enemyType != Type.D)
                Destroy(gameObject, 4);
        }
    }
}
