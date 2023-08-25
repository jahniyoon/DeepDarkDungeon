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

    //������ ���� ����
    public State state = State.IDLE;
    //���� �����Ÿ�
    public float traceDist = 10.0f;
    //���� �����Ÿ�
    public float attackDist = 1.0f;
    //������ ��� ����
    public bool isDie = false;

    //������Ʈ ĳ�ø� ó���� ����
    public Transform monsterTr;
    public Transform playerTr;
    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public NavMeshAgent agent;
    public Animator anim;
    public Transform target;

    public MeshRenderer[] meshs;
    public SkinnedMeshRenderer bossMesh;

    public Transform playerTarget;
    public GameObject monsterPrefab;
    public Transform spawnA;
    public Transform spawnB;
    public Transform spawnC;
    public GameObject bullet;
    public GameObject rock;
    //public GameObject monsterPrefab;

    [Header("Drop Item")]
    public GameObject exitKeyPrefab;
    public GameObject bossDeadPrefab;



    public float intensityFactor = 2f;


    // Animator �Ķ������ �ؽð� ����
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
    public string name;

    public bool isDamage;

    public int damage;
    bool traceStart; // ���� ���۱��. 



    // Start is called before the first frame update
    void Start()
    {
        //������ Transform �Ҵ�
        monsterTr = GetComponent<Transform>();

        //���� ����� Player�� Transform �Ҵ�
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        rigid = GetComponent<Rigidbody>();

        boxCollider = GetComponent<BoxCollider>();

        //NavMeshAgent ������Ʈ �Ҵ�
        agent = GetComponent<NavMeshAgent>();

        //Animator ������Ʈ �Ҵ�
        anim = GetComponent<Animator>();

        bossMesh = GetComponent<SkinnedMeshRenderer>();

        transform.LookAt(playerTr.position);

        meshs = GetComponentsInChildren<MeshRenderer>();

        if (isBoss)
        {
            GameManager.instance.SetBossMaxHealth(maxHp, name); // ���� �ƽ� ü�� ����
            GameManager.instance.SetBossHealth(hp);              // ���� ���� ü�� ����
        }
        //������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(CheckMonsterState());
        //���¿� ���� ������ �ൿ�� �����ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(MonsterAction());
    }


    //������ �������� ������ �ൿ ���¸� üũ
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            //0.3�� ���� ����(���)�ϴ� ���� ������� �޽��� ������ �纸
            yield return new WaitForSeconds(0.3f);

            //������ ���°� Die�� �� �ڷ�ƾ�� ����
            if (state == State.DIE) yield break;

            //���Ϳ� ���ΰ� ĳ���� ������ �Ÿ� ����
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            //���� �����Ÿ� ������ ���Դ��� Ȯ��
            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            //else if (distance >= traceDist)     //���� �����Ÿ� �ܺο��� ���� ����
            //{
            //    state = State.TRACE;
            //}
            else if (distance <= traceDist)     //���� �����Ÿ� ���ο��� ���� ����
            {
                traceStart = true;              // ��ȯ : ���� �����Ÿ��� ������ ��� �÷��̾ ����
                state = State.TRACE;
            }
            else if (!traceStart)               // ��ȯ : ���� �����Ÿ��� ������ ��� �÷��̾ ����
            {
                state = State.IDLE;
            }
        }
    }

    //������ ���¿� ���� ������ ������ ����
    public virtual IEnumerator MonsterAction()
    {
            while (!isDie)
            {
                switch (state)
                {
                    //IDLE ����
                    case State.IDLE:
                        //���� ����
                        agent.isStopped = true;

                        // Animator�� IsTrace ������ false�� ����
                        anim.SetBool(hashTrace, false);
                        break;

                    //���� ����
                    case State.TRACE:
                        //���� ����� ��ǥ�� �̵� ����
                        agent.SetDestination(playerTr.position);
                        agent.isStopped = false;

                        // Animator�� IsTrace ������ true�� ����
                        anim.SetBool(hashTrace, true);

                        // Animator�� IsAttack ������ false�� ����
                        anim.SetBool(hashAttack, false);
                        break;

                    //���� ����
                    case State.ATTACK:
                        // Animator�� IsAttack ������ true�� ����
                        //anim.SetBool(hashAttack, true);

                        switch(monsterType)
                        {
                            case Type.A:
                                anim.SetBool(hashAttack, true);
                                break;

                            case Type.B:

                            anim.SetBool(hashAttack, true);
                            yield return new WaitForSeconds(0.65f);
                                 GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                                 Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                                 rigidBullet.velocity = transform.forward * 3;

                                GameObject leftInstantBullet = Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -15, 0)));
                                Rigidbody rigidLeftBullet = leftInstantBullet.GetComponent<Rigidbody>();
                                rigidLeftBullet.velocity = leftInstantBullet.transform.forward * 3;

                                
                                GameObject rightInstantBullet = Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 15, 0)));
                                Rigidbody rigidRightBullet = rightInstantBullet.GetComponent<Rigidbody>();
                                rigidRightBullet.velocity = rightInstantBullet.transform.forward * 3;

                                yield return new WaitForSeconds(0.15f);                           
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
                                    yield return new WaitForSeconds(0.8f);

                                    GameObject instantSpawnA = Instantiate(monsterPrefab, spawnA.position, spawnA.rotation);
                                    GameObject instantSpawnB = Instantiate(monsterPrefab, spawnB.position, spawnB.rotation);
                                    GameObject instantSpawnC = Instantiate(monsterPrefab, spawnC.position, spawnC.rotation);
                                    anim.SetBool(hashSpawn, false);

                                    yield return new WaitForSeconds(0.1f);
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
                                    
                                    GameObject instantRock = Instantiate(rock, transform.position, transform.rotation);
                                    Rigidbody rigidRock = instantRock.GetComponent<Rigidbody>();
                                    rigidRock.velocity = transform.forward * 3;

                                    GameObject leftInstantRock = Instantiate(rock, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -30, 0)));
                                    Rigidbody rigidLeftrock = leftInstantRock.GetComponent<Rigidbody>();
                                    rigidLeftrock.velocity = leftInstantRock.transform.forward * 3;


                                    GameObject rightInstantRock = Instantiate(rock, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 30, 0)));
                                    Rigidbody rigidRightRock = rightInstantRock.GetComponent<Rigidbody>();
                                    rigidRightRock.velocity = rigidRightRock.transform.forward * 3;

                                    anim.SetBool(hashRock, false);
                                    yield return new WaitForSeconds(1.0f);
                                    break;
                            }
                            break;
                        }
                        break;

                    //���
                    case State.DIE:
                        isDie = true;
                        //���� ����
                        agent.isStopped = true;
                        //��� �ִϸ��̼� ����
                        anim.SetTrigger(hashDie);
                        //������ Collider ������Ʈ ��Ȱ��ȭ
                        GetComponent<BoxCollider>().enabled = false;
                    Destroy(gameObject);
                        break;
                }
                yield return new WaitForSeconds(0.3f);
            }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Melee"))
        {

            Weapon weapon = other.GetComponent<Weapon>();
            hp -= weapon.damage;
            //anim.SetTrigger(hashHit);

            //Debug.Log("���� hp: " + hp);

            if (isBoss)
            { GameManager.instance.SetBossHealth(hp); }            // ���� ���� ü�� ����

            if (hp <= 0)
            {
                state = State.DIE;
            }

            StartCoroutine(OnDamage());

        }
    }

    void OnDrawGizmos()
    {
        //���� �����Ÿ� ǥ��
        if (state == State.TRACE)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        //���� �����Ÿ� ǥ��
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }

    IEnumerator OnDamage()
    {
        if(hp > 0)
        {
            //Debug.Log("���� ��ȭ");

            foreach (MeshRenderer mesh in meshs)         //�ݺ����� ����Ͽ� ��� ������ ���� ����
            {
                //Material mat = mesh.material;

                //// ���� Emission intensity ���� ������
                //Color originalEmissionColor = mat.GetColor("_EmissionColor");
                //float originalIntensity = (originalEmissionColor.r + originalEmissionColor.g + originalEmissionColor.b) / 3f;

                //// Intensity�� 2��� �������� ���ο� Emission intensity ���� ����
                //float newIntensity = originalIntensity * 2f;

                //// ���ο� Emission intensity ���� �������� �����Ͽ� Emission ������ ����
                //Color newEmissionColor = new Color(newIntensity, newIntensity, newIntensity);
                //mat.SetColor("_EmissionColor", newEmissionColor);

                //// Emission Ȱ��ȭ
                //mat.EnableKeyword("_EMISSION");

                //// ���� ���� ����
                //mesh.material = mat;


                //Renderer renderer = mesh.GetComponent<Renderer>();
                //Material mat = renderer.material;

                //float emission = Mathf.PingPong(Time.time, 1.0f);
                //Color baseColor = Color.yellow;
                //Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

                //mat.SetColor("_EmissionColor", Color.red * 2.0f);

                Material mat = mesh.material;                          //���� �ڵ�
                mat.SetColor("_EmissionColor", Color.red * 2f);

                mat.EnableKeyword("_EMISSION");

                mesh.material = mat;

                //mat.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 2f);
            }
          
        }
        
        //Material firstMaterial = bossMesh.materials[0];
        //Material secondMaterial = bossMesh.materials[1];

        //Color originalColor1 = firstMaterial.color;
        //Color originalColor2 = secondMaterial.color;

        //firstMaterial.color = Color.red;
        //secondMaterial.color = Color.red;



        yield return new WaitForSeconds(0.5f);  //���� Ÿ��

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {

            Material mat = mesh.material;                      //���� ����ϴ�
            mat.SetColor("_EmissionColor", Color.black);
        }
        //firstMaterial.color = originalColor1;
        //secondMaterial.color = originalColor2;

        if (hp <= 0)
        {
            foreach (MeshRenderer mesh in meshs)         //�ݺ����� ����Ͽ� ��� ������ ���� ����
            {
                mesh.material.color = Color.gray;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTr.position);
    }
}