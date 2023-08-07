using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    public int coin;
    public int health;

    public int maxCoin;
    public int maxHealth;

    float hAxis;
    float vAxis;

    public GameObject[] weapons;  //플레이어 무기 관련 함수 - 무기 연결하는 
    public bool[] hasWeapons;     //플레이어 무기 관련 함수 - 무기 인벤토리 비슷한

    Vector3 moveVec;
    Vector3 dodgeVec;  //회피 방향전환 x

    Rigidbody PlayerRigid;
    Animator animator;

    bool wDown;
    bool jDown;

    bool iDown;

    bool fDown;   //공격

    //무기 스왑 
    bool sDown1;
    bool sDown2;
    bool sDown3;

    bool isSwap;

    bool isDodge;

    GameObject nearObject;
    Weapon equipWeapon;      //기존에 장착된 무기를 저장하는 변수를 선언

    int equipWeaponIndex = -1; //무기 중복 교체, 없는 무기 확인을 위한 조건



    // Start is called before the first frame update
    void Start()
    {
        PlayerRigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();     //자식 오브젝트에 애니메이션 넣어서 

       
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
    }

    void Move()     //캐릭터 움직임
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;

         
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;   //walk 속도

        
        animator.SetBool("isRun", moveVec != Vector3.zero);
        animator.SetBool("isWalk", wDown);
    }
    
    void Turn()  //캐릭터 회전시켜주는 
    {
            transform.LookAt(transform.position + moveVec);
    }

    void Attack()
    {
     
        if(fDown)
        {
            animator.SetTrigger("doSwing");
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

                Destroy(nearObject);
            }

        }
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


