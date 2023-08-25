using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class DungeonObject : MonoBehaviour
{
    Animator animator;
    public enum Type { Deco, GoldDrop, Chest }
    public Type type;

    [Header("Object Setting")]
    public int health;                  // ������Ʈ�� ü��
    public GameObject brokePrefab;      // �μ����� ������ ������
    bool isBroken = false;              // �μ������� üũ


    [Header("Item Setting")]
    public int goldMaxValue;            // �ִ� ��� ��
    public GameObject goldPrefab;       // ��� ������
    public GameObject heartPrefab;      // ü�� ������
    public GameObject[] weaponPrefab;   // ���ڿ��� ���� ������ ������

    int weaponNum;

   

    public void Start()
    {
        animator = GetComponent<Animator>();
      
    }

    private void OnTriggerEnter(Collider other)
    {

            // ������Ʈ�� ü���� ���� ��� ü�� ���
            if (other.tag.Equals("Melee")) 
            {
                Weapon weapon = other.GetComponent<Weapon>();
                health -= weapon.damage;
                Vector3 reactVec = transform.position - other.transform.position;   //�˹�
            }
            if (other.tag.Equals("EnemyBullet"))
            {
                Bullet enemyBullet = other.GetComponent<Bullet>(); 
                health -= enemyBullet.damage;

                Vector3 reactVec = transform.position - other.transform.position;   //�˹�
            }
            // �μ����� ������ �� �μ����� ȿ���� ���� ��� �μ����� �ϱ�.
            if (health <= 0 && !isBroken && brokePrefab != null)    
            {
                        isBroken = true;

                    Vector3 originalPosition = transform.position; // ���� �ڽ��� ��ġ ����
                    Quaternion originalRotation = transform.rotation; // ���� �ڽ��� ���� ����
                    GameObject newObject = Instantiate(brokePrefab, originalPosition, originalRotation);
                     AudioManager.instance.PlaySFX("BoxBroken");

            // �μ��� �� ��� ��� Ÿ���̸� ��� ���
            if (type == Type.GoldDrop)  
                    {
                        int goldValue = Random.Range(0, goldMaxValue);    // ��� ����� �������� ��� ����
                            Vector3 goldPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z); // ���� �ڽ��� ��ġ ����

                        for (int i = 0; i <= goldValue; i++)
                        {
                            GameObject newGold = Instantiate(goldPrefab, goldPosition, originalRotation);
                            newGold.tag = "Item";
                        }

                        if (goldValue <= 2 && heartPrefab != null) // ������� 1 Ȯ���� ��Ʈ ����
                        {
                            GameObject newHeart = Instantiate(heartPrefab, goldPosition, originalRotation);
                            newHeart.tag = "Item";
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

    private IEnumerator SpawnGoldWithDelay(Vector3 position, Quaternion rotation)   // ���ڰ� ������ �����ð� �ڿ� ��� ����
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
    private IEnumerator SpawnWeaponWithDelay(Vector3 position, Quaternion rotation)  // ���ڰ� ������ �����ð� �ڿ� ���� ����
    {
        yield return new WaitForSeconds(0.8f);

        int randomNum = Random.Range(0, 11);
        if (randomNum == 0) // 10% Ȯ���� ������
        {
            weaponNum = 0;
        }
        else if (randomNum == 1) // 10% Ȯ���� ��ũ������
        {
            weaponNum = 1;
        }
        else if (2 <= randomNum && randomNum <= 4)
        {
            weaponNum = 2;
        }
        else if (5 <= randomNum && randomNum <= 7)
        {
            weaponNum = 3;
        }
        else if (8 <= randomNum && randomNum <= 10)
        {
            weaponNum = 4;
        }
        Vector3 weaponPosition = new Vector3(position.x, position.y - 0.2f, position.z);

        GameObject newWeapon = Instantiate(weaponPrefab[weaponNum], weaponPosition, rotation);
        newWeapon.tag = "Weapon";
        
    }

}
