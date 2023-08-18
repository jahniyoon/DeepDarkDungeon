using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{

    [Header("Player")]
    public int playerMaxHP;
    public int playerCurHP;

    public int maxGold;
    public int curGold;

    public static bool loadEnable = false;

    public GameData(Player player)
    {
        playerMaxHP = player.maxHealth;
        playerCurHP = player.curHealth;
        maxGold = player.maxGold;
        curGold = player.curGold;
    }
}
