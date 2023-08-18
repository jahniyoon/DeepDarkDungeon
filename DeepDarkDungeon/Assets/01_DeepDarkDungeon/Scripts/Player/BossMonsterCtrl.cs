using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMonsterCtrl : MonoBehaviour
{
 
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    //������ ���� ����
    public State state = State.IDLE;
    //���� �����Ÿ�
    public float traceDist = 10.0f;
    //���� �����Ÿ�
    public float attackDist = 1.0f;
    //������ ��� ����
    public bool isDie = false;

    //������Ʈ ĳ�ø� ó���� ����
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;

    private MeshRenderer[] meshs;
    [SerializeField] private Transform playerTarget;


    // Animator �Ķ������ �ؽð� ����
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    //private readonly int hashHit = Animator.StringToHash("Hit");

    private int hp = 100;

    private bool isDamage;
    
    

    // Start is called before the first frame update
    void Start()
    {
        //������ Transform �Ҵ�
        monsterTr = GetComponent<Transform>();

        //���� ����� Player�� Transform �Ҵ�
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        //NavMeshAgent ������Ʈ �Ҵ�
        agent = GetComponent<NavMeshAgent>();

        //Animator ������Ʈ �Ҵ�
        anim = GetComponent<Animator>();

        meshs = GetComponentsInChildren<MeshRenderer>();
 
       

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
            else if (distance >= traceDist)     //���� �����Ÿ� ������ ���Դ��� Ȯ��
            {
                state = State.TRACE;
            }
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
    IEnumerator MonsterAction()
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
                    anim.SetBool(hashAttack, true);
                    break;

                //���
                case State.DIE:
                    //isDie = true;
                    ////���� ����
                    //agent.isStopped = true;
                    ////��� �ִϸ��̼� ����
                    ////anim.SetTrigger(hashDie);
                    ////������ Collider ������Ʈ ��Ȱ��ȭ
                    //GetComponent<CapsuleCollider>().enabled = false;
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Melee"))
        {
            if(!isDamage)
            {
                Weapon weapon = coll.collider.GetComponent<Weapon>();
                //������ hp ����
                hp -= weapon.damage;
                if (hp < 0)
                {
                    state = State.DIE;
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
            mesh.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.5f);  //���� Ÿ��

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;       
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
