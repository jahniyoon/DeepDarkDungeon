using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public float rate;

    public BoxCollider meleeArea;

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = true; //콜라이더 활성화

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;
    }
}
