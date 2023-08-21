//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using static UnityEngine.GraphicsBuffer;

//public class BossTest : EnemyTest
//{

//    public GameObject missile;
//    public Transform missilePortA;
//    public Transform missilePortB;
//    public bool isLook;      //바라보는

//    Vector3 lookVec;     //플레이어 움직임 예측 백터 변수
//    Vector3 tauntVec;


//    // Start is called before the first frame update
//    void Awake()   //awake함수는 자식 스크립트만 단독 실행 enemy awke스크립트 상속 안됨
//    {
//        rigid = GetComponent<Rigidbody>();
//        boxCollider = GetComponent<BoxCollider>();
//        meshs = GetComponentsInChildren<MeshRenderer>();  // Material은 MeshRenderer로 가져와야된다
//        nav = GetComponent<NavMeshAgent>();
//        anim = GetComponentInChildren<Animator>();


//        nav.isStopped = true;
//        StartCoroutine(Think());
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (isDead)
//        {
//            StopAllCoroutines();           //죽었을 때 패턴  코루틴 완전 정지
//            return;                        //리턴으로 아래 로직 실행 못하게 막음
//        }
//        if (isLook)
//        {
//            float h = Input.GetAxisRaw("Horizontal");
//            float v = Input.GetAxisRaw("Vertical");
//            lookVec = new Vector3(h, 0, v) * 5f;   //플레이어 입력값으로 예측 백터값 생성
//            transform.LookAt(target.position + lookVec);
//        }
//        else
//        {
//            nav.SetDestination(tauntVec);    //점프공격할 때 목표지점 이동
//        }

//    }

//    IEnumerator Think()
//    {
//        yield return new WaitForSeconds(0.1f);

//        int ranAction = Random.Range(0, 5);
//        switch (ranAction)
//        {
//            case 0:
//            case 1:
//                StartCoroutine(MissileShot());
//                break;
//            case 2:
//            case 3:
//                StartCoroutine(RockShot());
//                //돌 굴러가는 패턴
//                break;
//            case 4:
//                StartCoroutine(Taunt());
//                //점프 공격 패턴
//                break;

//        }

//    }

//    IEnumerator MissileShot()
//    {
//        anim.SetTrigger("doShot");
//        yield return new WaitForSeconds(0.2f);
//        GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
//        BossMissile bossMissileA = instantMissileA.GetComponent<BossMissile>();     //미사일 스크립트까지 접근하여 목표물 설정(유도)
//        bossMissileA.target = target;

//        yield return new WaitForSeconds(0.3f);
//        GameObject instantMissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
//        BossMissile bossMissileB = instantMissileB.GetComponent<BossMissile>();     //미사일 스크립트까지 접근하여 목표물 설정(유도)
//        bossMissileB.target = target;

//        yield return new WaitForSeconds(2f);

//        StartCoroutine(Think());
//    }

//    IEnumerator RockShot()
//    {
//        isLook = false;      //기 모을 때는 바라보기 중지하도록 플래그 설정
//        anim.SetTrigger("doBigShot");
//        Instantiate(bullet, transform.position, transform.rotation);
//        yield return new WaitForSeconds(3f);

//        isLook = true;
//        StartCoroutine(Think());
//    }


//    IEnumerator Taunt()
//    {
//        tauntVec = target.position + lookVec;

//        isLook = false;
//        nav.isStopped = false;
//        boxCollider.enabled = false;   //박스콜라이더가 플레이어를 밀지 않도록 비활성화
//        anim.SetTrigger("doTaunt");

//        yield return new WaitForSeconds(1.5f);
//        meleeArea.enabled = true;     //1.5초 후 공격 범위 콜라이더 활성화

//        yield return new WaitForSeconds(0.5f);
//        meleeArea.enabled = false;         //내려찍은 후라서 돌려놓는다

//        yield return new WaitForSeconds(1f);
//        isLook = true;
//        nav.isStopped = true;
//        boxCollider.enabled = true;


//        StartCoroutine(Think());
//    }

//}