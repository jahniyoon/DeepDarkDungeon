using UnityEngine.AI;
using UnityEngine;

public class BossBullet : Bullet
{

    public Transform target;
    public Transform playerTr;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(playerTr.position);
        Destroy(gameObject, 8f);
    }
}