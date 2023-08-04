using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    public GameObject[] floors;

    private void OnEnable()
    {
        bool floorActive = false;  // 활성화 되었는지 체크

        for (int i = 0; i < floors.Length; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                floors[i].SetActive(true);
                floorActive = true;
            }
            else
            floors[i].SetActive(false);
        }

        if (!floorActive && floors.Length > 0);
        {
            floors[Random.Range(0, floors.Length)].SetActive(true);
        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
