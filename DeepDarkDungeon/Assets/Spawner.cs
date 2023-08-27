using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;    //random 모호하다고 에러 나올때


public class Spawner : MonoBehaviour
{
    public Transform playerTr;

    public GameObject shurikenPrefab;
    
    public Transform spawnA;
    
    public float spawnRateMin = default;
    public float spawnRateMax = default;

    //public Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    bool isAttack;

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
        if (isAttack)
        {
            transform.LookAt(playerTr.position);

            timeAfterSpawn += Time.deltaTime;

            if (timeAfterSpawn > spawnRate)
            {
                timeAfterSpawn = 0f;

                Vector3 Shuriken = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                //Shuriken.y = 1.0f;
                //arrowPosition.y = 4.0f;

                GameObject instantSpawnA = Instantiate(shurikenPrefab, spawnA.position, spawnA.rotation);
                //GameObject shuriken = Instantiate(shurikenPrefab, Shuriken, transform.rotation);
                Rigidbody rigidSpawnA = instantSpawnA.GetComponent<Rigidbody>();
                rigidSpawnA.velocity = transform.forward * 3;

                instantSpawnA.transform.LookAt(playerTr.position);

                spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isAttack = false;
        }
    }
}
