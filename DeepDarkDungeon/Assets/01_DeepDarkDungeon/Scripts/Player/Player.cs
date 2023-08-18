using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;

    public Transform playerTransform;
    public GameObject followCamera;


    public int gold;
    public int maxGold;

    public int maxHealth;
    public int health;

    public float distance;

    float hAxis;
    float vAxis;

    public GameObject[] weapons;  //플레이어 무기 관련 함수 - 무기 연결하는 
    public bool[] hasWeapons;     //플레이어 무기 관련 함수 - 무기 인벤토리 비슷한

    Vector3 moveVec;
    Vector3 dodgeVec;  //회피 방향전환 x

    Rigidbody PlayerRigid;
    Animator animator;

    MeshRenderer[] meshs;

    bool wDown;
    bool jDown;

    bool iDown;

    bool fDown;   //공격

    //무기 스왑 
    bool sDown1;
    bool sDown2;
    bool sDown3;

    bool isSwap;
    bool isFireReady = true;

    bool isBorder;

    bool isDamage;

    bool isDodge;

    bool pauseDown; // 포즈 버튼 확인


    GameObject nearObject;
    GameObject exit;
    GameObject exitUI;
    GameObject chest;
    GameObject chestUI;
    GameObject sellItem;
    GameObject sellItemUI;
    Weapon equipWeapon;      //기존에 장착된 무기를 저장하는 변수를 선언

    bool hasSlot1 = false;
    bool hasSlot2 = false;
    bool hasSlot3 = false;

    int equipWeaponIndex = -1; //무기 중복 교체, 없는 무기 확인을 위한 조건

    float fireDelay;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();     //자식 오브젝트에 애니메이션 넣어서 
        meshs = GetComponentsInChildren<MeshRenderer>();

        GameManager.instance.SetMaxHealth(maxHealth); // 체력 초기화

    }

    // Update is called once per frame
    void Update()
    {
            GetInput();
            Pause();
        if (!GameManager.instance.isGameOver && !GameManager.instance.isPause)
        {
            Move();
            Turn();
            Attack();
            Dodge();
            Swap();
            Interation();
        }



    }

    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
       
    }


    void GetInput()        //Input 시스템
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
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;


        if (isSwap)   //스왑중일때는 움직임 수 없게   //여기에 isBorder넣으면 키보드에 의한 회전 문제 생긴다
        {
            moveVec = Vector3.zero;
        }
        if (!isBorder)
        {
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;     //삼항연산자 트루면 0.3 아니면 1
        }
            

        animator.SetBool("isRun", moveVec != Vector3.zero);
        animator.SetBool("isWalk", wDown);
    }
    
    void Turn()  //캐릭터 회전시켜주는 
    {
            transform.LookAt(transform.position + moveVec);

        
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
            animator.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");   //무기 타입에 따라 다른 트리거 실행 
            fireDelay = 0;  //공격 딜레이 0으로 올려서 다음 공격까지 기다리도록 작성
        }
    }



    void Dodge()
    {
        if(jDown && moveVec != Vector3.zero && !isDodge)
        {
            dodgeVec = moveVec; //회피하면서 방향 전환 x
            
            speed *= 2;
            animator.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.5f);
        }
        
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        Invoke("DodgeStop", 1f);
    }

    void DodgeStop() //회피 끝난 후 이동가능 - 회피 중에는 방향 전환 안되는 
    {
        isDodge = false;
    }

    void Pause()
    {
        if (pauseDown)
        {
            GameManager.instance.OnGamePause();
        }
   
    }
    void Swap()
    {
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
        {
            return;
        }
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
        {
            return;
        }
        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
        {
            return;
        }


        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

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

    void Interation()   //무기 입수
    {
        if(iDown && nearObject != null && !isDodge)   // e키를 했을때 nearobject 비어있지 않고 jump dodge false일때
        {
            if(nearObject.tag.Equals("Weapon"))
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                // ToDo 현재 1번 칸만 생성되게 구현. 따라서 아이템 중복으로 획득시 같은 자리에 생성됨.
                // 추후 아이템 셋팅 구현완료시 아이템에 따라 순차적으로 1,2,3 슬롯에 넣어지고 생성되도록 구현해야함.

                Debug.Log(item.value + "번호의 아이템을 먹었다." );
                Image itemSlot1 = GameManager.instance.itemSlot1[item.value].GetComponent<Image>();  // 아이템 슬롯 
                Image itemSlot2 = GameManager.instance.itemSlot2[item.value].GetComponent<Image>();  // 아이템 슬롯 
                Image itemSlot3 = GameManager.instance.itemSlot3[item.value].GetComponent<Image>();  // 아이템 슬롯 

                if (itemSlot1 != null && !hasSlot1)
                {
                    hasSlot1 = true;
                    Color newColor = new Color(255, 255, 255, 255);
                    itemSlot1.color = newColor;
                }
                else if (itemSlot2 != null && !hasSlot2)
                {
                    hasSlot2 = true;
                    Color newColor = new Color(255, 255, 255, 255);
                    itemSlot2.color = newColor;
                }
                else if (itemSlot3 != null && !hasSlot3)
                {
                    hasSlot3 = true;
                    Color newColor = new Color(255, 255, 255, 255);
                    itemSlot3.color = newColor;
                }

                Destroy(nearObject);
            }
        }
        if (iDown && exit != null && !isDodge)
        {
            if (exit.tag.Equals("Finish"))
            {
                Debug.Log("피니쉬 확인");
                GlobalFunc.LoadScene("DungeonScene");
            }
        }
        if (iDown && chest != null && !isDodge)
        {
            DungeonObject item = chest.GetComponent<DungeonObject>();
            if ((item != null))
            {
                item.ChestOpen();
                item.tag = "Untagged"; // 보물상자를 열었기 때문에 태그 제거
                chest = null;
                chestUI.gameObject.SetActive(false);
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

    //public void TakeDamage(int damage)
    //{
    //    health -= damage;

    //    StartCoroutine(OnDamage());
    //}


    public void OnTriggerEnter(Collider other)
    {
        if(!GameManager.instance.isGameOver)
        {
            if (other.tag.Equals("Item"))
            {
              Item item = other.GetComponent<Item>();
                switch(item.type)
                {
                    case Item.Type.Coin:
                        
                        gold += item.value;
                        if (maxGold < gold)
                        {
                            gold = maxGold;
                        }
                        GameManager.instance.goldText.text = string.Format("{0}", gold);
                        break;

                    case Item.Type.Heart:
                        health += item.value;
                        break;

                    case Item.Type.Key:
                        GameManager.instance.DoorOpen();
                        break;
                }
                Destroy(other.gameObject);
            }


            if (other.tag.Equals("EnemyBullet"))
                {
                if (!isDamage)
                {
                    Bullet enemyBullet = other.GetComponent<Bullet>();
                    health -= enemyBullet.damage;
                    GameManager.instance.SetHealth(health);

                    if (health <= 0)
                    {
                        GameManager.instance.OnGameOver();
                    }
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
                    Destroy(other.gameObject);        // !플레이어를 공격한 뒤 다시 공격하지 않는 문제
                }

            }
            if (other.tag.Equals("Enemy"))
            {
                Debug.Log("적 : 플레이어를 감지했다.");

                EnemyTest targetPosition = other.GetComponent<EnemyTest>();
                if (targetPosition.target == null)
                {
                    targetPosition.target = transform;

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
            Item itemUI = other.GetComponent<Item>();
            sellItemUI = itemUI.UI;
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
            mesh.material.color = Color.red;
        }

        //if (isBossAtk)
        //{
        //    rigid.AddForce(transform.forward * -25, ForceMode.Impulse);      //보스 taunt공격 맞은 후에 넉백
        //}
        yield return new WaitForSeconds(0.5f);  //무적 타임

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;       //원래대로 돌림
        }

        //if (isBossAtk)
        //{
        //    rigid.velocity = Vector3.zero;     //1초 후 원래대로 돌아옴
        //}

    }

  
 

}


