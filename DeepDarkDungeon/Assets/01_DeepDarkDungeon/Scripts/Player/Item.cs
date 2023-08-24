using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Header("ItemSetting")]
    [SerializeField]
    private int itemNo;
    [SerializeField]
    private ItemDB itemDB;

    public enum Type { Coin, Grenade, Heart, Weapon, Exit, Key, Shield, Speed, Power }
    public Type type;       //������ ���� ����
    public enum WeaponType { Sword, ChainSaw, TwohandSword, ShortSword, Mace };
    public WeaponType weaponType;       //������ ���� ����
    public int value;       //������ ������ �� ���� ������ ����
    public int damage;       //�������� �������� ������ ����
    public float rate;       //������ ����  �ӵ��� ������ ����
    public bool sellItem;   // ���� �Ǹ� ������ üũ
    public string itemName;

    [HideInInspector] public int price;
    [Header("UI Setting")]
    public TMP_Text nameText; 
    public GameObject nameUI;
    public TMP_Text priceText; 
    public GameObject priceUI;

    Rigidbody rigid;
    SphereCollider sphereCollider;

    void Awake()
    {
        if (itemNo != 0)    // �������� ���� ���� ���� ����
        {
            for (int i = 1; i < itemDB.Entities.Count; i++)
            {
                if (itemDB.Entities[i].itemNum == itemNo)
                {
                    this.type = (Type)itemDB.Entities[i].type;

                    if (!sellItem)
                    {
                        switch ((Type)itemDB.Entities[i].type)
                        {
                            case Type.Weapon:
                                this.tag = "Weapon";
                                this.weaponType = (WeaponType)itemDB.Entities[i].weaponType;
                                this.damage = itemDB.Entities[i].damage;
                                this.rate = itemDB.Entities[i].rate;
                                this.itemName = itemDB.Entities[i].itemName;
                                break;

                            case Type.Coin:
                                this.tag = "Item";
                                break;
                            case Type.Heart:
                                this.tag = "Item";

                                break;
                        }
                    }
                    else
                    {
                        this.tag = "Sell";  // ���� �Ǹ� ������ üũ���� ��� 
                        this.itemName = itemDB.Entities[i].itemName;

                    }
                    this.value = itemDB.Entities[i].value;
                    this.price = itemDB.Entities[i].price;
                    //Debug.Log(itemDB.Entities[i].itemName + "�̸���?");

                    //Debug.Log("������ �ѹ��� :" + itemNo + "������ ���� :" + value);
                }
            }
        }
        rigid = GetComponent<Rigidbody>();
        //sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime);

        if (priceText != null)    // ���� �Ǹ� �������� ��� ���� UI ����
        {
            priceText.text = string.Format("{0} Gold", price);
        }

        if (nameText != null)
        { 
            nameText.text = string.Format(itemName);
        }
        else
        { return; }
    }

    void OnCollisionEnter(Collision collision)
    {

        //if (collision.gameObject.tag.Equals("Floor"))
        //{
        //    rigid.isKinematic = true;
        //    sphereCollider.enabled = false;
        //}
    }

}
