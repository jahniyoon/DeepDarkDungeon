using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rigid;
    public float speed = 2.0f;
    public int damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = transform.forward * speed;

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
