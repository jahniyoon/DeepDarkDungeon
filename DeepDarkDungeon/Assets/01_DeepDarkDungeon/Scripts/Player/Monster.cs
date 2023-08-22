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

    public enum Type { A, B, C }
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
    public Transform playerTarget;
    public GameObject bullet;


    // Animator 파라미터의 해시값 추출
    public readonly int hashTrace = Animator.StringToHash("IsTrace");
    public readonly int hashAttack = Animator.StringToHash("IsAttack");
    public readonly int hashHit = Animator.StringToHash("Hit");
    public readonly int hashDie = Animator.StringToHash("Die");

    public int maxHp = 100;
    public int hp = 100;
    public string name;

    public bool isDamage;

    public int damage;



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

<<<<<<< HEAD
        transform.LookAt(playerTr.position);
=======
        GameManager.instance.SetBossMaxHealth(maxHp, name); // 보스 맥스 체력 셋팅
        GameManager.instance.SetBossHealth(hp);              // 보스 현재 체력 셋팅
>>>>>>> origin/feature/Jihwan

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
                state = State.TRACE;
            }
            else
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
                              
                                 yield return new WaitForSeconds(0.5f);
                                 GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                                 Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                                 rigidBullet.velocity = transform.forward * 3;

                                GameObject leftInstantBullet = Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -15, 0)));
                                Rigidbody rigidLeftBullet = leftInstantBullet.GetComponent<Rigidbody>();
                                rigidLeftBullet.velocity = leftInstantBullet.transform.forward * 3;

                                // 오른쪽 불렛 발사
                                GameObject rightInstantBullet = Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 15, 0)));
                                Rigidbody rigidRightBullet = rightInstantBullet.GetComponent<Rigidbody>();
                                rigidRightBullet.velocity = rightInstantBullet.transform.forward * 3;

                                yield return new WaitForSeconds(2f);
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Melee"))
        {
            if (!isDamage)
            {
                Weapon weapon = other.GetComponent<Weapon>();
                hp -= weapon.damage;
                anim.SetTrigger(hashHit);

                GameManager.instance.SetBossHealth(hp);              // 보스 현재 체력 셋팅

                if (hp < 0)
                {
                    state = State.DIE;
                    GameManager.instance.BossDead();

                }

                StartCoroutine(OnDamage());
            }

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

    IEnumerator OnDamage()
    {
        isDamage = true;
        foreach (MeshRenderer mesh in meshs)         //반복문을 사용하여 모든 재질의 색상 변경
        {
            //mesh.material.color = Color.red;
            Material mat = mesh.material;
            mat.SetColor("_EmissionColor", Color.gray * 0.5f);
        }
        yield return new WaitForSeconds(0.5f);  //무적 타임

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            Material mat = mesh.material;
            mat.SetColor("_EmissionColor", Color.black);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}