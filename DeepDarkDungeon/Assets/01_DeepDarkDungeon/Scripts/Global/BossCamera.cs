using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour
{

    public Transform target;
    public CinemachineVirtualCamera bossCamera;
    bool findCamera;

    private void Update()
    {
        if (!findCamera)
        {
            target = GameObject.FindWithTag("BossLich").GetComponent<Transform>();

            bossCamera.m_LookAt = target;
            bossCamera.Follow = target;
        }
    }
}
