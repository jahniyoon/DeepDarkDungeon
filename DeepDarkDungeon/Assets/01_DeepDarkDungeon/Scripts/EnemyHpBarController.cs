using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class EnemyHpBarController : MonoBehaviour
{
    public Slider hpSlider;
    public TMP_Text hpText;
    public EnemyTest enemy;
    public Monster enemyHealth;

    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
        //ant = FindObjectOfType<AntController>();

        hpSlider = GetComponent<Slider>();
        if (hpSlider != null && hpSlider.gameObject.activeSelf) // Hpslider가 있는지 확인 및 게임 오브젝트 활성화 여부 확인
        {
            hpSlider.minValue = 0;
        }
    }

    void Update()
    {
        if (hpSlider != null && hpSlider.gameObject.activeSelf) // Hpslider가 있는지 확인 및 게임 오브젝트 활성화 여부 확인
        {
            hpSlider.transform.LookAt(hpSlider.transform.position +
            cam.rotation * Vector3.forward,
            cam.rotation * Vector3.up);

            if (enemy != null)
            {
                hpSlider.maxValue = enemy.maxHealth;
                hpSlider.value = enemy.curHealth;
                hpText.text = (enemy.curHealth.ToString() + "/" + enemy.maxHealth.ToString());

                if (hpSlider.value == 0)
                {
                    gameObject.SetActive(false);
                }
            }
            else if(enemyHealth != null)
            {
                hpSlider.maxValue = enemyHealth.maxHp;
                hpSlider.value = enemyHealth.hp;
                hpText.text = (enemyHealth.hp.ToString() + "/" + enemyHealth.maxHp.ToString());

                if (hpSlider.value == 0)
                {
                    gameObject.SetActive(false);
                }
            }
            
        }


        

    }
}
