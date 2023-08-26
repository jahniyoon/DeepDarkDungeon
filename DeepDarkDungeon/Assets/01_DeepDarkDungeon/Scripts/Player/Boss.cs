using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Boss : Monster
{
    public GameObject tornado;
    public Transform tornadoA;



    public bool isLook;

    Vector3 lookVec;

    
    public readonly new int hashAttack1 = Animator.StringToHash("AttackPattern1");
    public readonly int hashAttack2 = Animator.StringToHash("AttackPattern2");
    public readonly int hashAttack3 = Animator.StringToHash("AttackPattern3");
    public readonly int hashAttack4 = Animator.StringToHash("AttackPattern4");

    // Start is called before the first frame update
    void Awake()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        meshs = GetComponents<MeshRenderer>();

    }
 

    public override IEnumerator MonsterAction()
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
                    anim.SetBool(hashAttack1, false);
                    anim.SetBool(hashAttack2, false);
                    anim.SetBool(hashAttack3, false);
                    break;

                //공격 상태
                case State.ATTACK:

                    int attackPattern = Random.Range(1, 5);


                    // 선택한 패턴에 따라 동작 실행
                    switch (attackPattern)
                    {
                        case 1:                     
                            anim.SetBool(hashAttack1, true);
                            yield return new WaitForSeconds(1.0f);
                            anim.SetBool(hashAttack1, false);
                            break;
                        case 2:
                            anim.SetBool(hashAttack2, true);
                            yield return new WaitForSeconds(1.0f);
                            anim.SetBool(hashAttack2, false);
                            break;
                        case 3:                     // 브레스
                            anim.SetBool(hashAttack3, true);
                            yield return new WaitForSeconds(3.5f);
                            anim.SetBool(hashAttack3, false);
                            break;
                        case 4:                     // 토네이도                
                            anim.SetBool(hashAttack4, true);
                       
                            yield return new WaitForSeconds(0.5f);     //애니메이션 60프레임 1초니까 0.5 30프레임
                            GameObject instantTornado = Instantiate(tornado, tornadoA.position, tornadoA.rotation);
                            BossBullet bulletTornadoA = instantTornado.GetComponent<BossBullet>();     
                            bulletTornadoA.target = playerTarget;
                            yield return new WaitForSeconds(0.5f);

                            anim.SetBool(hashAttack4, false);
                            yield return new WaitForSeconds(1.0f);
                            break;
                           


                    }
                    break;

                //사망
                case State.DIE:
                    isDie = true;
                    //추적 중지
                    agent.isStopped = true;
                    //사망 애니메이션 실행
                    //anim.SetTrigger(hashDie);
                    //몬스터의 Collider 컴포넌트 비활성화
                    GetComponent<BoxCollider>().enabled = false;

                    GameManager.instance.BossDead();


                    //foreach (MeshRenderer mesh in meshs)
                    //{
                    //    mesh.material.color = Color.gray;
                    //}
               
                    anim.SetTrigger("Die");



                    Material firstMaterial = bossMesh.materials[0];
                    Material secondMaterial = bossMesh.materials[1];
                    firstMaterial.color = new Color (0.1f,0.1f,0.1f,0.5f);
                    secondMaterial.color = new Color(0.1f, 0.1f, 0.1f, 0.5f);
                    agent.speed = 0;

                    Invoke("Die", 3.5f);

                    //Destroy(gameObject);
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
       

        if (isDie)
        {
            StopAllCoroutines();
            return;
        }
        if (isLook && playerTarget != null)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 0.25f;
            transform.LookAt(playerTr.position + lookVec);
        }
        else
        {
            //agent.SetDestination(tauntVec);
           
            transform.LookAt(playerTr.position );
        }


    }
    void Die()
    {
        Vector3 keyPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z); // 열쇠 드롭 위치
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
