using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{


    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    public enum Type { A, B, C, D, E }
    public Type monsterType;

    //몬스터의 현재 상태
    public State state = State.IDLE;
    //추적 사정거리
    public float traceDist = 10.0f;
    //공격 사정거리
    public float attackDist = 1.0f;
    //몬스터의 사망 여부
    public bool isDie = false;

    //컴포넌트 캐시를 처리할 변수
    public Transform monsterTr;
    public Transform playerTr;
    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public NavMeshAgent agent;
    public Animator anim;

    public MeshRenderer[] meshs;
    public SkinnedMeshRenderer bossMesh;

    public Transform playerTarget;
    public GameObject monsterPrefab;
    public Transform spawnA;
    public Transform spawnB;
    public Transform spawnC;
    public Transform rockSpawnA;
    public Transform rockSpawnB;
    public Transform rockSpawnC;
    public GameObject bullet;
    public GameObject rock;
    //public GameObject monsterPrefab;

    [Header("Drop Item")]
    public GameObject exitKeyPrefab;
    public GameObject bossDeadPrefab;
    public GameObject goldPrefab;
    public int goldMaxValue;





    // Animator 파라미터의 해시값 추출
    public readonly int hashTrace = Animator.StringToHash("IsTrace");
    public readonly int hashAttack = Animator.StringToHash("IsAttack");
    public readonly int hashHit = Animator.StringToHash("Hit");
    public readonly int hashDie = Animator.StringToHash("Die");
    public readonly int hashAttack1 = Animator.StringToHash("IsAttack1");
    public readonly int hashSpawn = Animator.StringToHash("doSpawn");
    public readonly int hashRock = Animator.StringToHash("doRock");

    public bool isBoss;
    public int maxHp = 100;
    public int hp = 100;
    public string bossName;

    public bool isDamage;

    public int damage;
    bool traceStart; // 추적 시작기능. 



    // Start is called before the first frame update
    void Start()
    {
        //몬스터의 Transform 할당
        monsterTr = GetComponent<Transform>();

        //추적 대상인 Player의 Transform 할당
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        rigid = GetComponent<Rigidbody>();

        boxCollider = GetComponent<BoxCollider>();

        //NavMeshAgent 컴포넌트 할당
        agent = GetComponent<NavMeshAgent>();

        //Animator 컴포넌트 할당
        anim = GetComponent<Animator>();

        meshs = GetComponentsInChildren<MeshRenderer>();
        bossMesh = GetComponent<SkinnedMeshRenderer>();

        transform.LookAt(playerTr.position);

        if (isBoss)
        {
            GameManager.instance.SetBossMaxHealth(maxHp, bossName); // 보스 맥스 체력 셋팅
            GameManager.instance.SetBossHealth(hp);              // 보스 현재 체력 셋팅
        }
        //몬스터의 상태를 체크하는 코루틴 함수 호출
        StartCoroutine(CheckMonsterState());
        //상태에 따라 몬스터의 행동을 수행하는 코루틴 함수 호출
        StartCoroutine(MonsterAction());
    }


    //일정한 간격으로 몬스터의 행동 상태를 체크
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            //0.3초 동안 중지(대기)하는 동안 제어권을 메시지 루프에 양보
            yield return new WaitForSeconds(0.3f);

            //몬스터의 상태가 Die일 때 코루틴을 종료
            if (state == State.DIE) yield break;

            //몬스터와 주인공 캐릭터 사이의 거리 측정
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            //공격 사정거리 범위로 들어왔는지 확인
            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            //else if (distance >= traceDist)     //추적 사정거리 외부에서 추적 시작
            //{
            //    state = State.TRACE;
            //}
            else if (distance <= traceDist)     //추적 사정거리 내부에서 추적 시작
            {
                traceStart = true;              // 지환 : 추적 사정거리에 들어오면 계속 플레이어를 추적
                state = State.TRACE;
            }
            else if (!traceStart)               // 지환 : 추적 사정거리에 들어오면 계속 플레이어를 추적
            {
                state = State.IDLE;
            }
        }
    }

    //몬스터의 상태에 따라 몬스터의 동작을 수행
    public virtual IEnumerator MonsterAction()
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


                        switch(monsterType)
                        {
                            case Type.A:
                                anim.SetBool(hashAttack, true);
                                break;

                            case Type.B:

                            anim.SetBool(hashAttack, true);
                            yield return new WaitForSeconds(0.65f);

                            Vector3 fireBallTransform = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
                                 GameObject instantBullet = Instantiate(bullet, fireBallTransform, transform.rotation);
                                 Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                                 rigidBullet.velocity = transform.forward * 3;

                                GameObject leftInstantBullet = Instantiate(bullet, fireBallTransform, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -15, 0)));
                                Rigidbody rigidLeftBullet = leftInstantBullet.GetComponent<Rigidbody>();
                                rigidLeftBullet.velocity = leftInstantBullet.transform.forward * 3;

                                
                                GameObject rightInstantBullet = Instantiate(bullet, fireBallTransform, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 15, 0)));
                                Rigidbody rigidRightBullet = rightInstantBullet.GetComponent<Rigidbody>();
                                rigidRightBullet.velocity = rightInstantBullet.transform.forward * 3;

                                yield return new WaitForSeconds(1.5f);                           
                            break;

                        case Type.C:        
                            
                            int knight = Random.Range(0, 3);
                            
                            switch (knight)
                            {
                                case 0:
                                    anim.SetBool(hashAttack, true);
                                    yield return new WaitForSeconds(2.0f);
                                    anim.SetBool(hashAttack, false);
                                    yield return new WaitForSeconds(0.1f);
                                    break;

                                case 1:
                                    anim.SetBool(hashAttack1, true);
                                    yield return new WaitForSeconds(2.0f);
                                    anim.SetBool(hashAttack1, false);
                                    yield return new WaitForSeconds(0.1f);
                                    break;

                                case 2:
                                    anim.SetBool(hashSpawn, true);
                                    yield return new WaitForSeconds(1.5f);

                                    GameObject instantSpawnA = Instantiate(monsterPrefab, spawnA.position, spawnA.rotation);
                                    GameObject instantSpawnB = Instantiate(monsterPrefab, spawnB.position, spawnB.rotation);
                                    GameObject instantSpawnC = Instantiate(monsterPrefab, spawnC.position, spawnC.rotation);
                                    anim.SetBool(hashSpawn, false);

                                    yield return new WaitForSeconds(1.0f);
                                    break;
                            }
                            break;

                            case Type.D:    

                            int dog = Random.Range(0, 2);

                            switch(dog)
                            {
                                case 0:
                                    anim.SetBool(hashAttack, true);
                                    yield return new WaitForSeconds(2.5f);
                                    anim.SetBool(hashAttack, false);
                                    yield return new WaitForSeconds(0.1f);
                                    break;
                                case 1:
                                    anim.SetBool(hashRock, true);
                                    yield return new WaitForSeconds(0.76f);

                                    GameObject instantSpawnA = Instantiate(rock, rockSpawnA.position, rockSpawnA.rotation);
                                    Rigidbody rigidRock = instantSpawnA.GetComponent<Rigidbody>();
                                    rigidRock.velocity = transform.forward * 4;

                                    GameObject leftInstantSpawnB = Instantiate(rock, rockSpawnB.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -30, 0)));
                                    Rigidbody rigidLeftrock = leftInstantSpawnB.GetComponent<Rigidbody>();
                                    rigidRock.velocity = leftInstantSpawnB.transform.forward * 4;

                                    GameObject rightInstantSpawnC = Instantiate(rock, rockSpawnC.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 30, 0)));
                                    Rigidbody rigidRightrock = rightInstantSpawnC.GetComponent<Rigidbody>();
                                    rigidRock.velocity = rightInstantSpawnC.transform.forward * 4;


                                    //GameObject instantRock = Instantiate(rock, transform.position, transform.rotation);
                                    // Rigidbody rigidRock = instantRock.GetComponent<Rigidbody>();
                                    //rigidRock.velocity = transform.forward * 10;

                                    //GameObject leftInstantRock = Instantiate(rock, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -30, 0)));
                                    //Rigidbody rigidLeftrock = leftInstantRock.GetComponent<Rigidbody>();
                                    //rigidLeftrock.velocity = leftInstantRock.transform.forward * 10;


                                    //GameObject rightInstantRock = Instantiate(rock, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 30, 0)));
                                    //Rigidbody rigidRightRock = rightInstantRock.GetComponent<Rigidbody>();
                                    //rigidRightRock.velocity = rigidRightRock.transform.forward * 10;

                                    anim.SetBool(hashRock, false);
                                    yield return new WaitForSeconds(1.0f);
                                    break;
                            }
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
                        GetComponent<BoxCollider>().enabled = false;

                    break;
                }
                yield return new WaitForSeconds(0.3f);
            }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Melee"))
        {
            if (!isDamage)
            {
                Weapon weapon = other.GetComponent<Weapon>();
                hp -= weapon.damage;
                //anim.SetTrigger(hashHit);

                if (isBoss)
                { GameManager.instance.SetBossHealth(hp); }            // 보스 현재 체력 셋팅

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                Vector3 reactVec = transform.position - other.transform.position;   //넉백

                StartCoroutine(OnDamage(reactVec));
            }
        }
        else if (other.tag.Equals("Bullet"))
        {
            Bullet enemyBullet = other.GetComponent<Bullet>();
            hp -= enemyBullet.damage;
            //anim.SetTrigger(hashHit);

            if (isBoss)
            { GameManager.instance.SetBossHealth(hp); }            // 보스 현재 체력 셋팅

            if (hp <= 0)
            {
                state = State.DIE;
            }

            Vector3 reactVec = transform.position - other.transform.position;   //넉백
            StartCoroutine(OnDamage(reactVec));
        }
        else if (other.tag.Equals("EnemyBullet"))
        {
            Bullet enemyBullet = other.GetComponent<Bullet>();
            hp -= enemyBullet.damage;
            //anim.SetTrigger(hashHit);

            if (isBoss)
            { GameManager.instance.SetBossHealth(hp); }            // 보스 현재 체력 셋팅

            if (hp <= 0)
            {
                state = State.DIE;
            }

            Vector3 reactVec = transform.position - other.transform.position;   //넉백
            StartCoroutine(OnDamage(reactVec));
        }
    }

    void OnDrawGizmos()
    {
        //추적 사정거리 표시
        if (state == State.TRACE)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        //공격 사정거리 표시
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        AudioManager.instance.PlaySFX("Hit");

        if (!isBoss)    // 기존 스크립트
        {
            isDamage = true;
            foreach (MeshRenderer mesh in meshs)         //반복문을 사용하여 모든 재질의 색상 변경
            {
                mesh.material.color = Color.red;

                Material mat = mesh.material;
                mat.SetColor("_EmissionColor", Color.red * 0.5f);
            }

            yield return new WaitForSeconds(0.5f);  //무적 타임

            isDamage = false;
            foreach (MeshRenderer mesh in meshs)
            {
                Material mat = mesh.material;
                mat.SetColor("_EmissionColor", Color.black);
            }
        
            if (hp <= 0)
            {
                foreach (MeshRenderer mesh in meshs)         //반복문을 사용하여 모든 재질의 색상 변경
                {
                    mesh.material.color = Color.gray;

                 
                }

                reactVec = reactVec.normalized;
                reactVec += Vector3.up;                             //백터 반대방향으로 설정
                rigid.AddForce(reactVec * 2f, ForceMode.Impulse);    //반대방향으로 힘이 가해진다
                yield return new WaitForSeconds(2f);

                Destroy(gameObject);

                Die();
            }
        }
        else if (isBoss)    // 보스일 경우 용용이의 렌더 색 변경을 위한 스크립트
        {
            isDamage = true;
            foreach (MeshRenderer mesh in meshs)         //반복문을 사용하여 모든 재질의 색상 변경
            {
                //mesh.material.color = Color.red;

                Material mat = mesh.material;
                mat.SetColor("_EmissionColor", Color.red * 0.5f);
            }

            Material firstMaterial = bossMesh.materials[0];
            Material secondMaterial = bossMesh.materials[1];

            Color originalColor1 = firstMaterial.color;
            Color originalColor2 = secondMaterial.color;

            firstMaterial.color = Color.red;
            secondMaterial.color = Color.red;


            yield return new WaitForSeconds(0.5f);  //무적 타임

            isDamage = false;
            foreach (MeshRenderer mesh in meshs)
            {
                Material mat = mesh.material;
                mat.SetColor("_EmissionColor", Color.black);
            }
            firstMaterial.color = originalColor1;
            secondMaterial.color = originalColor2;

            if (hp <= 0)
            {
                foreach (MeshRenderer mesh in meshs)         //반복문을 사용하여 모든 재질의 색상 변경
                {
                    mesh.material.color = Color.gray;
                }

                reactVec = reactVec.normalized;
                reactVec += Vector3.up;                             //백터 반대방향으로 설정
                rigid.AddForce(reactVec * 2f, ForceMode.Impulse);    //반대방향으로 힘이 가해진다
                yield return new WaitForSeconds(2f);

                Destroy(gameObject);

                Die();
            }
        }
    }

    // 몬스터 사망시 골드드롭
    public void Die()
    {


        //anim.SetTrigger("doDie");

        Vector3 originalPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z); // 기존 박스의 위치 저장
        Vector3 deathPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z); // 기존 박스의 위치 저장
        Quaternion originalRotation = transform.rotation; // 기존 박스의 각도 저장
        int goldValue = Random.Range(0, goldMaxValue);

        if (goldPrefab != null) // 골드 프리팹 있는 경우
        {
            for (int i = 0; i <= goldValue; i++)
            {
                GameObject newGold = Instantiate(goldPrefab, originalPosition, originalRotation);

                newGold.tag = "Item";
            }
            AudioManager.instance.PlaySFX("Puff");
            GameObject death = Instantiate(bossDeadPrefab, deathPosition, originalRotation);
            death.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // 스케일 조절
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        { transform.LookAt(playerTr.position); }
    }
}