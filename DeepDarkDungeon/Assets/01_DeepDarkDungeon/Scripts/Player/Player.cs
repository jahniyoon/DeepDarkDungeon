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

    public GameObject[] weapons;  //플레이어 무기 관련 함수 - 무기 연결하는 

    public bool hasWeapon1;     //플레이어 무기 관련 함수 - 무기 인벤토리 비슷한
    public bool hasWeapon2;     
    public bool hasWeapon3;  
    // 습득한 아이템 슬롯에 저장
    public int slot1;
    public int slot2;
    public int slot3;

    int equipWeaponIndex = -1; //무기 중복 교체, 없는 무기 확인을 위한 조건
    Weapon equipWeapon;         //기존에 장착된 무기를 저장하는 변수를 선언


    // 플레이어 인풋
    float hAxis;
    float vAxis;

    bool wDown;
    bool jDown;
    bool iDown;
    bool fDown;   // 공격

    // 플레이어 이동
    Vector3 moveVec;
    Vector3 dodgeVec;  // 회피 방향전환 x

    //무기 스왑 
    bool sDown1;
    bool sDown2;
    bool sDown3;

  

    // 플레이어 상태 체크
    bool isSwap;                // 아이템 스왑 중 체크
    bool isFireReady = true;    // 공격 딜레이 체크
    bool isBorder;              // 이동 시 정면 벽 체크
    bool isDamage;              // 데미지 상태인지 체크
    bool isDodge;               // 닷지중인지 체크
    bool isDie;                 // 죽었는지 체크
    bool isAttack;

    bool isBossRoom;            // 보스룸인지 체크

    bool pauseDown;             // 포즈 상태 확인
    float fireDelay;

    // 플레이어 상호작용 게임오브젝트
    GameObject bossRoomDoor;
    GameObject nearObject;      // 가까운 오브젝트
    GameObject exit;            // 출구 오브젝트
    GameObject exitUI;          // 출구 UI
    GameObject chest;           // 보물상자
    GameObject chestUI;         // 보물상자 UI
    GameObject sellItem;        // 판매 아이템
    GameObject sellItemUI;      // 판매 아이템 UI

    // Start is called before the first frame update
    void Start()
    {

        PlayerRigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();     //자식 오브젝트에 애니메이션 넣어서 
        meshs = GetComponentsInChildren<MeshRenderer>();

        if (GameData.loadEnable)    //  불러오기가 가능하면 데이터 로드 (던전을 클리어 해야 loadEnable = true)
        {
            LoadData();
        }
        //GameManager.instance.SaveData(maxHealth, curHealth, maxGold, curGold);  // 플레이어 데이터 저장
        GameManager.instance.Initialization();                                  // 상태 초기화
        GameManager.instance.SetMaxHealth(maxHealth); // 체력 초기화
        GameManager.instance.SetHealth(curHealth); // 체력 초기화
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

    // 입력
    void GetInput()        
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        wDown = Input.GetButton("Walk");       //shift 누를 때 작동
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire1");

        iDown = Input.GetButtonDown("Interation");

        //무기 스왑
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");

        pauseDown = Input.GetButtonDown("Cancel");
    }

    void Move()     //캐릭터 움직임
    {
        moveVec = new Vector3(hAxis, 0, vAxis);


        if (isDodge)
            moveVec = dodgeVec;


        if (isSwap)   //스왑중일때는 움직임 수 없게   //여기에 isBorder넣으면 키보드에 의한 회전 문제 생긴다
        {
            moveVec = Vector3.zero;
        }
        if (!isBorder)
        {
            //transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;     //삼항연산자 트루면 0.3 아니면 1
            PlayerRigid.MovePosition(transform.position + transform.forward * moveVec.normalized.magnitude * speed * Time.deltaTime);

        }


        animator.SetBool("isRun", moveVec != Vector3.zero);
        animator.SetBool("isWalk", wDown);
    }
    
    void Turn()  //캐릭터 회전
    {
        if (moveVec == Vector3.zero) return;

        var rot = Quaternion.LookRotation(moveVec.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
            //transform.LookAt(transform.position + moveVec);   
    }

    void Attack()
    {
        if (equipWeapon == null)      //무기만 있을 때 실행되도록 현재장비 체크
        {
            return;
        }

        fireDelay += Time.deltaTime;    //공격딜레이에 시간을 더해주고 공격가능 여부를 확인
        isFireReady = equipWeapon.rate < fireDelay;
     

        if (fDown && isFireReady && !isDodge && !isSwap)
        {
            equipWeapon.Use();                   //public 선언해서 다른 스크립트에 있는 거 가져올수있다

            isAttack = true;

            Debug.Log("공격을 했다. 공격중");

            switch (equipWeapon.weaponType)  // 무기 타입에 따라 스위치로 다른 애니메이션 트리거 실행
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
            //animator.SetTrigger(equipWeapon.weaponType == Weapon.WeaponType.Melee ? "doSwing" : "doShot");   //무기 타입에 따라 다른 트리거 실행 

            fireDelay = 0;  //공격 딜레이 0으로 올려서 다음 공격까지 기다리도록 작성
            StopCoroutine("AttackDelay");
            StartCoroutine("AttackDelay");


        }
    }
    IEnumerator AttackDelay()  // 공격 딜레이
    {
        yield return new WaitForSeconds(equipWeapon.rate - 0.5f);
        isAttack = false;

    }



    void Dodge()
    {
        if(jDown && moveVec != Vector3.zero && !isDodge)
        {
            isAttack = false;   // 어택 캔슬
            dodgeVec = moveVec; //회피하면서 방향 전환 x
            
            speed *= 2f;
            animator.SetTrigger("doDodge");
            isDodge = true;
            smoke.gameObject.SetActive(true);   // 닷지시 연기


            Invoke("DodgeOut", 0.1f);
        }
        
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        Invoke("DodgeStop", 0.6f);
    }

    void DodgeStop() //회피 끝난 후 이동가능 - 회피 중에는 방향 전환 안되는 
    {
        isDodge = false;
        smoke.gameObject.SetActive(false);   // 닷지시 연기

    }
    // 게임 Pause
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
            Debug.Log("1 스왑 불가");

            return;
        }
        if (sDown2 && (!hasWeapon2 || equipWeaponIndex == slot2))
        {
            Debug.Log("2 스왑 불가");

            return;
        }
        if (sDown3 && (!hasWeapon3 || equipWeaponIndex == slot3))
        {
            Debug.Log("3 스왑 불가");

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
                equipWeapon.gameObject.SetActive(false); //시작 비활성화
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

    void Interation()   // 획득 및 줍기 입력
    {
        if(iDown && nearObject != null && !isDodge)   // e키를 했을때 nearobject 비어있지 않고 jump dodge false일때
        {

            if(nearObject.tag.Equals("Weapon") && !hasWeapon3) // 아이템 획득
            {
                GetWeapon(nearObject);
                Destroy(nearObject);
            }
            else
            { Debug.Log("아이템이 꽉찼다.");}
        }
        if (iDown && exit != null && !isDodge)  // 다음 층으로 이동
        {
            if (exit.tag.Equals("Finish"))
            {
                SaveSystem.SaveData(this);  // 데이터 저장
                GameData.loadEnable = true; // 데이터 로드 가능여부
                GlobalFunc.LoadScene("DungeonScene");   
            }
        }
        if (iDown && chest != null && !isDodge) // 보물상자 열기
        {
            DungeonObject item = chest.GetComponent<DungeonObject>();
            if (item != null)
            {
                item.ChestOpen();
                item.tag = "Untagged"; // 보물상자를 열었기 때문에 태그 제거
                chest = null;
                chestUI.gameObject.SetActive(false);
            }
        }
        if (iDown && sellItem != null && !isDodge)
        {
            //Debug.Log("구매가 되나?");
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
        // 레이 위치 조정
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

                    //맞은 후 1초 무적? 때문에 작동이 안됨
                    //if(other.GetComponent<Bullet>() != null)       //리지드바디 유무를 조건으로 하여 
                    //{
                    //    Destroy(other.gameObject);
                    //}

                    //bool isBossAtk = other.name == "Boss Melee Area";
                    StartCoroutine(OnDamage());
                }
                if (other.GetComponent<Bullet>() != null)       //리지드바디 유무를 조건으로 하여 
                {
                    //Destroy(other.gameObject);        // !플레이어를 공격한 뒤 다시 공격하지 않는 문제
                }

            }
            if (other.tag.Equals("Enemy"))
            {

                EnemyTest targetPosition = other.GetComponent<EnemyTest>();
                if (targetPosition.target == null)
                {
                    targetPosition.target = transform;
                    targetPosition.scanCollider.enabled = false;    // !적 만나면 스캔 콜라이더 없애줘야함
                    Debug.Log("적 : 플레이어를 감지했다.");


                }
            }
            if (other.tag.Equals("BossRoom"))   // 보스룸 진입 시 보스룸 메서드 호출
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
        if (other.tag.Equals("Chest"))  // 보물상자가 범위 내에 있을 때 UI 껐다 키기
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
        foreach (MeshRenderer mesh in meshs)         //반복문을 사용하여 모든 재질의 색상 변경
        {
            //mesh.material.color = Color.red;
            Material mat = mesh.material;
            mat.SetColor("_EmissionColor", Color.red * 0.5f);   // EmissionColor 타입은 이렇게 색 변경
        }

        //if (isBossAtk)
        //{
        //    rigid.AddForce(transform.forward * -25, ForceMode.Impulse);      //보스 taunt공격 맞은 후에 넉백
        //}
        yield return new WaitForSeconds(0.5f);  //무적 타임

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            //mesh.material.color = Color.white;       //원래대로 돌림
            Material mat = mesh.material;
            mat.SetColor("_EmissionColor", Color.black);
        }

        //if (isBossAtk)
        //{
        //    rigid.velocity = Vector3.zero;     //1초 후 원래대로 돌아옴
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
        //Debug.Log(item.value + "번호의 아이템을 먹었다.");
        Image itemSlot1 = GameManager.instance.itemSlot1[slot1].GetComponent<Image>();  // 아이템 슬롯 
        Image itemSlot2 = GameManager.instance.itemSlot2[slot2].GetComponent<Image>();  // 아이템 슬롯 
        Image itemSlot3 = GameManager.instance.itemSlot3[slot3].GetComponent<Image>();  // 아이템 슬롯 

        Color newColor = new Color(255, 255, 255, 255);
        if (hasWeapon1)
        { itemSlot1.color = newColor; }
        if (hasWeapon2)
        { itemSlot2.color = newColor; }
        if (hasWeapon3)
        { itemSlot3.color = newColor; }
    }

    // 플레이어 체력 회복
    public void PlayerHeal(int heal)
    {
        curHealth += heal;
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        GameManager.instance.SetHealth(curHealth);
    }

    // 플레이어 데미지
    public void PlayerDamage(int damage)
    {
        curHealth -= damage;
        GameManager.instance.SetHealth(curHealth);
        animator.SetTrigger("isDamage");


        if (0 >= curHealth && !isDie)
        {
            isDie = true;
            GameData.loadEnable = false;    // 세이브 로드 못하게 설정
            SaveSystem.SaveData(this);  // 데이터 저장
            GameManager.instance.OnGameOver();
            animator.SetBool("isDie", GameManager.instance.isGameOver);
        }
    }

    // 골드 획득
    public void AddGold(int gold)
    {
        curGold += gold;
        if(curGold > maxGold)
        {
            curGold = maxGold;
        }
        GameManager.instance.SetGold(curGold);

    }
    // 골드 소비
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