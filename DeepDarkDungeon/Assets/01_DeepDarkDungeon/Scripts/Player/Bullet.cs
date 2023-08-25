using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    public bool isRock;
    public bool isDestroy;
    public GameObject particle;
    int hp = 0;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))  //ź��  //!isRock ���ӻ󿡼� isRocküũ�ؼ� ������� �ʰ�
        {
            Destroy(gameObject, 5.0f); 
        }
        
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (isMelee && other.gameObject.layer == LayerMask.NameToLayer("Wall") || other.tag.Equals("Player"))
        {

            GameObject fireParticle = Instantiate(particle, transform.position, transform.rotation);
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