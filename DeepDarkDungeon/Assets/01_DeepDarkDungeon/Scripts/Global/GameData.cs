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

    public int slot1;
    public int slot2;
    public int slot3;

    public bool hasWeapon1;     //플레이어 무기 관련 함수 - 무기 인벤토리 비슷한
    public bool hasWeapon2;
    public bool hasWeapon3;


    public static bool loadEnable = false;
    public static int floor;

    public GameData(Player player)
    {
        playerMaxHP = player.maxHealth;
        playerCurHP = player.curHealth;
        maxGold = player.maxGold;
        curGold = player.curGold;

        slot1 = player.slot1;
        slot2 = player.slot2;
        slot3 = player.slot3;

        hasWeapon1 = player.hasWeapon1;
        hasWeapon2 = player.hasWeapon2;
        hasWeapon3 = player.hasWeapon3;

    }
}
