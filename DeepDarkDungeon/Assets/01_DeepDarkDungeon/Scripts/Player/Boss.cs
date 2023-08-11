using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    public ParticleSystem particle;

    public bool isLook;

    Vector3 lookVec;     //플레이어 움직임 예측 백터 변수


    // Start is called before the first frame update
    void Awake()   //awake함수는 자식 스크립트만 단독 실행 enemy awke스크립트 상속 안됨
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();  // Material은 MeshRenderer로 가져와야된다
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        StartCoroutine(Think());
    }


    // Update is called once per frame
    void Update()
    {
        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;   //플레이어 입력값으로 예측 백터값 생성
            transform.LookAt(target.position + lookVec);
        }
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 5);
        switch (ranAction)
        {
            case 0:
            case 1:
                StartCoroutine(MissileShot());
                break;
            case 2:
            case 3:
                //돌 굴러가는 패턴
                break;
            case 4:
                //점프 공격 패턴
                break;

        }

    }

    IEnumerator MissileShot()
    {
        anim.SetTrigger("doShot");
        yield return new WaitForSeconds(0.2f);
        //GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        //BossMissile bossMissileA = instantMissileA.GetComponent<BossMissile>();     //미사일 스크립트까지 접근하여 목표물 설정(유도)
        //bossMissileA.target = target;

        //yield return new WaitForSeconds(0.3f);
        //GameObject instantMissileB = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        //BossMissile bossMissileB = instantMissileA.GetComponent<BossMissile>();     //미사일 스크립트까지 접근하여 목표물 설정(유도)
        //bossMissileA.target = target;

        //yield return new WaitForSeconds(2f);

        //StartCoroutine(Think());
    }

}
