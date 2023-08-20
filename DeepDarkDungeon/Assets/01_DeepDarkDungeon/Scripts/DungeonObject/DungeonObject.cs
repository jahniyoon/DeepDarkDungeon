using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class DungeonObject : MonoBehaviour
{
    Animator animator;
    public enum Type { Deco, GoldDrop, Chest }
    public Type type;

    [Header("Object Setting")]
    public int health;                  // 오브젝트의 체력
    public GameObject brokePrefab;      // 부서지면 생성될 프리팹
    bool isBroken = false;              // 부서졌는지 체크


    [Header("Item Setting")]
    public int goldMaxValue;            // 최대 골드 값
    public GameObject goldPrefab;       // 골드 프리팹
    public GameObject heartPrefab;      // 체력 프리팹
    public GameObject[] weaponPrefab;   // 상자에서 나올 아이템 프리팹


   

    public void Start()
    {
        animator = GetComponent<Animator>();
      
    }

    private void OnTriggerEnter(Collider other)
    {

            // 오브젝트에 체력이 있을 경우 체력 깎기
            if (other.tag.Equals("Melee")) 
            {
                Weapon weapon = other.GetComponent<Weapon>();
                health -= weapon.damage;
                Vector3 reactVec = transform.position - other.transform.position;   //넉백
            }

            // 부서지는 아이템 중 부서지는 효과가 있을 경우 부서지게 하기.
            if (health <= 0 && !isBroken && brokePrefab != null)    
                {
                    isBroken = true;

                Vector3 originalPosition = transform.position; // 기존 박스의 위치 저장
                Quaternion originalRotation = transform.rotation; // 기존 박스의 각도 저장
                GameObject newObject = Instantiate(brokePrefab, originalPosition, originalRotation);

                // 부서질 때 골드 드롭 타입이면 골드 드롭
                if (type == Type.GoldDrop)  
                    {
                    int goldValue = Random.Range(0, goldMaxValue);    // 골드 밸류중 랜덤으로 골드 생성
                        Vector3 goldPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z); // 기존 박스의 위치 저장

                    for (int i = 0; i <= goldValue; i++)
                    {
                        GameObject newGold = Instantiate(goldPrefab, goldPosition, originalRotation);
                        newGold.tag = "Item";
                    }

                    if (goldValue == 0 && heartPrefab != null) // 골드밸류의 1 확률로 하트 생성
                    {
                        GameObject newHeart = Instantiate(heartPrefab, goldPosition, originalRotation);
                        newHeart.tag = "Item";
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

    private IEnumerator SpawnGoldWithDelay(Vector3 position, Quaternion rotation)   // 상자가 열리는 일정시간 뒤에 골드 스폰
    {
        yield return new WaitForSeconds(0.8f);

        for (int i = 0; i <= goldMaxValue; i++)
        {
            GameObject newGold = Instantiate(goldPrefab, position, rotation);
            newGold.tag = "Item";
        }
        GameObject newHeart = Instantiate(heartPrefab, position, rotation);
        newHeart.tag = "Item";  
    }
    private IEnumerator SpawnWeaponWithDelay(Vector3 position, Quaternion rotation)  // 상자가 열리는 일정시간 뒤에 무기 스폰
    {
        yield return new WaitForSeconds(0.8f);

        int randomNum = Random.Range(0, 3);

        Vector3 weaponPosition = new Vector3(position.x, position.y - 0.2f, position.z);

        GameObject newWeapon = Instantiate(weaponPrefab[randomNum], weaponPosition, rotation);
        newWeapon.tag = "Weapon";
        
    }

}
