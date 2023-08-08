using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    public GameObject[] floors;

    private void OnEnable()
    {

        if (Random.Range(0, 2) == 0)
        {
            floors[0].SetActive(true);
            floors[1].SetActive(false);
        }
        else
        {
            floors[0].SetActive(false);
            floors[1].SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Floor"))
        {
            //Debug.Log("바닥 중복 발생! floorOverlap = " + DungeonCreator.floorOverlap);
            DungeonCreator.floorOverlap = true;
        }

    }


}
