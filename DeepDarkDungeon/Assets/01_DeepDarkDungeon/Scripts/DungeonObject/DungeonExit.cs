using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExit : MonoBehaviour
{
    GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
        //Door = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isDoorOpen)
        {

        }
    }
}
