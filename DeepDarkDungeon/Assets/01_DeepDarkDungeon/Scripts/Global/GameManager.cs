using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Debug instance")]

    public GameObject gameoverUI;

    // 초기화
    public void Initialization()
    {
 
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


    void Update()
    {
       
       
    }

}
