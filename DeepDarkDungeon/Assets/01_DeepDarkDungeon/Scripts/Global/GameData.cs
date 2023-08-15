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

    public bool data = false;
}
