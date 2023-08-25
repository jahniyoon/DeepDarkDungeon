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
    public string bossName;

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

        meshs = GetComponentsInChildren<MeshRenderer>();
        bossMesh = GetComponent<SkinnedMeshRenderer>();

        transform.LookAt(playerTr.position);

        if (isBoss)
        {
            GameManager.instance.SetBossMaxHealth(maxHp, bossName); // ���� �ƽ� ü�� ����
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

                    //���
                    case State.DIE:
                        isDie = true;
                        //���� ����
                        agent.isStopped = true;
                        //��� �ִϸ��̼� ����
                        anim.SetTrigger(hashDie);
                        //������ Collider ������Ʈ ��Ȱ��ȭ
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
                { GameManager.instance.SetBossHealth(hp); }            // ���� ���� ü�� ����

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                Vector3 reactVec = transform.position - other.transform.position;   //�˹�

                StartCoroutine(OnDamage(reactVec));
            }
        }
        else if (other.tag.Equals("Bullet"))
        {
            Bullet enemyBullet = other.GetComponent<Bullet>();
            hp -= enemyBullet.damage;
            //anim.SetTrigger(hashHit);

            if (isBoss)
            { GameManager.instance.SetBossHealth(hp); }            // ���� ���� ü�� ����

            if (hp <= 0)
            {
                state = State.DIE;
            }

            Vector3 reactVec = transform.position - other.transform.position;   //�˹�
            StartCoroutine(OnDamage(reactVec));
        }
        else if (other.tag.Equals("EnemyBullet"))
        {
            Bullet enemyBullet = other.GetComponent<Bullet>();
            hp -= enemyBullet.damage;
            //anim.SetTrigger(hashHit);

            if (isBoss)
            { GameManager.instance.SetBossHealth(hp); }            // ���� ���� ü�� ����

            if (hp <= 0)
            {
                state = State.DIE;
            }

            Vector3 reactVec = transform.position - other.transform.position;   //�˹�
            StartCoroutine(OnDamage(reactVec));
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

    IEnumerator OnDamage(Vector3 reactVec)
    {
        AudioManager.instance.PlaySFX("Hit");

        if (!isBoss)    // ���� ��ũ��Ʈ
        {
            isDamage = true;
            foreach (MeshRenderer mesh in meshs)         //�ݺ����� ����Ͽ� ��� ������ ���� ����
            {
                mesh.material.color = Color.red;

                Material mat = mesh.material;
                mat.SetColor("_EmissionColor", Color.red * 0.5f);
            }

            yield return new WaitForSeconds(0.5f);  //���� Ÿ��

            isDamage = false;
            foreach (MeshRenderer mesh in meshs)
            {
                Material mat = mesh.material;
                mat.SetColor("_EmissionColor", Color.black);
            }
        
            if (hp <= 0)
            {
                foreach (MeshRenderer mesh in meshs)         //�ݺ����� ����Ͽ� ��� ������ ���� ����
                {
                    mesh.material.color = Color.gray;

                 
                }

                reactVec = reactVec.normalized;
                reactVec += Vector3.up;                             //���� �ݴ�������� ����
                rigid.AddForce(reactVec * 2f, ForceMode.Impulse);    //�ݴ�������� ���� ��������
                yield return new WaitForSeconds(2f);

                Destroy(gameObject);

                Die();
            }
        }
        else if (isBoss)    // ������ ��� ������� ���� �� ������ ���� ��ũ��Ʈ
        {
            isDamage = true;
            foreach (MeshRenderer mesh in meshs)         //�ݺ����� ����Ͽ� ��� ������ ���� ����
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


            yield return new WaitForSeconds(0.5f);  //���� Ÿ��

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
                foreach (MeshRenderer mesh in meshs)         //�ݺ����� ����Ͽ� ��� ������ ���� ����
                {
                    mesh.material.color = Color.gray;
                }

                reactVec = reactVec.normalized;
                reactVec += Vector3.up;                             //���� �ݴ�������� ����
                rigid.AddForce(reactVec * 2f, ForceMode.Impulse);    //�ݴ�������� ���� ��������
                yield return new WaitForSeconds(2f);

                Destroy(gameObject);

                Die();
            }
        }
    }

    // ���� ����� �����
    public void Die()
    {


        //anim.SetTrigger("doDie");

        Vector3 originalPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z); // ���� �ڽ��� ��ġ ����
        Vector3 deathPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z); // ���� �ڽ��� ��ġ ����
        Quaternion originalRotation = transform.rotation; // ���� �ڽ��� ���� ����
        int goldValue = Random.Range(0, goldMaxValue);

        if (goldPrefab != null) // ��� ������ �ִ� ���
        {
            for (int i = 0; i <= goldValue; i++)
            {
                GameObject newGold = Instantiate(goldPrefab, originalPosition, originalRotation);

                newGold.tag = "Item";
            }
            AudioManager.instance.PlaySFX("Puff");
            GameObject death = Instantiate(bossDeadPrefab, deathPosition, originalRotation);
            death.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // ������ ����
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        { transform.LookAt(playerTr.position); }
    }
}