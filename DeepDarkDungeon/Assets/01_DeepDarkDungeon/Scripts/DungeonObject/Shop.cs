using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public GameObject[] SellItem1, SellItem2, SellItem3;

    
    private void OnEnable() // TODO : ������ �Ǹ� ����Ȯ�� ���� �ʿ�
    {
        SellItem1[Random.Range(0, 3)].SetActive(true);
        SellItem2[Random.Range(0, 3)].SetActive(true);
        SellItem3[Random.Range(0, 3)].SetActive(true);
    }

}
