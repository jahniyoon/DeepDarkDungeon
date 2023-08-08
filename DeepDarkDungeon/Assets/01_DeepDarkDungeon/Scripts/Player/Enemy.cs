using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour                     //�߿�! navmesh�� static������Ʈ�� bake �����ϴ�!
{
    public enum Type { A, B, C };       //enum Ÿ�� ������ 
    public Type enemyType;              //�װ��� ������ ����

    public int maxHealth;
    public int curHealth;
    public Transform target;
    public BoxCollider meleeArea;         
    public bool isChase;
    public bool isAttack;

    public float jumpForce = 10f;

    Rigidbody rigid;
    BoxCollider boxCollider;
    //Material mat;
    Material mat2;

    NavMeshAgent nav;    //���� ���ӽ����̽� ai�߰��ؾ��Ѵ�

    Animator anim;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        //mat = GetComponentInChildren<MeshRenderer>().material;   // Material�� MeshRenderer�� �����;ߵȴ�
        
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        mat2 = GetComponentInChildren<SkinnedMeshRenderer>().material;

        Invoke("ChaseStart", 2);
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);

      
    }

    void Update()
    {
        if (nav.enabled)       //navi�� Ȱ��ȭ�Ǿ���������
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
          
        }

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
        targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));    //�ڽ��� ��ġ, ������, ��� ����, ���� ��� �Ÿ�      

        //rayHit ������ �����Ͱ� ������ ���� �ڷ�ƾ �����Ѵ�
        if (rayHits.Length > 0 && !isAttack)  //���� ���̰� �������̸� �������� �ʴ´�
        {
            StartCoroutine(Attack());
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
        if (other.tag.Equals("melee"))
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
       // mat.color = Color.red;
        mat2.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            //mat.color = Color.white;
            mat2.color = Color.white;
        }
        else
        {
            //mat.color = Color.gray;
            mat2.color = Color.gray;
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


            Destroy(gameObject, 4);
        }
    }
}

