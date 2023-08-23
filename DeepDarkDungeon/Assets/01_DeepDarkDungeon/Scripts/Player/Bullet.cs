using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    public bool isRock;
    public bool isDestroy;
    int hp = 0;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))  //탄피  //!isRock 게임상에서 isRock체크해서 사라지지 않게
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
        else if(other.gameObject.tag.Equals("Player") && isDestroy)
        {
            hp++;
        }

        if (hp == 2)
        {
            Destroy(gameObject);

        }
    }



}