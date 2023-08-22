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
    public readonly int hashJumpAttack1 = Animator.StringToHash("doJumpAttack1");


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

        StartCoroutine(Think());
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie)
        {
            StopAllCoroutines();           //�׾��� �� ����  �ڷ�ƾ ���� ����
            return;                        //�������� �Ʒ� ���� ���� ���ϰ� ����
        }
        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;   //�÷��̾� �Է°����� ���� ���Ͱ� ����
            transform.LookAt(playerTarget.position + lookVec);
        }
        else
        {
            agent.SetDestination(tauntVec);    //���������� �� ��ǥ���� �̵�
        }
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        state = State.ATTACK;

        int attackPattern = Random.Range(0, 2);
        switch(attackPattern)
        {
            case 0:
                //anim.SetBool(hashAttack, true);
                //yield return new WaitForSeconds(0.5f);
                NormalAttack();
                break;
                case 1:
                JumpAttack();
                    break;
        }    


    }

    IEnumerator NormalAttack()
    {
        anim.SetBool(hashAttack, true);
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Think());
    }

    IEnumerator JumpAttack()
    {
        anim.SetTrigger(hashJumpAttack);
        yield return new WaitForSeconds(2.0f);

        anim.SetTrigger(hashJumpAttack1);
        yield return new WaitForSeconds(1.2f);

        StartCoroutine(Think());

    }
}
