using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    float hAxis;
    float vAxis;

    Vector3 moveVec;
    Vector3 dodgeVec;  //회피 방향전환 x

    Rigidbody PlayerRigid;
    Animator animator;

    bool wDown;
    bool jDown;

    
    bool isDodge;


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
        Dodge();
        
    }

    void GetInput()        //Input 시스템
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        wDown = Input.GetButton("Walk");       //shift 누를 때 작동
        jDown = Input.GetButtonDown("Jump");
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
        isDodge = false;
    }


}
