using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonObject : MonoBehaviour
{
    public int health;
    public GameObject[] dungeonObject;
    public GameObject brokePrefab;



    public void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Melee"))
        {
            Debug.Log("�ڽ� : �÷��̾��� ����� ��Ҵ�.");
            Weapon weapon = other.GetComponent<Weapon>();
            health -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;   //�˹�

        }

        if (health <= 0)
        {
            Vector3 originalPosition = transform.position; // ���� �ڽ��� ��ġ ����
            GameObject newObject = Instantiate(brokePrefab, originalPosition, Quaternion.identity);
            Destroy(gameObject); // ���� ������Ʈ �ı�

        }

    }
   
}
