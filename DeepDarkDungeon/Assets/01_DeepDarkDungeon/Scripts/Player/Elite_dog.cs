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

                

                    int attackPattern = Random.Range(1, 3);


                    // ������ ���Ͽ� ���� ���� ����
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


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTr.position);

        //if (isDie)
        //{
        //    StopAllCoroutines();           //�׾��� �� ����  �ڷ�ƾ ���� ����
        //    return;                        //�������� �Ʒ� ���� ���� ���ϰ� ����
        //}
        //if (isLook)
        //{
        //    float h = Input.GetAxisRaw("Horizontal");
        //    float v = Input.GetAxisRaw("Vertical");
        //    lookVec = new Vector3(h, 0, v) * 5f;   //�÷��̾� �Է°����� ���� ���Ͱ� ����
        //    transform.LookAt(playerTarget.position + lookVec);
        //}
        //else
        //{
        //    //agent.SetDestination(tauntVec);    //���������� �� ��ǥ���� �̵�
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
