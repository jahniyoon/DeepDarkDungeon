using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnroomChest : MonoBehaviour
{
    public GameObject spawnChest;
    // Start is called before the first frame update
    void Start()
    {
        if (spawnChest != null)
        {
            if (GameData.floor == 1)
            {
                spawnChest.SetActive(true);
            }
            else
                spawnChest.SetActive(false);
        }
    }


}
