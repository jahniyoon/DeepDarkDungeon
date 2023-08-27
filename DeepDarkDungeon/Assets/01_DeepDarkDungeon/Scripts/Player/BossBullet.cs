using UnityEngine.AI;
using UnityEngine;

public class BossBullet : Bullet
{

    public Transform target;
    public Transform playerTr;
    NavMeshAgent nav;
    public bool isArrow;
    public Rigidbody rigid;
    public float speed = 2.0f;
    

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
        if(isDestroy)
        {
            nav.SetDestination(playerTr.position);

            Destroy(this.gameObject, 5.0f);
        }
      


    }

    

}