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

    public enum Type { Coin, Grenade, Heart, Weapon, Exit, Key}
    public Type type;       //������ ���� ����
    public enum WeaponType { Melee, Range }
    public WeaponType weaponType;       //������ ���� ����
    public int value;       //������ ������ �� ���� ������ ����
    public int damage;       //�������� �������� ������ ����
    public float rate;       //������ ����  �ӵ��� ������ ����
    public bool sellItem;   // ���� �Ǹ� ������ üũ

    [HideInInspector] public int price;
    [Header("UI Setting")]
    public TMP_Text priceUI; 
    public GameObject UI;

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
                    }
                    this.value = itemDB.Entities[i].value;
                    this.price = itemDB.Entities[i].price;
                    //Debug.Log("������ �ѹ��� :" + itemNo  + "������ ���� :" + value);
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

        if (priceUI != null)    // ���� �Ǹ� �������� ��� ���� UI ����
        {
            priceUI.text = string.Format("{0} Gold", price);
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
