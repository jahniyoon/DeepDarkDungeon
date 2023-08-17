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
                Vector3 reactVec = transform.position - other.transform.position;   //넉백



            }

            if (health <= 0 && !isBroken && brokePrefab != null)
            {
                isBroken = true;

                Vector3 originalPosition = transform.position; // 기존 박스의 위치 저장
                Quaternion originalRotation = transform.rotation; // 기존 박스의 각도 저장
                GameObject newObject = Instantiate(brokePrefab, originalPosition, originalRotation);

                if (type == Type.GoldDrop)
                {
                    for (int i = 0; i <= goldValue; i++)
                    {
                    Vector3 goldPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z); // 기존 박스의 위치 저장
                    GameObject newGold = Instantiate(goldPrefab, goldPosition, originalRotation);
                        newGold.tag = "Item";
                    }
                }
                Destroy(gameObject); // 기존 오브젝트 파괴
            }
        

    }

    public void ChestOpen()
    {
        Debug.Log("상자가 열리나?");

        animator.SetTrigger("Open");
        Vector3 originalPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z); // 기존 박스의 위치 저장
        Quaternion originalRotation = transform.rotation; // 기존 박스의 각도 저장

        if (goldPrefab != null) // 골드 프리팹 있는 경우
        {
            StartCoroutine(SpawnGoldWithDelay(originalPosition, originalRotation));
        }

        if (weaponPrefab != null)   // 무기 프리팹이 있는 경우
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
