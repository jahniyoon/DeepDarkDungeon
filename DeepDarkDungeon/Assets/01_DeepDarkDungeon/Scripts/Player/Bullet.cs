using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))  //ÅºÇÇ
        {
            Destroy(gameObject, 3);
        }
        else if (collision.gameObject.tag.Equals("Wall"))  //ÃÑ¾Ë
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Wall"))
        {
            Destroy(gameObject);

        }
    }
}