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
    public Type type;       //저장할 변수 지정
    public enum WeaponType { Melee, Range }
    public WeaponType weaponType;       //저장할 변수 지정
    public int value;       //아이템 갯수나 값 등을 저장할 변수
    public int damage;       //아이템의 데미지를 저장할 변수
    public float rate;       //아이템 공격  속도를 저장할 변수
    public bool sellItem;   // 상점 판매 아이템 체크

    [HideInInspector] public int price;
    [Header("UI Setting")]
    public TMP_Text priceUI; 
    public GameObject UI;

    Rigidbody rigid;
    SphereCollider sphereCollider;

    void Awake()
    {
        if (itemNo != 0)    // 아이템의 값이 있을 때만 변경
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
                        this.tag = "Sell";  // 상점 판매 아이템 체크했을 경우 
                    }
                    this.value = itemDB.Entities[i].value;
                    this.price = itemDB.Entities[i].price;
                    //Debug.Log("아이템 넘버는 :" + itemNo  + "아이템 값은 :" + value);
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

        if (priceUI != null)    // 상점 판매 아이템일 경우 가격 UI 변경
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
