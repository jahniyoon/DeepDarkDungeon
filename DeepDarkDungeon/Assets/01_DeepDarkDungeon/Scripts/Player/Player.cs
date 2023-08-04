using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    float hAxis;
    float vAxis;

    Vector3 moveVec;
    Vector3 dodgeVec;  //ȸ�� ������ȯ x

    Rigidbody PlayerRigid;
    Animator animator;

    bool wDown;
    bool jDown;

    
    bool isDodge;


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
        Dodge();
        
    }

    void GetInput()        //Input �ý���
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        wDown = Input.GetButton("Walk");       //shift ���� �� �۵�
        jDown = Input.GetButtonDown("Jump");
    }

    void Move()     //ĳ���� ������
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;

         
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;   //walk �ӵ�

        
        animator.SetBool("isRun", moveVec != Vector3.zero);
        animator.SetBool("isWalk", wDown);
    }
    
    void Turn()  //ĳ���� ȸ�������ִ� 
    {
        
            transform.LookAt(transform.position + moveVec);
        
        

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
        isDodge = false;
    }


}
