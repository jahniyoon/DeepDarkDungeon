using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int health;
    public GameObject[] box;
    public GameObject boxPrefab;



    public void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Melee"))
        {
            Debug.Log("박스 : 플레이어의 무기와 닿았다.");
            Weapon weapon = other.GetComponent<Weapon>();
            health -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;   //넉백

        }

        if (health <= 0)
        {
            Vector3 originalPosition = transform.position; // 원래 박스의 위치 저장
            GameObject newBox = Instantiate(boxPrefab, originalPosition, Quaternion.identity);
            Destroy(gameObject); // 원래 박스 파괴

        }

    }
   
}
