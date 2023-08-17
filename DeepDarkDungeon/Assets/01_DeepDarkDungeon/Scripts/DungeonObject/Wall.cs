using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Destroy"))
        {
            //Debug.Log("출구와 만났다.");

            Destroy(gameObject);
            //Debug.Log("벽 충돌");
        }
    }
}


