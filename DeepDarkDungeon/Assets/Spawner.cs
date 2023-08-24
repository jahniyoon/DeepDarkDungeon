using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;    //random 모호하다고 에러 나올때


public class Spawner : MonoBehaviour
{
    public Transform playerTr;

    public GameObject shuriken;
    
    public Transform spawnA;
    
    public float spawnRateMin = default;
    public float spawnRateMax = default;

    //public Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        //target = FindObjectOfType<Player>().transform;

        timeAfterSpawn = 0f;

        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTr.position);

        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn > spawnRate)
        {
            timeAfterSpawn = 0f;

            Vector3 arrowPosition = transform.position;
            //arrowPosition.y = 4.0f;

            GameObject instantSpawnA = Instantiate(shuriken, spawnA.position, spawnA.rotation);
            

            instantSpawnA.transform.LookAt(playerTr.position);
            
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
