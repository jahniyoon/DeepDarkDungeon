using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class Weapon : MonoBehaviour
{
    [SerializeField]
    private ItemDB itemDB;
    [SerializeField]
    private int itemNo;
    public int value;
    public enum WeaponType { Sword, ChainSaw, TwohandSword };
    public WeaponType weaponType;
    public int damage;
    public float rate;

    public BoxCollider meleeArea;

    private void Awake()
    {
        if (itemNo != 0)    // 아이템의 값이 있을 때만 변경
        {
            for (int i = 1; i < itemDB.Entities.Count; i++)
            {
                if (itemDB.Entities[i].itemNum == itemNo)
                {
                    this.weaponType = (WeaponType)itemDB.Entities[i].weaponType;
                    this.damage = itemDB.Entities[i].damage;
                    this.rate = itemDB.Entities[i].rate;
                }
            }
        }
    }
    public void Use()
    {
        if(weaponType == WeaponType.Sword || weaponType == WeaponType.TwohandSword)
        {
            StopCoroutine("SwordSwing");
            StartCoroutine("SwordSwing");
        }
        else if (weaponType == WeaponType.ChainSaw )
        {
            StopCoroutine("ChainSawSwing");
            StartCoroutine("ChainSawSwing");
        }
    }

   
    IEnumerator SwordSwing()  // 소드 스윙일 경우
    {
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = true; //콜라이더 활성화

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;
    }
    // ! 각 속도는 무기의 애니메이션 별로 속도를 체크해서 사용해야합니다.
    // ! 체인소우의 경우 공격 속도가 느리기에 콜라이더 활성화 시간이 살짝 느립니다.
    IEnumerator ChainSawSwing()      // 체인소우 스윙일 경우                   
    {
        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = true; //콜라이더 활성화

        yield return new WaitForSeconds(0.7f);
        meleeArea.enabled = false;
    }

}
