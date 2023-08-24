using UnityEngine.AI;
using UnityEngine;

public class BossBullet : Bullet
{

    public Transform target;
    public Transform playerTr;
    NavMeshAgent nav;
    public bool isShuriken;
    private Rigidbody rigid;

    public float desiredRotationAngle = 90f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       nav.SetDestination(playerTr.position);
       

        if (isShuriken)
        {
            float additionalRotation = desiredRotationAngle - transform.eulerAngles.x;
            transform.Rotate(additionalRotation, 0, 0);

            Destroy(gameObject, 3.5f);
        }
        else
        {
            Destroy(gameObject, 8f);
        }


    }
}