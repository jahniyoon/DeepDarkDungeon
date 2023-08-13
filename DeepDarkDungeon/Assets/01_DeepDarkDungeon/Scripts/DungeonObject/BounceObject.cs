using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour
{
    public float bounceForce = 10.0f;
    public float bounceDuration = 0.5f;
    public float gravityScale = 1.0f;
    public float friction = 0.5f;

    private Rigidbody rigidbody;
    private bool isBouncing = false;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // ��ü�� �����Ǹ� Ƣ�������
        StartBounce();  
    }

    void Update()
    {
        if (isBouncing)
        {
            rigidbody.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }

    }

    private void StartBounce()
    {
        // �߷� ��Ȱ��ȭ �� Ƣ������� ���� ����
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

        // ���� �ð� ���� �߷� �ٽ� Ȱ��ȭ
        StartCoroutine(EnableGravityAfterDelay(bounceDuration));
    }

    private IEnumerator EnableGravityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rigidbody.useGravity = true;
        isBouncing = false;
    }

}
