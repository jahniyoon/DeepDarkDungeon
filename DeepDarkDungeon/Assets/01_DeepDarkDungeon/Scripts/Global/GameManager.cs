using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Debug instance")]

    public GameObject gameoverUI;

    // �ʱ�ȭ
    public void Initialization()
    {
 
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


    void Update()
    {
       
       
    }

}
