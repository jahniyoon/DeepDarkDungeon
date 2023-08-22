using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{

    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    public enum Type { A, B, C }
    public Type monsterType;

    //������ ���� ����
    public State state = State.IDLE;
    //���� �����Ÿ�
    public float traceDist = 10.0f;
    //���� �����Ÿ�
    public float attackDist = 1.0f;
    //������ ��� ����
    public bool isDie = false;

    //������Ʈ ĳ�ø� ó���� ����
    public Transform monsterTr;
    public Transform playerTr;
    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public NavMeshAgent agent;
    public Animator anim;

    public MeshRenderer[] meshs;
    public Transform playerTarget;
    public GameObject bullet;


    // Animator �Ķ������ �ؽð� ����
    public readonly int hashTrace = Animator.StringToHash("IsTrace");
    public readonly int hashAttack = Animator.StringToHash("IsAttack");
    public readonly int hashHit = Animator.StringToHash("Hit");
    public readonly int hashDie = Animator.StringToHash("Die");

    public int maxHp = 100;
    public int hp = 100;
    public string name;

    public bool isDamage;

    public int damage;



    // Start is called before the first frame update
    void Start()
    {
        //������ Transform �Ҵ�
        monsterTr = GetComponent<Transform>();

        //���� ����� Player�� Transform �Ҵ�
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        rigid = GetComponent<Rigidbody>();

        boxCollider = GetComponent<BoxCollider>();

        //NavMeshAgent ������Ʈ �Ҵ�
        agent = GetComponent<NavMeshAgent>();

        //Animator ������Ʈ �Ҵ�
        anim = GetComponent<Animator>();

        meshs = GetComponentsInChildren<MeshRenderer>();

<<<<<<< HEAD
        transform.LookAt(playerTr.position);
=======
        GameManager.instance.SetBossMaxHealth(maxHp, name); // ���� �ƽ� ü�� ����
        GameManager.instance.SetBossHealth(hp);              // ���� ���� ü�� ����
>>>>>>> origin/feature/Jihwan

        //������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(CheckMonsterState());
        //���¿� ���� ������ �ൿ�� �����ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(MonsterAction());
    }


    //������ �������� ������ �ൿ ���¸� üũ
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            //0.3�� ���� ����(���)�ϴ� ���� ������� �޽��� ������ �纸
            yield return new WaitForSeconds(0.3f);

            //������ ���°� Die�� �� �ڷ�ƾ�� ����
            if (state == State.DIE) yield break;

            //���Ϳ� ���ΰ� ĳ���� ������ �Ÿ� ����
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            //���� �����Ÿ� ������ ���Դ��� Ȯ��
            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            //else if (distance >= traceDist)     //���� �����Ÿ� �ܺο��� ���� ����
            //{
            //    state = State.TRACE;
            //}
            else if (distance <= traceDist)     //���� �����Ÿ� ���ο��� ���� ����
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }

    //������ ���¿� ���� ������ ������ ����
    public virtual IEnumerator MonsterAction()
    {
            while (!isDie)
            {
                switch (state)
                {
                    //IDLE ����
                    case State.IDLE:
                        //���� ����
                        agent.isStopped = true;

                        // Animator�� IsTrace ������ false�� ����
                        anim.SetBool(hashTrace, false);
                        break;

                    //���� ����
                    case State.TRACE:
                        //���� ����� ��ǥ�� �̵� ����
                        agent.SetDestination(playerTr.position);
                        agent.isStopped = false;

                        // Animator�� IsTrace ������ true�� ����
                        anim.SetBool(hashTrace, true);

                        // Animator�� IsAttack ������ false�� ����
                        anim.SetBool(hashAttack, false);
                        break;

                    //���� ����
                    case State.ATTACK:
                        // Animator�� IsAttack ������ true�� ����
                        //anim.SetBool(hashAttack, true);

                        switch(monsterType)
                        {
                            case Type.A:
                                anim.SetBool(hashAttack, true);

                                break;

                            case Type.B:
                              
                                 yield return new WaitForSeconds(0.5f);
                                 GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                                 Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                                 rigidBullet.velocity = transform.forward * 3;

                                GameObject leftInstantBullet = Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -15, 0)));
                                Rigidbody rigidLeftBullet = leftInstantBullet.GetComponent<Rigidbody>();
                                rigidLeftBullet.velocity = leftInstantBullet.transform.forward * 3;

                                // ������ �ҷ� �߻�
                                GameObject rightInstantBullet = Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 15, 0)));
                                Rigidbody rigidRightBullet = rightInstantBullet.GetComponent<Rigidbody>();
                                rigidRightBullet.velocity = rightInstantBullet.transform.forward * 3;

                                yield return new WaitForSeconds(2f);
                            break;
                            
                            
                                
                        }

                        break;

                    //���
                    case State.DIE:
                        isDie = true;
                        //���� ����
                        agent.isStopped = true;
                        //��� �ִϸ��̼� ����
                        anim.SetTrigger(hashDie);
                        //������ Collider ������Ʈ ��Ȱ��ȭ
                        GetComponent<CapsuleCollider>().enabled = false;
                        break;
                }
                yield return new WaitForSeconds(0.3f);
            }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Melee"))
        {
            if (!isDamage)
            {
                Weapon weapon = other.GetComponent<Weapon>();
                hp -= weapon.damage;
                anim.SetTrigger(hashHit);

                GameManager.instance.SetBossHealth(hp);              // ���� ���� ü�� ����

                if (hp < 0)
                {
                    state = State.DIE;
                    GameManager.instance.BossDead();

                }

                StartCoroutine(OnDamage());
            }

        }
    }

    void OnDrawGizmos()
    {
        //���� �����Ÿ� ǥ��
        if (state == State.TRACE)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        //���� �����Ÿ� ǥ��
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        foreach (MeshRenderer mesh in meshs)         //�ݺ����� ����Ͽ� ��� ������ ���� ����
        {
            //mesh.material.color = Color.red;
            Material mat = mesh.material;
            mat.SetColor("_EmissionColor", Color.gray * 0.5f);
        }
        yield return new WaitForSeconds(0.5f);  //���� Ÿ��

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            Material mat = mesh.material;
            mat.SetColor("_EmissionColor", Color.black);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}