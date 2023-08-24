using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [Header("Camera")]
    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera bossRoomCamera;
    public CinemachineVirtualCamera[] virtualCameras;

    private void Awake()
    {
        if (instance == null)
        { instance = this; }

        else
        {
            GlobalFunc.LogWarning("씬에 두 개 이상의 카메라 매니저가 존재합니다.");
        }
    } 
    private void Start()
    {
        primaryCamera = GetComponent<CinemachineVirtualCamera>();
        SwitchToBossRoomCamera();
        SwitchToPlayerCamera();
    }


    // 카메라 전환
    public void SwitchToPlayerCamera()
    {
        primaryCamera = playerCamera;
        SwitchToCamera(primaryCamera);
    }
    public void SwitchToBossRoomCamera()
    {
        primaryCamera = bossRoomCamera;
        SwitchToCamera(primaryCamera);
    }
    public void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (CinemachineVirtualCamera camera in virtualCameras)
        {
            camera.enabled = camera == targetCamera;
        }
    }
   

    public void OnBossRoom()
    {
        SwitchToBossRoomCamera();
        StartCoroutine("BossRoomCutScene");
    }
    IEnumerator BossRoomCutScene()
    {
        yield return new WaitForSeconds(1f);

        Time.timeScale = 0.0f;

        yield return new WaitForSeconds(3f);
        SwitchToPlayerCamera();
        Time.timeScale = 1f;
    }

}
