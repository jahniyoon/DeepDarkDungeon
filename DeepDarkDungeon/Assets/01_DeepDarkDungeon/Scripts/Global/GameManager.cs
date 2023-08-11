using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text goldText;

    public Slider playerHealth;
    Player player;

    [Header("Debug instance")]

    public bool isGameOver = false;
    public bool isDoorOpen = false;
    public GameObject gameoverUI;


    // 초기화
    public void Initialization()
    {
        isGameOver = false;
    }   // } Initialization // } Initialization

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


    void Update()
    {
       
       
    }


    public void SetMaxHealth(int health)
    {
        playerHealth.maxValue = health;
        playerHealth.value = health;
    }

    public void SetHealth(int health)
    {
        playerHealth.value = health;
    }

    public void OnGameOver()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);

        //Audio audio = FindObjectOfType<Audio>();
        //audio.RetrySound();
    }

}
