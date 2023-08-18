using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class DungeonObject : MonoBehaviour
{
    public int health;
    public GameObject brokePrefab;
    public GameObject goldPrefab;
    public GameObject[] weaponPrefab;
    public int goldValue;

    Animator animator;

    public enum Type { Deco, GoldDrop, Chest }
    public Type type;
    bool isBroken = false;

    public void Start()
    {
        animator = GetComponent<Animator>();

      
    }

    private void OnTriggerEnter(Collider other)
    {
        
            if (other.tag.Equals("Melee"))
            {
                Weapon weapon = other.GetComponent<Weapon>();
                health -= weapon.damage;
                Vector3 reactVec = transform.position - other.transform.position;   //�˹�



            }

            if (health <= 0 && !isBroken && brokePrefab != null)
            {
                isBroken = true;

                Vector3 originalPosition = transform.position; // ���� �ڽ��� ��ġ ����
                Quaternion originalRotation = transform.rotation; // ���� �ڽ��� ���� ����
                GameObject newObject = Instantiate(brokePrefab, originalPosition, originalRotation);

                if (type == Type.GoldDrop)
                {
                    for (int i = 0; i <= goldValue; i++)
                    {
                    Vector3 goldPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z); // ���� �ڽ��� ��ġ ����
                    GameObject newGold = Instantiate(goldPrefab, goldPosition, originalRotation);
                        newGold.tag = "Item";
                    }
                }
                Destroy(gameObject); // ���� ������Ʈ �ı�
            }
        

    }

    public void ChestOpen()
    {
        Debug.Log("���ڰ� ������?");

        animator.SetTrigger("Open");
        Vector3 originalPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z); // ���� �ڽ��� ��ġ ����
        Quaternion originalRotation = transform.rotation; // ���� �ڽ��� ���� ����

        if (goldPrefab != null) // ��� ������ �ִ� ���
        {
            StartCoroutine(SpawnGoldWithDelay(originalPosition, originalRotation));
        }

        if (weaponPrefab != null)   // ���� �������� �ִ� ���
        {
            StartCoroutine(SpawnWeaponWithDelay(originalPosition, originalRotation));
        }

    }

    private IEnumerator SpawnGoldWithDelay(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(0.8f);

        for (int i = 0; i <= goldValue; i++)
        {
            GameObject newGold = Instantiate(goldPrefab, position, rotation);
            newGold.tag = "Item";
        }
    }
    private IEnumerator SpawnWeaponWithDelay(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(0.8f);

        int randomNum = Random.Range(0, 3);

        GameObject newWeapon = Instantiate(weaponPrefab[randomNum], position, rotation);
        newWeapon.tag = "Weapon";
        
    }

}
