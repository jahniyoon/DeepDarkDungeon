using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Destroy"))
        {
            //Debug.Log("�ⱸ�� ������.");

            Destroy(gameObject);
            //Debug.Log("�� �浹");
        }
    }
}


