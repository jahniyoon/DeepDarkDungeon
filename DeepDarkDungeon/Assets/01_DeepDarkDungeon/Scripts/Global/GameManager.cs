using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    GameData gameData = new GameData();
    public static GameManager instance;

    [HideInInspector] public bool isGameOver = false; // ���ӿ��� Ȯ��
    [HideInInspector] public bool isPause = false; // ���� Ȯ��
    [HideInInspector] public bool isDoorOpen = false; // �� ���� Ȯ��

    [Header("UI instance")]

    public GameObject gameoverUI;   // ���� ���� UI
    public GameObject gamePauseUI;   // ���� ���� UI
    public GameObject keyUI;   // ���� ���� UI

    public Slider playerHealth; // ü�� ��
    public TMP_Text goldText;   // ��� �ؽ�Ʈ

    public Image[] itemSlot1;
    public Image[] itemSlot2;
    public Image[] itemSlot3;


    // �ʱ�ȭ
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
            GlobalFunc.LogWarning("���� �� �� �̻��� ���� �Ŵ����� �����մϴ�.");
        }
    }   // } Awake


    void Start()
    {
        Initialization();   // �ʱ�ȭ
    }    // } Start()

    // ü�¹� UI �ƽ��� ����
    public void SetMaxHealth(int health)
    {
        playerHealth.maxValue = health;
        playerHealth.value = health;
    }
    // ü�¹� UI �÷��̾��� ü������ ����
    public void SetHealth(int health)
    {
        playerHealth.value = health;
    }
    // ��� UI ������Ʈ
    public void SetGold(int gold)
    {
        goldText.text = string.Format("{0}", gold);
    }

    // ������ ���̺�
    public void SaveData(int playerMaxHP, int playerCurHP, int maxGold, int curGold)
    {
        gameData.playerMaxHP = playerMaxHP;
        gameData.playerCurHP = playerCurHP;
        gameData.maxGold = maxGold;
        gameData.curGold = curGold;
        gameData.data = true;
    }

    // ������ �ε�
    public void LoadData(int playerMaxHP, int playerCurHP, int maxGold, int curGold)
    {
        playerMaxHP = gameData.playerMaxHP;
        playerCurHP = gameData.playerCurHP;
        maxGold = gameData.maxGold;
        curGold = gameData.curGold;
    }
    public bool DataCheck()
    {
        return gameData.data;
    }

    // ���� ����
    public void OnGameOver()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);

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
    public void DoorOpen()
    {
        isDoorOpen = true;
        keyUI.SetActive(true);
    }


}
