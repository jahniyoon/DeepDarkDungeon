using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public bool isGameOver = false; // 게임오버 확인
    [HideInInspector] public bool isPause = false; // 포즈 확인
    [HideInInspector] public bool isDoorOpen = false; // 문 열림 확인
    private int floor = 1;

    [Header("UI instance")]

    public GameObject gameoverUI;   // 게임 오버 UI
    public GameObject gamePauseUI;   // 게임 포즈 UI
    public GameObject keyUI;   
    public GameObject bossUI;

    [Header("Player UI instance")]
    public Slider playerHealth; // 체력 바
    public TMP_Text goldText;   // 골드 텍스트
    public TMP_Text floorText;

    [Header("Item Slot UI")]
    public Image[] itemSlot1;
    public Image[] itemSlot2;
    public Image[] itemSlot3;


    // 초기화
    public void Initialization()
    {
        isGameOver = false;
        isPause = false;

        Time.timeScale = 1;
        

    }   // } Initialization 

    private void Awake()
    {
        if (instance == null)
        { instance = this; }

        else
        {
            GlobalFunc.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다.");
        }
    }   // } Awake


    void Start()
    {
        Initialization();   // 초기화
    }    // } Start()

    // 체력바 UI 플레이어의 체력으로 설정
    public void SetHealth(int health)
    {
        playerHealth.value = health;
    }
    public void SetMaxHealth(int maxHealth)
    {
        playerHealth.maxValue = maxHealth;
        playerHealth.value = maxHealth;
    }
    // 골드 UI 업데이트
    public void SetGold(int gold)
    {
        goldText.text = string.Format("{0}", gold);
    }

    public void SetFloor()
    {
        GameData.floor += 1;
        floorText.text = string.Format("Floor {0}", GameData.floor);


    }

    // 게임 오버
    public void OnGameOver()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);
        GameData.floor = 0;
        
        //Audio audio = FindObjectOfType<Audio>();
        //audio.RetrySound();
    }

    public void OnGamePause()
    {
        if (!isPause && !isGameOver)
        {
            isPause = true; 
            Time.timeScale = 0;
            gamePauseUI.SetActive(true);
        }

        else if(isPause && !isGameOver)
        {
            isPause = false;
            Time.timeScale = 1;
            gamePauseUI.SetActive(false);
        }
    }
    public void OnBossRoom()
    {
        bossUI.SetActive(true);
    }
    public void BossDead()
    {
        bossUI.SetActive(false);
    }
    public void DoorOpen()
    {
        isDoorOpen = true;
    }


}
