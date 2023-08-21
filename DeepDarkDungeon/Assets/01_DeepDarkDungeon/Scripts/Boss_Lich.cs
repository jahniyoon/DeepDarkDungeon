using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Lich : MonoBehaviour
{
    Animator animator;
    bool isDamage;
    bool isDead;
    bool isChase;
    bool isAttack;
    bool startChase;

    public int bossMaxHealth;
    public int bossCurHealth;
    public string bossName;
    
    // Ÿ��
    public SphereCollider scanCollider;
    public Transform target;
    public NavMeshAgent nav;

    public BoxCollider meleeArea;


    public GameObject exitKeyPrefab;
    public GameObject bossDeadPrefab;

    void Awake()
    {
        scanCollider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();

        if (nav != null)
        { nav = GetComponent<NavMeshAgent>(); }
    }


    void Start()
    {
        GameManager.instance.SetBossMaxHealth(bossMaxHealth, bossName); // ���� �ƽ� ü�� ����
        GameManager.instance.SetBossHealth(bossCurHealth);              // ���� ���� ü�� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !startChase && GameManager.instance.isBoss) //  �÷��̾ �����ϸ� ChaseStart           
        {
            startChase = true;
            ChaseStart();
        }
        if (nav != null && nav.enabled && target != null)       //navi�� Ȱ��ȭ�Ǿ���������     //Ÿ�� 
        {
            nav.SetDestination(target.position);     //SetDestination ������ ��ǥ ��ġ ���� �Լ� 
            nav.isStopped = !isChase;                //isStopped�� ����Ͽ� �Ϻ��ϰ� ���ߵ���

        }

    }
    void ChaseStart()
    {
        isChase = true;
        //animator.SetBool("isWalk", true);
    }
    void Targeting()
    {
            float targetRadius = 3.0f;
            float targetRange = 0.8f;                   //sphererCast�� ������, ���̸� ���� ����

            

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
            targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));    //�ڽ��� ��ġ, ������, ��� ����, ���� ��� �Ÿ�      

            Debug.DrawRay(transform.position, transform.forward, Color.red);


            //rayHit ������ �����Ͱ� ������ ���� �ڷ�ƾ �����Ѵ�
            if (rayHits.Length > 0 && !isAttack)  //���� ���̰� �������̸� �������� �ʴ´�
            {
                StartCoroutine(MeleeAttack());
            }


    }
    IEnumerator MeleeAttack()
    {
        //���� ������ �� ����, �ִϸ��̼ǰ� �Բ� ���ݹ��� Ȱ��ȭ
        isChase = false;
        isAttack = true;
        //anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true;  //���ݹ��� Ȱ��ȭ

        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;  //���ݹ��� ��Ȱ��ȭ

        yield return new WaitForSeconds(1f);
           
        isChase = true;
        isAttack = false;
        //anim.SetBool("isAttack", false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Melee"))
        {
            if (!isDamage)
            {
                Weapon weapon = other.GetComponent<Weapon>();
                //������ hp ����
                BossDamage(weapon.damage);

                if (bossCurHealth <= 0 && !isDead)
                {
                    isDead = true;
                    GameManager.instance.BossDead();
                    StartCoroutine("Death");
                }
                //StartCoroutine(OnDamage());
            }
        }
    }
    // ���� ������ �Ծ��� ��
    void BossDamage(int damage)
    {
        animator.SetTrigger("isDamage");
        bossCurHealth -= damage;
        GameManager.instance.SetBossHealth(bossCurHealth);
    }
    // ���� �׾��� �� �ִϸ��̼� ���� ���� ���
    IEnumerator Death() 
    {
        animator.SetTrigger("isDead");

        yield return new WaitForSeconds(2f);

        Vector3 keyPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z); // ���� ��� ��ġ
        Vector3 originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z); // ���� �ִϸ��̼� ��ġ
        Quaternion originalRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90f, 1); // ���� ��� �� ���� ����
        if (exitKeyPrefab != null)
        {
            GameObject key = Instantiate(exitKeyPrefab, keyPosition, originalRotation);
            GameObject deathEffect = Instantiate(bossDeadPrefab, originalPosition, originalRotation);   // ���� ���� ����Ʈ
            key.tag = "Item";
        }
        Destroy(gameObject);
    }
   
}

//public GameObject missile;
//public Transform missilePortA;
//public Transform missilePortB;
//public bool isLook;      //�ٶ󺸴�

//Vector3 lookVec;     //�÷��̾� ������ ���� ���� ����
//Vector3 tauntVec;


//// Start is called before the first frame update
//void Awake()   //awake�Լ��� �ڽ� ��ũ��Ʈ�� �ܵ� ���� enemy awke��ũ��Ʈ ��� �ȵ�
//{
//    rigid = GetComponent<Rigidbody>();
//    boxCollider = GetComponent<BoxCollider>();
//    meshs = GetComponentsInChildren<MeshRenderer>();  // Material�� MeshRenderer�� �����;ߵȴ�
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
//        StopAllCoroutines();           //�׾��� �� ����  �ڷ�ƾ ���� ����
//        return;                        //�������� �Ʒ� ���� ���� ���ϰ� ����
//    }
//    if (isLook)
//    {
//        float h = Input.GetAxisRaw("Horizontal");
//        float v = Input.GetAxisRaw("Vertical");
//        lookVec = new Vector3(h, 0, v) * 5f;   //�÷��̾� �Է°����� ���� ���Ͱ� ����
//        transform.LookAt(target.position + lookVec);
//    }
//    else
//    {
//        nav.SetDestination(tauntVec);    //���������� �� ��ǥ���� �̵�
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
//            //�� �������� ����
//            break;
//        case 4:
//            StartCoroutine(Taunt());
//            //���� ���� ����
//            break;

//    }

//}

//IEnumerator MissileShot()
//{
//    anim.SetTrigger("doShot");
//    yield return new WaitForSeconds(0.2f);
//    GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
//    BossMissile bossMissileA = instantMissileA.GetComponent<BossMissile>();     //�̻��� ��ũ��Ʈ���� �����Ͽ� ��ǥ�� ����(����)
//    bossMissileA.target = target;

//    yield return new WaitForSeconds(0.3f);
//    GameObject instantMissileB = Instantiate(missile, missilePortA.position, missilePortA.rotation);
//    BossMissile bossMissileB = instantMissileA.GetComponent<BossMissile>();     //�̻��� ��ũ��Ʈ���� �����Ͽ� ��ǥ�� ����(����)
//    bossMissileB.target = target;

//    yield return new WaitForSeconds(2f);

//    StartCoroutine(Think());
//}

//IEnumerator RockShot()
//{
//    isLook = false;      //�� ���� ���� �ٶ󺸱� �����ϵ��� �÷��� ����
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
//    boxCollider.enabled = false;   //�ڽ��ݶ��̴��� �÷��̾ ���� �ʵ��� ��Ȱ��ȭ
//    anim.SetTrigger("doTaunt");

//    yield return new WaitForSeconds(1.5f);
//    meleeArea.enabled = true;     //1.5�� �� ���� ���� �ݶ��̴� Ȱ��ȭ

//    yield return new WaitForSeconds(0.5f);
//    meleeArea.enabled = false;         //�������� �Ķ� �������´�

//    yield return new WaitForSeconds(1f);
//    isLook = true;
//    nav.isStopped = true;
//    boxCollider.enabled = true;


//    StartCoroutine(Think());
//}
