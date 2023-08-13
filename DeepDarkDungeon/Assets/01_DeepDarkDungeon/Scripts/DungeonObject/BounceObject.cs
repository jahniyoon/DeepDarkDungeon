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
        // 물체가 생성되면 튀어오르기
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
        // 중력 비활성화 및 튀어오르는 상태 설정
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

        // 일정 시간 이후 중력 다시 활성화
        StartCoroutine(EnableGravityAfterDelay(bounceDuration));
    }

    private IEnumerator EnableGravityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rigidbody.useGravity = true;
        isBouncing = false;
    }

}
