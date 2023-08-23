using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameData 
{

    [Header("Player")]
    public int playerMaxHP;
    public int playerCurHP;

    public int maxGold;
    public int curGold;

    // ������ ����
    public bool[] hasWeapon; // ���� ����
    public int[] itemSlot;   // ������ ����

    public static bool loadEnable = false;
    public static int floor;

    public GameData(Player player)
    {
        playerMaxHP = player.maxHealth;
        playerCurHP = player.curHealth;
        maxGold = player.maxGold;
        curGold = player.curGold;


        hasWeapon = new bool[3];
        itemSlot = new int[3]; // �迭 �ʱ�ȭ

        for (int i = 0; i < 3; i++)
        {
            hasWeapon[i] = player.hasWeapon[i];
            itemSlot[i] = player.itemSlot[i];
        }
    }
}
