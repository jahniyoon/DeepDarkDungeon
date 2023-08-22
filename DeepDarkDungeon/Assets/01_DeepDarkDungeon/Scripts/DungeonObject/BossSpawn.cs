using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject[] bossList;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameData.floor % 2 == 0)
        {
            bossList[0].SetActive(false);
            bossList[1].SetActive(true);
            Debug.Log("府摹 积己");
        }
        else
        {
            bossList[0].SetActive(true);
            bossList[1].SetActive(false);
            Debug.Log("侩侩捞 积己");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
