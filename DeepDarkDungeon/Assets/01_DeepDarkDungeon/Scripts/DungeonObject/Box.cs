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
            Debug.Log("�ڽ� : �÷��̾��� ����� ��Ҵ�.");
            Weapon weapon = other.GetComponent<Weapon>();
            health -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;   //�˹�

        }

        if (health <= 0)
        {
            Vector3 originalPosition = transform.position; // ���� �ڽ��� ��ġ ����
            GameObject newBox = Instantiate(boxPrefab, originalPosition, Quaternion.identity);
            Destroy(gameObject); // ���� �ڽ� �ı�

        }

    }
   
}
