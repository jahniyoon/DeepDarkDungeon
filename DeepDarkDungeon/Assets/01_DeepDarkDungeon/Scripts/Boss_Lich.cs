using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Lich : MonoBehaviour
{
    Animator animator;
    Rigidbody rigid;
    [Header("Status")]
    bool isDamage;
    bool isDead;
    bool isChase;
    bool isAttack;
    bool isDash;
    bool startChase;

    [Header("Boss Info")]

    public int bossMaxHealth;
    public int bossCurHealth;
    public string bossName;

    BoxCollider hitBox;
    MeshRenderer[] meshs;  //피격 이펙트를 모든 메테리얼로



    [Header("Target")]
    public SphereCollider scanCollider;
    public Transform target;
    public NavMeshAgent nav;

    [Header("Attack")]
    public BoxCollider meleeArea;
    public BoxCollider poisonArea;
    public GameObject skeletonePrefab;
    public GameObject spawnAuraPrefab;

    [Header("Drop Item")]
    public GameObject exitKeyPrefab;
    public GameObject bossDeadPrefab;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        scanCollider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();

        hitBox = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();  // Material은 MeshRenderer로 가져와야된다


        if (nav != null)
        { nav = GetComponent<NavMeshAgent>(); }
    }


    void Start()
    {
        GameManager.instance.SetBossMaxHealth(bossMaxHealth, bossName); // 보스 맥스 체력 셋팅
        GameManager.instance.SetBossHealth(bossCurHealth);              // 보스 현재 체력 셋팅
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !startChase && GameManager.instance.isBoss) //  플레이어를 감지하면 ChaseStart           
        {
            startChase = true;
            ChaseStart();
        }
        if (nav != null && nav.enabled && target != null)       //navi가 활성화되어있을때만
        {
            nav.SetDestination(target.position);                //SetDestination 도착할 목표 위치 지정 함수 
            nav.isStopped = !isChase;                            //isStopped을 사용하여 완벽하게 멈추도록
        }

    }
    void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }
    void ChaseStart()
    {
        isChase = true;
        animator.SetBool("isMove", true);
    }
    void Targeting()
    {
            float targetRadius = 0.3f;
            float targetRange = 0.3f;                   //sphererCast의 반지름, 길이를 조정 변수

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
            targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));    //자신의 위치, 반지름, 쏘는 방향, 레이 쏘는 거리      
            Debug.DrawRay(transform.position, transform.forward, Color.red);



        //rayHit 변수에 데이터가 들어오면 공격 코루틴 실행한다
        if (rayHits.Length > 0 && !isAttack)  //범위 안이고 공격중이면 공격하지 않는다
            {
                int nextAttack = Random.Range(0,3);
                
                    switch (nextAttack)
                {
                    case 0:
                        StartCoroutine(MeleeAttack());  // 근접 공격
                        break;
                    case 1:
                        StartCoroutine(PoisonAttack()); // 독 공격
                        break;
                    case 2:
                        StartCoroutine(SkeletoneSpawn());   // 스켈레톤 스폰
                        break;
            }
            
        }
        else if (0 == Random.Range(0, 10) && !isDash && !isAttack && startChase)
        {
            StartCoroutine(DashAttack());
            Debug.Log("대시어택");
        }

    }
    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;      //물리력이 navAgent 이동을 방해하지 않도록 로직 추가
            rigid.angularVelocity = Vector3.zero;    //물리회전속도 0으로
        }

    }

    //=====================Attack=====================
    IEnumerator MeleeAttack()
    {
        //먼저 정지를 한 다음, 애니메이션과 함께 공격범위 활성화
        isChase = false;
        isAttack = true;
        animator.SetTrigger("isMeleeAttack");

        yield return new WaitForSeconds(0.9f);
        meleeArea.enabled = true;  //공격범위 활성화

        yield return new WaitForSeconds(0.6f);
        meleeArea.enabled = false;  //공격범위 비활성화
        yield return new WaitForSeconds(0.5f);


        isChase = true;
        isAttack = false;
    }
    IEnumerator PoisonAttack()
    {
        //먼저 정지를 한 다음, 애니메이션과 함께 공격범위 활성화
        isChase = false;
        isAttack = true;
        animator.SetTrigger("isPoisonAttack");

        yield return new WaitForSeconds(1.05f);
        meleeArea.enabled = true;  //공격범위 활성화



        yield return new WaitForSeconds(1.3f);
        meleeArea.enabled = false;  //공격범위 비활성화



        yield return new WaitForSeconds(0.5f);


        isChase = true;
        isAttack = false;
    }
    IEnumerator DashAttack()
    {
        isChase = false;

        isDash = true;
        animator.SetTrigger("isDashAttack");
        
        yield return new WaitForSeconds(4f);

        isChase = true;

        float originalSpeed = nav.speed;
        nav.speed *= 20;
        meleeArea.enabled = true;  //공격범위 활성화
        yield return new WaitForSeconds(0.2f);
        nav.speed = originalSpeed;

        yield return new WaitForSeconds(0.3f);
        //dashEffect.SetActive(false);


        meleeArea.enabled = false;  //공격범위 비활성화


        yield return new WaitForSeconds(8f);
        isDash = false;


    }
    IEnumerator SkeletoneSpawn()
    {
        isChase = false;
        isAttack = true;

        animator.SetTrigger("isEnemySpawn");
        Vector3 frontPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f); // 죽음 애니메이션 위치
        Vector3 backLeftPosition = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z + 1f); // 죽음 애니메이션 위치
        Vector3 backRightPosition = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z + 1f); // 죽음 애니메이션 위치

        GameObject aura1 = Instantiate(spawnAuraPrefab, frontPosition, transform.rotation);
        GameObject aura2 = Instantiate(spawnAuraPrefab, backLeftPosition, transform.rotation);
        GameObject aura3 = Instantiate(spawnAuraPrefab, backRightPosition, transform.rotation);
        Destroy(aura1, 3.5f);
        Destroy(aura2, 3.5f);
        Destroy(aura3, 3.5f);

        yield return new WaitForSeconds(3f);
       


        GameObject newSkeletone1 = Instantiate(skeletonePrefab, frontPosition, transform.rotation);
        GameObject newSkeletone2 = Instantiate(skeletonePrefab, backLeftPosition, transform.rotation);
        GameObject newSkeletone3 = Instantiate(skeletonePrefab, backRightPosition, transform.rotation);

       

        isChase = true;
        isAttack = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Melee"))
        {
            if (!isDamage)
            {
                Weapon weapon = other.GetComponent<Weapon>();
                //몬스터의 hp 차감
                StartCoroutine(OnDamage(weapon.damage));


                if (bossCurHealth <= 0 && !isDead)
                {
                    isDead = true;
                    GameManager.instance.BossDead();
                    StartCoroutine("Death");
                }
            }
        }
    }
    // 보스 데미지 입었을 때
    IEnumerator OnDamage(int damage)
    {
        hitBox.enabled = false; 

        animator.SetTrigger("isDamage");
        bossCurHealth -= damage;
        GameManager.instance.SetBossHealth(bossCurHealth);

        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.2f);

        if (bossCurHealth > 0)
        {
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }
        }
       

        yield return new WaitForSeconds(1.5f);
        hitBox.enabled = true; //콜라이더 활성화


    }
    void BossDamage(int damage)
    {
        animator.SetTrigger("isDamage");
        bossCurHealth -= damage;
        GameManager.instance.SetBossHealth(bossCurHealth);
    }
    // 보스 죽었을 때 애니메이션 이후 열쇠 드롭
    IEnumerator Death() 
    {
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.gray;
        }
        isChase = false;
        nav.enabled = false;      //사망 리액션 유지하기 위해서

        animator.SetTrigger("BossDead");
        animator.SetBool("isDie", true);

        GameManager.instance.isBossDie = true;

        yield return new WaitForSeconds(2f);

        Vector3 keyPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z); // 열쇠 드롭 위치
        Vector3 originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z); // 죽음 애니메이션 위치
        Quaternion originalRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90f, 1); // 열쇠 드롭 시 각도 보정
        if (exitKeyPrefab != null)
        {
            GameObject key = Instantiate(exitKeyPrefab, keyPosition, originalRotation);
            GameObject deathEffect = Instantiate(bossDeadPrefab, originalPosition, originalRotation);   // 보스 죽음 이펙트
            key.tag = "Item";
        }
        Destroy(gameObject);
    }
   
}

//public GameObject missile;
//public Transform missilePortA;
//public Transform missilePortB;
//public bool isLook;      //바라보는

//Vector3 lookVec;     //플레이어 움직임 예측 백터 변수
//Vector3 tauntVec;


//// Start is called before the first frame update
//void Awake()   //awake함수는 자식 스크립트만 단독 실행 enemy awke스크립트 상속 안됨
//{
//    rigid = GetComponent<Rigidbody>();
//    boxCollider = GetComponent<BoxCollider>();
//    meshs = GetComponentsInChildren<MeshRenderer>();  // Material은 MeshRenderer로 가져와야된다
//    nav = GetComponent<NavMeshAgent>();
//    anim = GetComponentInChildren<Animator>();


//    nav.isStopped = true;
//    StartCoroutine(Think());
//}

//// Update is called once per frame
//void Update()
//{
//    if (isDead)
//    {
//        StopAllCoroutines();           //죽었을 때 패턴  코루틴 완전 정지
//        return;                        //리턴으로 아래 로직 실행 못하게 막음
//    }
//    if (isLook)
//    {
//        float h = Input.GetAxisRaw("Horizontal");
//        float v = Input.GetAxisRaw("Vertical");
//        lookVec = new Vector3(h, 0, v) * 5f;   //플레이어 입력값으로 예측 백터값 생성
//        transform.LookAt(target.position + lookVec);
//    }
//    else
//    {
//        nav.SetDestination(tauntVec);    //점프공격할 때 목표지점 이동
//    }

//}

//IEnumerator Think()
//{
//    yield return new WaitForSeconds(0.1f);

//    int ranAction = Random.Range(0, 5);
//    switch (ranAction)
//    {
//        case 0:
//        case 1:
//            StartCoroutine(MissileShot());
//            break;
//        case 2:
//        case 3:
//            StartCoroutine(RockShot());
//            //돌 굴러가는 패턴
//            break;
//        case 4:
//            StartCoroutine(Taunt());
//            //점프 공격 패턴
//            break;

//    }

//}

//IEnumerator MissileShot()
//{
//    anim.SetTrigger("doShot");
//    yield return new WaitForSeconds(0.2f);
//    GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
//    BossMissile bossMissileA = instantMissileA.GetComponent<BossMissile>();     //미사일 스크립트까지 접근하여 목표물 설정(유도)
//    bossMissileA.target = target;

//    yield return new WaitForSeconds(0.3f);
//    GameObject instantMissileB = Instantiate(missile, missilePortA.position, missilePortA.rotation);
//    BossMissile bossMissileB = instantMissileA.GetComponent<BossMissile>();     //미사일 스크립트까지 접근하여 목표물 설정(유도)
//    bossMissileB.target = target;

//    yield return new WaitForSeconds(2f);

//    StartCoroutine(Think());
//}

//IEnumerator RockShot()
//{
//    isLook = false;      //기 모을 때는 바라보기 중지하도록 플래그 설정
//    anim.SetTrigger("doBigShot");
//    Instantiate(bullet, transform.position, transform.rotation);
//    yield return new WaitForSeconds(3f);

//    isLook = true;
//    StartCoroutine(Think());
//}


//IEnumerator Taunt()
//{
//    tauntVec = target.position + lookVec;

//    isLook = false;
//    nav.isStopped = false;
//    boxCollider.enabled = false;   //박스콜라이더가 플레이어를 밀지 않도록 비활성화
//    anim.SetTrigger("doTaunt");

//    yield return new WaitForSeconds(1.5f);
//    meleeArea.enabled = true;     //1.5초 후 공격 범위 콜라이더 활성화

//    yield return new WaitForSeconds(0.5f);
//    meleeArea.enabled = false;         //내려찍은 후라서 돌려놓는다

//    yield return new WaitForSeconds(1f);
//    isLook = true;
//    nav.isStopped = true;
//    boxCollider.enabled = true;


//    StartCoroutine(Think());
//}
