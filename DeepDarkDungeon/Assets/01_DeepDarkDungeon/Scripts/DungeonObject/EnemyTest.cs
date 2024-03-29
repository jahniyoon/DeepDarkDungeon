using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyTest : MonoBehaviour                     //중요! navmesh는 static오브젝트만 bake 가능하다!
{
    public enum Type { A, B, C, D, Skeletone};       //enum 타입 나누고
    public Type enemyType;              //그것을 지정할 변수

    public int maxHealth;
    public int curHealth;
    public Transform target;
    public BoxCollider meleeArea;          //근접 공격 범위
    public GameObject bullet;
    public bool isChase;
    public bool isAttack;
    public bool isDead;
    public bool isDummy;
    bool startChase = false;
    

    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public SphereCollider scanCollider;
    //Material mat;
    public MeshRenderer[] meshs;  //피격 이펙트를 모든 메테리얼로

    public NavMeshAgent nav;    //위에 네임스페이스 ai추가해야한다

    public Animator anim;

    public bool doLook;

    Vector3 doLookVec;

    [Header("Drop Gold")] // 몬스터 사망 시 골드 드롭

    public GameObject goldPrefab;   // 골드 프리팹
    public int goldMaxValue; // 최대 골드 값
    public GameObject deathPrefab;   // 죽음 프리팹
    //GameObject player;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        scanCollider = GetComponent<SphereCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();  // Material은 MeshRenderer로 가져와야된다
        if (nav != null)
        { nav = GetComponent<NavMeshAgent>(); }
        anim = GetComponentInChildren<Animator>();

        //player = GameObject.FindWithTag("Player");

        //if (enemyType != Type.D)
        //    Invoke("ChaseStart", 2);  // 플레이어 만나면 시작되게 하기위해 뺐습니다.
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }


    void Update()
    {
        //transform.LookAt(player.transform);

        if (target != null && !startChase && !isDummy) //  플레이어를 감지하면 ChaseStart              //플레이어 타켓 관련 
        {
            startChase = true;
            ChaseStart();
        }


        if (nav != null &&  nav.enabled &&enemyType != Type.D && target != null)       //navi가 활성화되어있을때만     //타켓 
        {
            nav.SetDestination(target.position);     //SetDestination 도착할 목표 위치 지정 함수 
            nav.isStopped = !isChase;     //isStopped을 사용하여 완벽하게 멈추도록

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

    //일반형 몬스터
    void Targeting()
    {
        if (enemyType != Type.D)
        {
            float targetRadius = 0;
            float targetRange = 0;                   //sphererCast의 반지름, 길이를 조정 변수

            switch (enemyType)
            {
                case Type.A:
                    targetRadius = 0.3f; //공격하는 거리
                    targetRange = 0.8f;
                    break;
                case Type.B:                //돌격형 몬스터
                    targetRadius = 1f;
                    targetRange = 12f;
                    break;
                case Type.C:
                    targetRadius = 0.5f;   //두께
                    targetRange = 25f;     //범위
                    break;
                case Type.Skeletone:
                    targetRadius = 0.15f;   //두께
                    targetRange = 0.15f;     //범위
                    break;
            }

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
            targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));    //자신의 위치, 반지름, 쏘는 방향, 레이 쏘는 거리      

            Debug.DrawRay(transform.position, transform.forward, Color.red);


            //rayHit 변수에 데이터가 들어오면 공격 코루틴 실행한다
            if (rayHits.Length > 0 && !isAttack)  //범위 안이고 공격중이면 공격하지 않는다
            {
                StartCoroutine(Attack());
            }
        }

    }

    IEnumerator Attack()
    {
        //먼저 정지를 한 다음, 애니메이션과 함께 공격범위 활성화
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);




        switch (enemyType)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;  //공격범위 활성화

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;  //공격범위 비활성화

                yield return new WaitForSeconds(1f);
                break;
            case Type.B:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse);          //AddForce 돌격 구현
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                yield return new WaitForSeconds(2f);  //돌격했으니 2초간 쉰다
                break;
            case Type.C:
                yield return new WaitForSeconds(0.5f);
                GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 20;

                yield return new WaitForSeconds(2f);
                break;
            case Type.Skeletone:


                yield return new WaitForSeconds(1f);    // 공격 애니메이션 속도 대기
                anim.SetBool("isAttack", false);
                yield return new WaitForSeconds(1f);    // 공격 쿨타임

                break;
        }



        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }


    void FixedUpdate()
    {
        if (!isDummy)
        {
            Targeting();
            FreezeVelocity();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Melee"))
        {
            Weapon weapon = other.GetComponent<Weapon>();

            float totalDamage = weapon.damage * GameManager.instance.power; // 데미지에 파워를 곱한 결과 (float)
            int finalDamage = Mathf.FloorToInt(totalDamage); // 내림하여 int로 변환

            curHealth -= finalDamage;
            Vector3 reactVec = (transform.position - other.transform.position);   //넉백

            StartCoroutine(OnDamage(reactVec, false));
        }
        else if (other.tag.Equals("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);

            StartCoroutine(OnDamage(reactVec, false));
        }
        if (other.tag.Equals("EnemyBullet"))
        {

            Bullet enemyBullet = other.GetComponent<Bullet>();
            curHealth -= enemyBullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;   //넉백
            StartCoroutine(OnDamage(reactVec, false));


        }
    }

    IEnumerator OnDamage(Vector3 reactVec, bool isGrenade)
    {
        boxCollider.enabled = false; //콜라이더 활성화
                                     //Debug.Log("적 한대 맞았다. 히트박스 비활성화");
        AudioManager.instance.PlaySFX("Hit");


        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }
        }

        // 몬스터 죽음 시
        else
        {
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.gray;
            }
            isChase = false;
            target = null;

            //nav.enabled = false;      //사망 리액션 유지하기 위해서
            
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;                             //백터 반대방향으로 설정
            rigid.AddForce(reactVec * 2f, ForceMode.Impulse);    //반대방향으로 힘이 가해진다
            yield return new WaitForSeconds(0.5f);
            AudioManager.instance.PlaySFX("Puff");

            Destroy(gameObject);
            Die();

        }
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = true; //콜라이더 활성화
        //Debug.Log("적 히트박스 활성화");


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
                GameObject death = Instantiate(deathPrefab, deathPosition, originalRotation);
                death.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // 스케일 조절

                newGold.tag = "Item";
            }
        }
    }

}
