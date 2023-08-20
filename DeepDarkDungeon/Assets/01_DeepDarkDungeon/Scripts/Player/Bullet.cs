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
        if (collision.gameObject.tag.Equals("Floor"))  //ź��  //!isRock ���ӻ󿡼� isRocküũ�ؼ� ������� �ʰ�
        {
            Destroy(gameObject, 5.0f); 
        }
        
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (isMelee && other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);

        }
        else if(other.gameObject.tag.Equals("Player"))
        {
            //Destroy(gameObject);
        }
    }



}