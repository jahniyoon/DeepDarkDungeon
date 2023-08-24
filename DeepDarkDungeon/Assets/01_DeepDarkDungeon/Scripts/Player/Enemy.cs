using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour                     //중요! navmesh는 static오브젝트만 bake 가능하다!
{
    public enum Type { A, B, C, D, E };       //enum 타입 나누고
    public Type enemyType;              //그것을 지정할 변수

    public int maxHealth;
    public int curHealth;
    public Transform target;
    public BoxCollider meleeArea;          //근접 공격 범위
    public GameObject bullet;
    public GameObject magicBall;
    public bool isChase;
    public bool isAttack;                 //일반형 몬스터 변수

    public Rigidbody rigid;
    //public SphereCollider sphereCollider;
    public BoxCollider boxCollider;
    //Material mat;
    public MeshRenderer[] meshs;  //피격 이펙트를 모든 메테리얼로

    public NavMeshAgent nav;    //위에 네임스페이스 ai추가해야한다

    public Animator anim;


   

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();  // Material은 MeshRenderer로 가져와야된다
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        

        if (enemyType != Type.D)
            Invoke("ChaseStart", 2);
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Update()
    {
        if (nav.enabled && enemyType != Type.D )       //navi가 활성화되어있을때만
        {


            nav.SetDestination(target.position);     //SetDestination 도착할 목표 위치 지정 함수 
            nav.isStopped = !isChase;     //isStopped을 사용하여 완벽하게 멈추도록
        }
       

    }
    //// if(isChase)
    //    {
    //        nav.SetDestination(target.position);   이 로직은 목표만 잃어버리는거라 이동이 유지됨...
    //    }

    //일반형몬스터


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
                    targetRadius = 1.5f;
                    targetRange = 3f;
                    break;
                case Type.B:                //돌격형 몬스터
                    targetRadius = 1f;
                    targetRange = 12f;
                    break;
                case Type.C:
                    targetRadius = 0.5f;   //두께
                    targetRange = 25f;     //범위
                    break;
                case Type.E:
                    targetRadius = 0.5f;   //두께
                    targetRange = 25f;     //범위
                    break;

            }

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
            targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));    //자신의 위치, 반지름, 쏘는 방향, 레이 쏘는 거리      

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
            case Type.E:
                yield return new WaitForSeconds(0.5f);
                GameObject instantMagicBall = Instantiate(magicBall, transform.position, transform.rotation);

                


                break;

        }



        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }


    void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();

    }

    private void OnTriggerEnter(Collider other)
    {
       
        
            //if(other.tag.Equals("Player"))
            //{
            //    sphereCollider.enabled = false;
            //    Transform target = other.GetComponent<Transform>();
                
            //    findPlayer = true;
            //    Debug.Log("플레이어 위치 가져옴");


            //}
        
        if (other.tag.Equals("Melee"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;   //넉백

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
    }

    public void HitByGrenade(Vector3 explosionPos)
    {
        curHealth -= 100;                        //수류탄 터진 위치
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnDamage(reactVec, true));

    }

    IEnumerator OnDamage(Vector3 reactVec, bool isGrenade)
    {
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
        else
        {
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.gray;
            }
            gameObject.layer = 14;
            isChase = false;

            nav.enabled = false;      //사망 리액션 유지하기 위해서

            anim.SetTrigger("doDie");

            if (isGrenade)
            {
                reactVec = reactVec.normalized;
                reactVec += Vector3.up * 3;

                rigid.freezeRotation = false;     //freeze rotation x,y체크 해놔서 false로
                rigid.AddForce(reactVec * 5, ForceMode.Impulse);    //반대방향으로 힘이 가해진다
                rigid.AddTorque(reactVec * 15, ForceMode.Impulse);

            }
            else
            {
                reactVec = reactVec.normalized;
                reactVec += Vector3.up;                             //백터 반대방향으로 설정
                rigid.AddForce(reactVec * 5, ForceMode.Impulse);    //반대방향으로 힘이 가해진다
            }

            if (enemyType != Type.D)
                Destroy(gameObject, 4);
        }
    }
}
