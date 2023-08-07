using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    public Transform playerTransform;

    public GameObject followCamera;

    public int coin;
    public int health;

    public int maxCoin;
    public int maxHealth;

    float hAxis;
    float vAxis;

    public GameObject[] weapons;  //�÷��̾� ���� ���� �Լ� - ���� �����ϴ� 
    public bool[] hasWeapons;     //�÷��̾� ���� ���� �Լ� - ���� �κ��丮 �����

    Vector3 moveVec;
    Vector3 dodgeVec;  //ȸ�� ������ȯ x

    Rigidbody PlayerRigid;
    Animator animator;

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

    bool isDodge;

    GameObject nearObject;
    Weapon equipWeapon;      //������ ������ ���⸦ �����ϴ� ������ ����

    int equipWeaponIndex = -1; //���� �ߺ� ��ü, ���� ���� Ȯ���� ���� ����

    float fireDelay;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();     //�ڽ� ������Ʈ�� �ִϸ��̼� �־ 

       
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Attack();
        Dodge();
        Swap();
        Interation();

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
            animator.SetTrigger(equipWeapon.type == Weapon.Type.melee ? "doSwing" : "doShot");   //���� Ÿ�Կ� ���� �ٸ� Ʈ���� ���� 
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

                Destroy(nearObject);
            }

        }
    }

    void FreezeRotation()
    {
        PlayerRigid.angularVelocity = Vector3.zero;
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 10f, LayerMask.GetMask("Wall"));
    }

    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Item"))
        {
          Item item = other.GetComponent<Item>();
            switch(item.type)
            {
                case Item.Type.Coin:
                    coin += item.value;
                    if (coin > maxCoin)
                        coin = maxCoin;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;
            }
            Destroy(other.gameObject);
        }
      
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Weapon"))
        {
            nearObject = other.gameObject;

            //Debug.Log(nearObject.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Weapon"))
        {
            nearObject = null; 
        }

    }

}


