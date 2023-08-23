using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elite_dog : Monster
{
    public bool isLook;

    Vector3 lookVec;
    Vector3 tauntVec;

    
    public readonly int hashJumpAttack = Animator.StringToHash("doJumpAttack");
    


    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();

        //StartCoroutine(Think());
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
                    anim.SetBool(hashAttack, false);
                    break;

                //공격 상태
                case State.ATTACK:
                // Animator의 IsAttack 변수를 true로 설정
                //anim.SetBool(hashAttack, true);

                

                    int attackPattern = Random.Range(1, 3);


                    // 선택한 패턴에 따라 동작 실행
                    switch (attackPattern)
                    {
                        case 1:
                            anim.SetBool(hashAttack, true);
                            yield return new WaitForSeconds(2.5f);
                            anim.SetBool(hashAttack, false);
                            break;

                        case 2:
                            anim.SetBool(hashJumpAttack, true);
                            yield return new WaitForSeconds(2.0f);
                            anim.SetBool(hashJumpAttack, false);

                            yield return new WaitForSeconds(0.1f);
                            break;
                      
                    }
                    break;

                //사망
                case State.DIE:
                    isDie = true;
                    //추적 중지
                    agent.isStopped = true;
                    //사망 애니메이션 실행
                    anim.SetTrigger(hashDie);
                    //몬스터의 Collider 컴포넌트 비활성화
                    GetComponent<CapsuleCollider>().enabled = false;
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTr.position);

        //if (isDie)
        //{
        //    StopAllCoroutines();           //죽었을 때 패턴  코루틴 완전 정지
        //    return;                        //리턴으로 아래 로직 실행 못하게 막음
        //}
        //if (isLook)
        //{
        //    float h = Input.GetAxisRaw("Horizontal");
        //    float v = Input.GetAxisRaw("Vertical");
        //    lookVec = new Vector3(h, 0, v) * 5f;   //플레이어 입력값으로 예측 백터값 생성
        //    transform.LookAt(playerTarget.position + lookVec);
        //}
        //else
        //{
        //    //agent.SetDestination(tauntVec);    //점프공격할 때 목표지점 이동
        //}
    }





























    //IEnumerator Think()
    //{
    //    yield return new WaitForSeconds(0.1f);

        

    //    int attackPattern = Random.Range(0, 2);

       

    //    switch(attackPattern)
    //    {
    //        case 0:
    //        case 1:
    //            JumpAttack();
    //                break;
    //    }    


    //}

    

    //IEnumerator JumpAttack()
    //{
    //    anim.SetTrigger(hashJumpAttack);
    //    yield return new WaitForSeconds(2.0f);

    //    anim.SetTrigger(hashJumpAttack1);
    //    yield return new WaitForSeconds(1.2f);

    //    StartCoroutine(Think());

    //}
}
