using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    float hAxis;
    float vAxis;

    Vector3 moveVec;

    Rigidbody PlayerRigid;
    Animator animator;

    bool wDown;


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

    }

    void GetInput()        //Input �ý���
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        wDown = Input.GetButton("Walk");       //shift ���� �� �۵�
    }

    void Move()     //ĳ���� ������
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;


        animator.SetBool("isWalk", wDown);
    }

    


}
