using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    public bool isRock;     

    void OnCollisionEnter(Collision collision)
    {
        if (!isRock && collision.gameObject.tag.Equals("Floor"))  //ź��  //���ӻ󿡼� isRocküũ�ؼ� ������� �ʰ�
        {
            Destroy(gameObject, 10); 
        }
        
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (isMelee && other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);

        }
    }



}