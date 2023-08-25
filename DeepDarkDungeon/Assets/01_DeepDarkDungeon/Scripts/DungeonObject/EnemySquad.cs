using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquad : MonoBehaviour
{

    public GameObject[] eliteEnemies;
    public GameObject[] enemies;
    // Start is called before the first frame update


    private void OnEnable()
    {
        int eliteNum = Random.Range(0, eliteEnemies.Length);
        eliteEnemies[eliteNum].SetActive(true);

        int enemyNum = Random.Range(0, 3);
        enemies[enemyNum].SetActive(true);
    }

}
