using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = default; // speed �� default�� ���� : ��ȯ

    Rigidbody PlayerRigid;     //������ �ٵ� ���� : ����
    Animator animator;        //�ִϸ����� ���� : ����

    // Start is called before the first frame update
    void Start()
    {
        // ��ȯ ������;

        PlayerRigid = GetComponent<Rigidbody>();    //������ �ҷ��� : ��
        animator = GetComponent<Animator>();        //�ִϸ����� �ҷ��� : ��
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk()
    {
        float playerSpeed;
        playerSpeed = speed;

    }
}
