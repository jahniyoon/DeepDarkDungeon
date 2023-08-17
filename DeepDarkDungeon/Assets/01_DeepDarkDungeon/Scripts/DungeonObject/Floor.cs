using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    public GameObject[] floors;

    private void OnEnable()
    {
        int floor = Random.Range(0, 4);

        if (floor == 0)
        {
            floors[0].SetActive(false);
            floors[1].SetActive(true);
            floors[2].SetActive(false);
        }
        else if (floor == 1)
        {
            floors[0].SetActive(false);
            floors[1].SetActive(false);
            floors[2].SetActive(true);
        }
        else
        {
            floors[0].SetActive(true);
            floors[1].SetActive(false);
            floors[2].SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Floor"))
        {
            //Debug.Log("바닥 중복 발생! floorOverlap = " + DungeonCreator.floorOverlap);
            DungeonCreator.floorOverlap = true;
        }
        if (other.tag.Equals("Finish"))
        {
            floors[0].SetActive(false);
            floors[1].SetActive(false);

        }

    }


}
