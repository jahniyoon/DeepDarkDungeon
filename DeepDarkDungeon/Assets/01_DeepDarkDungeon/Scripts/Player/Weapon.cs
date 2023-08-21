using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int value;
    public enum WeaponType { Sword, ChainSaw, TwohandSword };
    public WeaponType weaponType;
    public int damage;
    public float rate;

    public BoxCollider meleeArea;

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

   
    IEnumerator SwordSwing()  // �ҵ� ������ ���
    {
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = true; //�ݶ��̴� Ȱ��ȭ

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;
    }
    // ! �� �ӵ��� ������ �ִϸ��̼� ���� �ӵ��� üũ�ؼ� ����ؾ��մϴ�.
    // ! ü�μҿ��� ��� ���� �ӵ��� �����⿡ �ݶ��̴� Ȱ��ȭ �ð��� ��¦ �����ϴ�.
    IEnumerator ChainSawSwing()      // ü�μҿ� ������ ���                   
    {
        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = true; //�ݶ��̴� Ȱ��ȭ

        yield return new WaitForSeconds(0.7f);
        meleeArea.enabled = false;
    }

}
