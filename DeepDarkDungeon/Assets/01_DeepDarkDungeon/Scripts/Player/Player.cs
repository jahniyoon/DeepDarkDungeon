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

    public GameObject[] weapons;  //�÷��̾� ���� ���� �Լ� - ���� �����ϴ� 
    public bool[] hasWeapons;     //�÷��̾� ���� ���� �Լ� - ���� �κ��丮 �����

    Vector3 moveVec;
    Vector3 dodgeVec;  //ȸ�� ������ȯ x

    Rigidbody PlayerRigid;
    Animator animator;

    MeshRenderer[] meshs;

    bool wDown;
    bool jDown;

    bool iDown;

    bool fDown;   //����

    //���� ���� 
    bool sDown1;
    bool sDown2;
    bool sDown3;

    bool isSwap;
    bool isFireReady = true;

    bool isBorder;

    bool isDamage;

    bool isDodge;

    bool pauseDown; // ���� ��ư Ȯ��


    GameObject nearObject;
    GameObject exit;
    GameObject exitUI;
    GameObject chest;
    GameObject chestUI;
    GameObject sellItem;
    GameObject sellItemUI;
    Weapon equipWeapon;      //������ ������ ���⸦ �����ϴ� ������ ����

    bool hasSlot1 = false;
    bool hasSlot2 = false;
    bool hasSlot3 = false;

    int equipWeaponIndex = -1; //���� �ߺ� ��ü, ���� ���� Ȯ���� ���� ����

    float fireDelay;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();     //�ڽ� ������Ʈ�� �ִϸ��̼� �־ 
        meshs = GetComponentsInChildren<MeshRenderer>();

        GameManager.instance.SetMaxHealth(maxHealth); // ü�� �ʱ�ȭ

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


    void GetInput()        //Input �ý���
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
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;


        if (isSwap)   //�������϶��� ������ �� ����   //���⿡ isBorder������ Ű���忡 ���� ȸ�� ���� �����
        {
            moveVec = Vector3.zero;
        }
        if (!isBorder)
        {
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;     //���׿����� Ʈ��� 0.3 �ƴϸ� 1
        }
            

        animator.SetBool("isRun", moveVec != Vector3.zero);
        animator.SetBool("isWalk", wDown);
    }
    
    void Turn()  //ĳ���� ȸ�������ִ� 
    {
            transform.LookAt(transform.position + moveVec);

        
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
            animator.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");   //���� Ÿ�Կ� ���� �ٸ� Ʈ���� ���� 
            fireDelay = 0;  //���� ������ 0���� �÷��� ���� ���ݱ��� ��ٸ����� �ۼ�
        }
    }



    void Dodge()
    {
        if(jDown && moveVec != Vector3.zero && !isDodge)
        {
            dodgeVec = moveVec; //ȸ���ϸ鼭 ���� ��ȯ x
            
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

    void DodgeStop() //ȸ�� ���� �� �̵����� - ȸ�� �߿��� ���� ��ȯ �ȵǴ� 
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

    void Interation()   //���� �Լ�
    {
        if(iDown && nearObject != null && !isDodge)   // eŰ�� ������ nearobject ������� �ʰ� jump dodge false�϶�
        {
            if(nearObject.tag.Equals("Weapon"))
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                // ToDo ���� 1�� ĭ�� �����ǰ� ����. ���� ������ �ߺ����� ȹ��� ���� �ڸ��� ������.
                // ���� ������ ���� �����Ϸ�� �����ۿ� ���� ���������� 1,2,3 ���Կ� �־����� �����ǵ��� �����ؾ���.

                Debug.Log(item.value + "��ȣ�� �������� �Ծ���." );
                Image itemSlot1 = GameManager.instance.itemSlot1[item.value].GetComponent<Image>();  // ������ ���� 
                Image itemSlot2 = GameManager.instance.itemSlot2[item.value].GetComponent<Image>();  // ������ ���� 
                Image itemSlot3 = GameManager.instance.itemSlot3[item.value].GetComponent<Image>();  // ������ ���� 

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
                Debug.Log("�ǴϽ� Ȯ��");
                GlobalFunc.LoadScene("DungeonScene");
            }
        }
        if (iDown && chest != null && !isDodge)
        {
            DungeonObject item = chest.GetComponent<DungeonObject>();
            if ((item != null))
            {
                item.ChestOpen();
                item.tag = "Untagged"; // �������ڸ� ������ ������ �±� ����
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
        // ���� ��ġ ����
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
                    Destroy(other.gameObject);        // !�÷��̾ ������ �� �ٽ� �������� �ʴ� ����
                }

            }
            if (other.tag.Equals("Enemy"))
            {
                Debug.Log("�� : �÷��̾ �����ߴ�.");

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
        if (other.tag.Equals("Chest"))  // �������ڰ� ���� ���� ���� �� UI ���� Ű��
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
        foreach (MeshRenderer mesh in meshs)         //�ݺ����� ����Ͽ� ��� ������ ���� ����
        {
            mesh.material.color = Color.red;
        }

        //if (isBossAtk)
        //{
        //    rigid.AddForce(transform.forward * -25, ForceMode.Impulse);      //���� taunt���� ���� �Ŀ� �˹�
        //}
        yield return new WaitForSeconds(0.5f);  //���� Ÿ��

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;       //������� ����
        }

        //if (isBossAtk)
        //{
        //    rigid.velocity = Vector3.zero;     //1�� �� ������� ���ƿ�
        //}

    }

  
 

}


