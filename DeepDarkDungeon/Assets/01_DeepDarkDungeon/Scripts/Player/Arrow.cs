using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Arrow : MonoBehaviour
{
    private Rigidbody rigid;
    public float speed = 2.0f;
    public int damage = 5;
    public GameObject particle;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = transform.forward * speed;

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 3.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall") || other.tag.Equals("Player"))
        {

            GameObject fireParticle = Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);

        }

    }
}
