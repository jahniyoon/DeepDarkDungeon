using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Boss : Monster
{
    public GameObject tornado;
    public Transform tornadoA;



    public bool isLook;

    Vector3 lookVec;
    Vector3 tauntVec;

    public new readonly int hashTrace = Animator.StringToHash("IsTrace");
    public readonly int hashAttack1 = Animator.StringToHash("AttackPattern1");
    public readonly int hashAttack2 = Animator.StringToHash("AttackPattern2");
    public readonly int hashAttack3 = Animator.StringToHash("AttackPattern3");
    public readonly int hashAttack4 = Animator.StringToHash("AttackPattern4");
    public new readonly int  hashHit = Animator.StringToHash("Hit");
    public new readonly int hashDie = Animator.StringToHash("Die");

    // Start is called before the first frame update
    void Awake()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();

        
    }

    //IEnumerator Think()
    //{
    //    yield return null;
    //}

    public override IEnumerator MonsterAction()
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
                    anim.SetBool(hashAttack1, false);
                    anim.SetBool(hashAttack2, false);
                    anim.SetBool(hashAttack3, false);
                    break;

                //���� ����
                case State.ATTACK:

                    int attackPattern = Random.Range(1, 5);
                    

                    // ������ ���Ͽ� ���� ���� ����
                    switch (attackPattern)
                    {
                        case 1:
                            anim.SetBool(hashAttack1, true);
                            break;
                        case 2:
                            anim.SetBool(hashAttack2, true);
                            break;
                        case 3:
                            anim.SetBool(hashAttack3, true);
                            break;
                        case 4:
                            anim.SetBool(hashAttack4, true);
                            yield return new WaitForSeconds(0.2f);
                            GameObject instantTornado = Instantiate(tornado, tornadoA.position, tornadoA.rotation);
                            BossBullet bulletTornadoA = instantTornado.GetComponent<BossBullet>();     //�̻��� ��ũ��Ʈ���� �����Ͽ� ��ǥ�� ����(����)
                            bulletTornadoA.target = playerTarget;

                            //yield return new WaitForSeconds(2.0f);
                            break;
                    }
                    state = State.TRACE;
                    break;

                //���
                case State.DIE:
                    isDie = true;
                    //���� ����
                    agent.isStopped = true;
                    //��� �ִϸ��̼� ����
                    //anim.SetTrigger(hashDie);
                    //������ Collider ������Ʈ ��Ȱ��ȭ
                    GetComponent<BoxCollider>().enabled = false;
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isDie)
        {
            StopAllCoroutines();
            return;
        }
        if(isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 10;
            transform.LookAt(playerTarget.position + lookVec);
        }
        else
        {
            agent.SetDestination(tauntVec);
        }
       

    }
   
}
