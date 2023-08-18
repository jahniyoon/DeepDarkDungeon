using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Monster
{
    public GameObject Rock;

    public Transform RockA;
    public Transform RockB;
    public Transform RockC;
    public Transform RockD;
    public Transform RockE;

    public bool isLook;

    Vector3 moveVector;
    Vector3 lookVec;
    Vector3 tauntVec;

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

        StartCoroutine(Think());
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
            lookVec = new Vector3(h, 0, v) * 5;
            transform.LookAt(playerTarget.position + lookVec);
        }
        else
        {
            agent.SetDestination(tauntVec);
        }
       

    }
    //void BossSkill()
    //{
    //    float distance = Vector3.Distance(playerTr.position, monsterTr.position);

    //    if (distance >= traceDist)
    //    {
    //        int ranAction = Random.Range(0, 5);
    //        switch (ranAction)
    //        {
    //            case 0:
    //            case 1:
    //            case 2:
    //            case 3:
    //                StartCoroutine(RockAttack());
    //                break;
    //            case 4:
    //                //StartCoroutine();

    //                break;

    //        }
    //    }
    //}


    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 5);
        switch (ranAction)
        {
            case 0:
            case 1:
                StartCoroutine(Taunt());
                break;
            case 2:
            case 3:
                StartCoroutine(RockAttack());
                //돌 굴러가는 패턴
                break;
            case 4:
                break;

        }

    }





    IEnumerator RockAttack()
    {
        agent.isStopped = true;

        anim.SetTrigger("doRock");
        yield return new WaitForSeconds(1.0f);
        GameObject instantRockA = Instantiate(Rock, RockA.position, RockA.rotation);

        yield return new WaitForSeconds(3f);
        agent.isStopped = false;


        StartCoroutine(Think());
        
    }

    IEnumerator Taunt()
    {
        tauntVec = playerTarget.position + lookVec;

        yield return null;      //임시로 넣은 값

        StartCoroutine(Think());
    }
}
