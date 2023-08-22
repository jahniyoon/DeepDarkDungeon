using System;

[System.Serializable]
public class ItemDBEntity
{
    public enum Type { Coin, Grenade, Heart, Weapon, Exit, Key, Shield, Speed, Power}
    public enum WeaponType { Sword, ChainSaw, TwohandSword };

    public int itemNum;
    public Type type;
    public string name;
    public int value;
    public int price;
    public WeaponType weaponType;
    public int damage;
    public float rate;
}
