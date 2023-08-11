using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonObject : MonoBehaviour
{
    public int health;
    public GameObject[] dungeonObject;
    public GameObject brokePrefab;
    public GameObject goldPrefab;
    public int goldValue;
    public enum Type { Deco, GoldDrop }
    public Type type;

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
            Vector3 originalPosition = transform.position; // 기존 박스의 위치 저장
            GameObject newObject = Instantiate(brokePrefab, originalPosition, Quaternion.identity);

            if (type == Type.GoldDrop)
            {
                for (int i = 0; i <= goldValue; i++)
                {
                    GameObject newGold = Instantiate(goldPrefab, originalPosition, Quaternion.identity);
                    newGold.tag = "Item";

                }
            }

            Destroy(gameObject); // 기존 오브젝트 파괴

        }

    }
   
}
