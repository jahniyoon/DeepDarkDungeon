using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Coin, Grenade, Heart, Weapon }     
    public Type type;                                     //저장할 변수 지정
    public int value;                                     //아이템 갯수나 값 등을 저장할 변수

    Rigidbody rigid;
    SphereCollider sphereCollider;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        //sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime);
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
