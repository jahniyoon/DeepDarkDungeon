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


    // �ʱ�ȭ
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
            GlobalFunc.LogWarning("���� �� �� �̻��� ���� �Ŵ����� �����մϴ�.");
        }
    }   // } Awake


    void Start()
    {
        Initialization();   // �ʱ�ȭ
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
