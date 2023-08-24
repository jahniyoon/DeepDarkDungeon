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

    
    public readonly int hashAttack1 = Animator.StringToHash("AttackPattern1");
    public readonly int hashAttack2 = Animator.StringToHash("AttackPattern2");
    public readonly int hashAttack3 = Animator.StringToHash("AttackPattern3");
    public readonly int hashAttack4 = Animator.StringToHash("AttackPattern4");
    



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

    public override IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                //IDLE 상태
                case State.IDLE:
                    //추적 중지
                    agent.isStopped = true;

                    // Animator의 IsTrace 변수를 false로 설정
                    anim.SetBool(hashTrace, false);
                    break;

                //추적 상태
                case State.TRACE:
                    //추적 대상의 좌표로 이동 시작
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;

                    // Animator의 IsTrace 변수를 true로 설정
                    anim.SetBool(hashTrace, true);

                    // Animator의 IsAttack 변수를 false로 설정
                    anim.SetBool(hashAttack1, false);
                    anim.SetBool(hashAttack2, false);
                    anim.SetBool(hashAttack3, false);
                    break;

                //공격 상태
                case State.ATTACK:

                    //int attackPattern = Random.Range(1, 5);
                    int attackPattern = 3;

                    // 선택한 패턴에 따라 동작 실행
                    switch (attackPattern)
                    {
                        case 1:
                            anim.SetBool(hashAttack1, true);
                            yield return new WaitForSeconds(1.0f);
                            anim.SetBool(hashAttack1, false);
                            break;
                        case 2:
                            anim.SetBool(hashAttack2, true);
                            yield return new WaitForSeconds(1.0f);
                            anim.SetBool(hashAttack2, false);
                            break;
                        case 3:
                            anim.SetBool(hashAttack3, true);
                            yield return new WaitForSeconds(3.5f);
                            anim.SetBool(hashAttack3, false);
                            break;
                        case 4:                               
                            anim.SetBool(hashAttack4, true);
                       
                            yield return new WaitForSeconds(0.5f);     //애니메이션 60프레임 1초니까 0.5 30프레임
                            GameObject instantTornado = Instantiate(tornado, tornadoA.position, tornadoA.rotation);
                            BossBullet bulletTornadoA = instantTornado.GetComponent<BossBullet>();     
                            bulletTornadoA.target = playerTarget;
                            yield return new WaitForSeconds(0.5f);

                            anim.SetBool(hashAttack4, false);
                            yield return new WaitForSeconds(1.0f);
                            break;
                           


                    }
                    break;

                //사망
                case State.DIE:
                    isDie = true;
                    //추적 중지
                    agent.isStopped = true;
                    //사망 애니메이션 실행
                    //anim.SetTrigger(hashDie);
                    //몬스터의 Collider 컴포넌트 비활성화
                    GetComponent<BoxCollider>().enabled = false;
                    Destroy(gameObject);
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
        //if(isLook && playerTarget != null)
        //{
        //    float h = Input.GetAxisRaw("Horizontal");
        //    float v = Input.GetAxisRaw("Vertical");
        //    lookVec = new Vector3(h, 0, v) * 0.2f;
        //    transform.LookAt(playerTarget.position + lookVec);
        //}
       
       

    }
   
}
