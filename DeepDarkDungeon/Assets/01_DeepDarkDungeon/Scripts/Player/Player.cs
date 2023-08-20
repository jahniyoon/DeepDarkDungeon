using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum WeaponType { Sword, ChainSaw, TwohandSword };
    WeaponType type;

    Rigidbody PlayerRigid;
    Animator animator;
    MeshRenderer[] meshs;

    public Transform playerTransform;
    //public GameObject followCamera;
    public GameObject smoke;
    [Header("Player")]
    public int maxHealth;
    public int curHealth;

    public int maxGold;
    public int curGold;

    public float distance;
    public float speed;
    public float turnSpeed;
    public float attackDistance;

    public GameObject[] weapons;  //�÷��̾� ���� ���� �Լ� - ���� �����ϴ� 

    public bool hasWeapon1;     //�÷��̾� ���� ���� �Լ� - ���� �κ��丮 �����
    public bool hasWeapon2;     
    public bool hasWeapon3;  
    // ������ ������ ���Կ� ����
    public int slot1;
    public int slot2;
    public int slot3;

    int equipWeaponIndex = -1; //���� �ߺ� ��ü, ���� ���� Ȯ���� ���� ����
    Weapon equipWeapon;         //������ ������ ���⸦ �����ϴ� ������ ����


    // �÷��̾� ��ǲ
    float hAxis;
    float vAxis;

    bool wDown;
    bool jDown;
    bool iDown;
    bool fDown;   // ����

    // �÷��̾� �̵�
    Vector3 moveVec;
    Vector3 dodgeVec;  // ȸ�� ������ȯ x

    //���� ���� 
    bool sDown1;
    bool sDown2;
    bool sDown3;

  

    // �÷��̾� ���� üũ
    bool isSwap;                // ������ ���� �� üũ
    bool isFireReady = true;    // ���� ������ üũ
    bool isBorder;              // �̵� �� ���� �� üũ
    bool isDamage;              // ������ �������� üũ
    bool isDodge;               // ���������� üũ
    bool isDie;                 // �׾����� üũ
    bool isAttack;

    bool isBossRoom;            // ���������� üũ

    bool pauseDown;             // ���� ���� Ȯ��
    float fireDelay;

    // �÷��̾� ��ȣ�ۿ� ���ӿ�����Ʈ
    GameObject bossRoomDoor;
    GameObject nearObject;      // ����� ������Ʈ
    GameObject exit;            // �ⱸ ������Ʈ
    GameObject exitUI;          // �ⱸ UI
    GameObject chest;           // ��������
    GameObject chestUI;         // �������� UI
    GameObject sellItem;        // �Ǹ� ������
    GameObject sellItemUI;      // �Ǹ� ������ UI

    // Start is called before the first frame update
    void Start()
    {

        PlayerRigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();     //�ڽ� ������Ʈ�� �ִϸ��̼� �־ 
        meshs = GetComponentsInChildren<MeshRenderer>();

        if (GameData.loadEnable)    //  �ҷ����Ⱑ �����ϸ� ������ �ε� (������ Ŭ���� �ؾ� loadEnable = true)
        {
            LoadData();
        }
        //GameManager.instance.SaveData(maxHealth, curHealth, maxGold, curGold);  // �÷��̾� ������ ����
        GameManager.instance.Initialization();                                  // ���� �ʱ�ȭ
        GameManager.instance.SetMaxHealth(maxHealth); // ü�� �ʱ�ȭ
        GameManager.instance.SetHealth(curHealth); // ü�� �ʱ�ȭ
        GameManager.instance.SetGold(curGold);
        GameManager.instance.SetFloor();
        SetWeapon();

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Pause();

        if (!GameManager.instance.isGameOver && !GameManager.instance.isPause)
        {
            Attack();
            Dodge();
            Swap();
            Interation();
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isPause && !isAttack)
        {
            Move();
            Turn();
        }
        FreezeRotation();
        StopToWall();
    }

    // �Է�
    void GetInput()        
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        wDown = Input.GetButton("Walk");       //shift ���� �� �۵�
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire1");

        iDown = Input.GetButtonDown("Interation");

        //���� ����
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");

        pauseDown = Input.GetButtonDown("Cancel");
    }

    void Move()     //ĳ���� ������
    {
        moveVec = new Vector3(hAxis, 0, vAxis);


        if (isDodge)
            moveVec = dodgeVec;


        if (isSwap)   //�������϶��� ������ �� ����   //���⿡ isBorder������ Ű���忡 ���� ȸ�� ���� �����
        {
            moveVec = Vector3.zero;
        }
        if (!isBorder)
        {
            //transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;     //���׿����� Ʈ��� 0.3 �ƴϸ� 1
            PlayerRigid.MovePosition(transform.position + transform.forward * moveVec.normalized.magnitude * speed * Time.deltaTime);

        }


        animator.SetBool("isRun", moveVec != Vector3.zero);
        animator.SetBool("isWalk", wDown);
    }
    
    void Turn()  //ĳ���� ȸ��
    {
        if (moveVec == Vector3.zero) return;

        var rot = Quaternion.LookRotation(moveVec.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
            //transform.LookAt(transform.position + moveVec);   
    }

    void Attack()
    {
        if (equipWeapon == null)      //���⸸ ���� �� ����ǵ��� ������� üũ
        {
            return;
        }

        fireDelay += Time.deltaTime;    //���ݵ����̿� �ð��� �����ְ� ���ݰ��� ���θ� Ȯ��
        isFireReady = equipWeapon.rate < fireDelay;
     

        if (fDown && isFireReady && !isDodge && !isSwap)
        {
            equipWeapon.Use();                   //public �����ؼ� �ٸ� ��ũ��Ʈ�� �ִ� �� �����ü��ִ�

            isAttack = true;

            Debug.Log("������ �ߴ�. ������");

            switch (equipWeapon.weaponType)  // ���� Ÿ�Կ� ���� ����ġ�� �ٸ� �ִϸ��̼� Ʈ���� ����
            {
                case Weapon.WeaponType.Sword:
                    animator.SetTrigger("doAttackSword");
                    break;
                case Weapon.WeaponType.ChainSaw:
                    animator.SetTrigger("doAttackChainSaw");
                    break;
                case Weapon.WeaponType.TwohandSword:
                    animator.SetTrigger("doAttackTwohandSword");
                    break;
            }
            //animator.SetTrigger(equipWeapon.weaponType == Weapon.WeaponType.Melee ? "doSwing" : "doShot");   //���� Ÿ�Կ� ���� �ٸ� Ʈ���� ���� 

            fireDelay = 0;  //���� ������ 0���� �÷��� ���� ���ݱ��� ��ٸ����� �ۼ�
            StopCoroutine("AttackDelay");
            StartCoroutine("AttackDelay");


        }
    }
    IEnumerator AttackDelay()  // ���� ������
    {
        yield return new WaitForSeconds(equipWeapon.rate - 0.5f);
        isAttack = false;

    }



    void Dodge()
    {
        if(jDown && moveVec != Vector3.zero && !isDodge)
        {
            isAttack = false;   // ���� ĵ��
            dodgeVec = moveVec; //ȸ���ϸ鼭 ���� ��ȯ x
            
            speed *= 2f;
            animator.SetTrigger("doDodge");
            isDodge = true;
            smoke.gameObject.SetActive(true);   // ������ ����


            Invoke("DodgeOut", 0.1f);
        }
        
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        Invoke("DodgeStop", 0.6f);
    }

    void DodgeStop() //ȸ�� ���� �� �̵����� - ȸ�� �߿��� ���� ��ȯ �ȵǴ� 
    {
        isDodge = false;
        smoke.gameObject.SetActive(false);   // ������ ����

    }
    // ���� Pause
    void Pause()
    {
        if (pauseDown)
        {
            GameManager.instance.OnGamePause();
        }
    }

    void Swap()
    {
        if (sDown1 && (!hasWeapon1 || equipWeaponIndex == slot1))
        {
            Debug.Log("1 ���� �Ұ�");

            return;
        }
        if (sDown2 && (!hasWeapon2 || equipWeaponIndex == slot2))
        {
            Debug.Log("2 ���� �Ұ�");

            return;
        }
        if (sDown3 && (!hasWeapon3 || equipWeaponIndex == slot3))
        {
            Debug.Log("3 ���� �Ұ�");

            return;
        }

        int weaponIndex = -1;
        if (sDown1)
        {
            weaponIndex = slot1;
        }
        if (sDown2) weaponIndex = slot2;
        if (sDown3) weaponIndex = slot3;

        if((sDown1 || sDown2 || sDown3) && !isDodge)
        {
            if(equipWeapon != null)
            {
                equipWeapon.gameObject.SetActive(false); //���� ��Ȱ��ȭ
            }

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            animator.SetTrigger("doSwap");

            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }

    }

    void SwapOut()
    {
        isSwap = false;
    }

    void Interation()   // ȹ�� �� �ݱ� �Է�
    {
        if(iDown && nearObject != null && !isDodge)   // eŰ�� ������ nearobject ������� �ʰ� jump dodge false�϶�
        {

            if(nearObject.tag.Equals("Weapon") && !hasWeapon3) // ������ ȹ��
            {
                GetWeapon(nearObject);
                Destroy(nearObject);
            }
            else
            { Debug.Log("�������� ��á��.");}
        }
        if (iDown && exit != null && !isDodge)  // ���� ������ �̵�
        {
            if (exit.tag.Equals("Finish"))
            {
                SaveSystem.SaveData(this);  // ������ ����
                GameData.loadEnable = true; // ������ �ε� ���ɿ���
                GlobalFunc.LoadScene("DungeonScene");   
            }
        }
        if (iDown && chest != null && !isDodge) // �������� ����
        {
            DungeonObject item = chest.GetComponent<DungeonObject>();
            if (item != null)
            {
                item.ChestOpen();
                item.tag = "Untagged"; // �������ڸ� ������ ������ �±� ����
                chest = null;
                chestUI.gameObject.SetActive(false);
            }
        }
        if (iDown && sellItem != null && !isDodge)
        {
            //Debug.Log("���Ű� �ǳ�?");
            Item item = sellItem.GetComponent<Item>();

            if (curGold >= item.price)
            {
                switch (item.type)
                {
                    case Item.Type.Heart:
                        PlayerHeal(item.value);
                        break;

                    case Item.Type.Weapon:
                        GetWeapon(sellItem);
                        break;
                }
                SpendGold(item);
                Destroy(sellItem);
                sellItem = null;
                sellItemUI.gameObject.SetActive(false);
            }
        }
    }

    void FreezeRotation()
    {
        PlayerRigid.angularVelocity = Vector3.zero;
    }

    void StopToWall()
    {
        // ���� ��ġ ����
        float playerEye = 0.05f;
        Vector3 PlayerPosition = new Vector3 (transform.position.x, playerEye, transform.position.z);

        Debug.DrawRay(PlayerPosition, transform.forward * distance, Color.green);
        isBorder = Physics.Raycast(PlayerPosition, transform.forward, distance, LayerMask.GetMask("Wall"));
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if(!GameManager.instance.isGameOver)
        {
            if (other.tag.Equals("Item"))
            {
              Item item = other.GetComponent<Item>();
                switch(item.type)
                {
                    case Item.Type.Coin:
                        AddGold(item.value); 
                        break;

                    case Item.Type.Heart:
                        PlayerHeal(item.value);
                        break;

                    case Item.Type.Key:
                        GameManager.instance.DoorOpen();

                        Animator bossDoor = bossRoomDoor.GetComponentInChildren<Animator>();
                        bossDoor.SetBool("isDoorClose", false);

                        break;
                }
                Destroy(other.gameObject);
            }

            if(other.tag.Equals("MonsterMelee"))
            {
                if(!isDamage)
                {
                    Weapon weapon = other.GetComponent<Weapon>();
                    PlayerDamage(weapon.damage);

                    StartCoroutine(OnDamage());
                }
            }

            if (other.tag.Equals("EnemyBullet"))
            {
                if (!isDamage)
                {
                    Bullet enemyBullet = other.GetComponent<Bullet>();
                    PlayerDamage(enemyBullet.damage);

                    //���� �� 1�� ����? ������ �۵��� �ȵ�
                    //if(other.GetComponent<Bullet>() != null)       //������ٵ� ������ �������� �Ͽ� 
                    //{
                    //    Destroy(other.gameObject);
                    //}

                    //bool isBossAtk = other.name == "Boss Melee Area";
                    StartCoroutine(OnDamage());
                }
                if (other.GetComponent<Bullet>() != null)       //������ٵ� ������ �������� �Ͽ� 
                {
                    //Destroy(other.gameObject);        // !�÷��̾ ������ �� �ٽ� �������� �ʴ� ����
                }

            }
            if (other.tag.Equals("Enemy"))
            {

                EnemyTest targetPosition = other.GetComponent<EnemyTest>();
                if (targetPosition.target == null)
                {
                    targetPosition.target = transform;
                    targetPosition.scanCollider.enabled = false;    // !�� ������ ��ĵ �ݶ��̴� ���������
                    Debug.Log("�� : �÷��̾ �����ߴ�.");


                }
            }
            if (other.tag.Equals("BossRoom"))   // ������ ���� �� ������ �޼��� ȣ��
            {
                if (!isBossRoom)
                {
                    isBossRoom = true;
                    bossRoomDoor = other.gameObject;
                    Animator bossDoor = bossRoomDoor.GetComponentInChildren<Animator>();
                    bossDoor.SetBool("isDoorClose", true);

                    GameManager.instance.OnBossRoom();
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Weapon"))
        {
            nearObject = other.gameObject;

            //Debug.Log(nearObject.name);
        }
        if(other.tag.Equals("Finish"))
        {
            exit = other.gameObject;

            exitUI = other.transform.GetChild(0).gameObject;
            exitUI.gameObject.SetActive(true);

        }
        if (other.tag.Equals("Chest"))  // �������ڰ� ���� ���� ���� �� UI ���� Ű��
        {
            chest = other.gameObject;

            chestUI = other.transform.GetChild(0).gameObject;
            chestUI.gameObject.SetActive(true);
        }
        if (other.tag.Equals("Sell"))
        {
            sellItem = other.gameObject;

            Item item = sellItem.GetComponent<Item>();

            sellItemUI = item.UI;
            sellItemUI.gameObject.SetActive(true);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Weapon"))
        {
            nearObject = null; 
        }
        if (other.tag.Equals("Finish"))
        {
            exit = null;
            exitUI.gameObject.SetActive(false);
        }
        if (other.tag.Equals("Chest"))
        {
            chest = null;
            chestUI.gameObject.SetActive(false);
        }
        if (other.tag.Equals("Sell"))
        {
            sellItem = null;
            sellItemUI.gameObject.SetActive(false);
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        foreach (MeshRenderer mesh in meshs)         //�ݺ����� ����Ͽ� ��� ������ ���� ����
        {
            //mesh.material.color = Color.red;
            Material mat = mesh.material;
            mat.SetColor("_EmissionColor", Color.red * 0.5f);   // EmissionColor Ÿ���� �̷��� �� ����
        }

        //if (isBossAtk)
        //{
        //    rigid.AddForce(transform.forward * -25, ForceMode.Impulse);      //���� taunt���� ���� �Ŀ� �˹�
        //}
        yield return new WaitForSeconds(0.5f);  //���� Ÿ��

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            //mesh.material.color = Color.white;       //������� ����
            Material mat = mesh.material;
            mat.SetColor("_EmissionColor", Color.black);
        }

        //if (isBossAtk)
        //{
        //    rigid.velocity = Vector3.zero;     //1�� �� ������� ���ƿ�
        //}
    }

    public void GetWeapon(GameObject nearWeapon)
    {
        Item item = nearWeapon.GetComponent<Item>();

       

        if (!hasWeapon1)
        {

            hasWeapon1 = true;
            slot1 = item.value;

            Weapon weaponSlot1 = weapons[item.value].GetComponent<Weapon>();
            weaponSlot1.damage = item.damage;
            weaponSlot1.rate = item.rate;
            switch (item.weaponType)
            {
                case Item.WeaponType.Sword:
                    weaponSlot1.weaponType = Weapon.WeaponType.Sword;
                    break;
                case Item.WeaponType.ChainSaw:
                    weaponSlot1.weaponType = Weapon.WeaponType.ChainSaw;
                    break;
                case Item.WeaponType.TwohandSword:
                    weaponSlot1.weaponType = Weapon.WeaponType.TwohandSword;
                    break;
            }
        }
        else if (!hasWeapon2)
        {
            hasWeapon2 = true;
            slot2 = item.value;

            Weapon weaponSlot2 = weapons[item.value].GetComponent<Weapon>();
            weaponSlot2.damage = item.damage;
            weaponSlot2.rate = item.rate;
            switch (item.weaponType)
            {
                case Item.WeaponType.Sword:
                    weaponSlot2.weaponType = Weapon.WeaponType.Sword;
                    break;
                case Item.WeaponType.ChainSaw:
                    weaponSlot2.weaponType = Weapon.WeaponType.ChainSaw;
                    break; 
                case Item.WeaponType.TwohandSword:
                    weaponSlot2.weaponType = Weapon.WeaponType.TwohandSword;
                    break;
            }
        }
        else if (!hasWeapon3)
        {
            hasWeapon3 = true;
            slot3 = item.value;

            Weapon weaponSlot3 = weapons[item.value].GetComponent<Weapon>();
            weaponSlot3.damage = item.damage;
            weaponSlot3.rate = item.rate;
            switch (item.weaponType)
            {
                case Item.WeaponType.Sword:
                    weaponSlot3.weaponType = Weapon.WeaponType.Sword;
                    break;
                case Item.WeaponType.ChainSaw:
                    weaponSlot3.weaponType = Weapon.WeaponType.ChainSaw;
                    break; 
                case Item.WeaponType.TwohandSword:
                    weaponSlot3.weaponType = Weapon.WeaponType.TwohandSword;
                    break;
            }
        }
        SetWeapon();
       
    }
    public void SetWeapon()
    {
        //Debug.Log(item.value + "��ȣ�� �������� �Ծ���.");
        Image itemSlot1 = GameManager.instance.itemSlot1[slot1].GetComponent<Image>();  // ������ ���� 
        Image itemSlot2 = GameManager.instance.itemSlot2[slot2].GetComponent<Image>();  // ������ ���� 
        Image itemSlot3 = GameManager.instance.itemSlot3[slot3].GetComponent<Image>();  // ������ ���� 

        Color newColor = new Color(255, 255, 255, 255);
        if (hasWeapon1)
        { itemSlot1.color = newColor; }
        if (hasWeapon2)
        { itemSlot2.color = newColor; }
        if (hasWeapon3)
        { itemSlot3.color = newColor; }
    }

    // �÷��̾� ü�� ȸ��
    public void PlayerHeal(int heal)
    {
        curHealth += heal;
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        GameManager.instance.SetHealth(curHealth);
    }

    // �÷��̾� ������
    public void PlayerDamage(int damage)
    {
        curHealth -= damage;
        GameManager.instance.SetHealth(curHealth);
        animator.SetTrigger("isDamage");


        if (0 >= curHealth && !isDie)
        {
            isDie = true;
            GameData.loadEnable = false;    // ���̺� �ε� ���ϰ� ����
            SaveSystem.SaveData(this);  // ������ ����
            GameManager.instance.OnGameOver();
            animator.SetBool("isDie", GameManager.instance.isGameOver);
        }
    }

    // ��� ȹ��
    public void AddGold(int gold)
    {
        curGold += gold;
        if(curGold > maxGold)
        {
            curGold = maxGold;
        }
        GameManager.instance.SetGold(curGold);

    }
    // ��� �Һ�
    public void SpendGold(Item item)
    {
        curGold -= item.price;
        GameManager.instance.goldText.text = string.Format("{0}", curGold);
        item.tag = "Untagged";
    }

    public void LoadData()
    {
        GameData data = SaveSystem.LoadData();

        maxHealth = data.playerMaxHP;
        curHealth = data.playerCurHP;
        maxGold = data.maxGold;
        curGold = data.curGold;

        slot1 = data.slot1;
        slot2 = data.slot2;
        slot3 = data.slot3;

        hasWeapon1 = data.hasWeapon1;
        hasWeapon2 = data.hasWeapon2;
        hasWeapon3 = data.hasWeapon3;

    }
   
}