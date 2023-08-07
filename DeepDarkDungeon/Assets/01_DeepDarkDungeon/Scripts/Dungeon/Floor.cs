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

}
