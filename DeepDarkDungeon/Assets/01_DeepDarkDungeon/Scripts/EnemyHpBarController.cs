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

    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
        //ant = FindObjectOfType<AntController>();

        hpSlider = GetComponent<Slider>();
        if (hpSlider != null && hpSlider.gameObject.activeSelf) // Hpslider�� �ִ��� Ȯ�� �� ���� ������Ʈ Ȱ��ȭ ���� Ȯ��
        {
            hpSlider.minValue = 0;
        }
    }

    void Update()
    {
        if (hpSlider != null && hpSlider.gameObject.activeSelf) // Hpslider�� �ִ��� Ȯ�� �� ���� ������Ʈ Ȱ��ȭ ���� Ȯ��
        {
            hpSlider.transform.LookAt(hpSlider.transform.position +
            cam.rotation * Vector3.forward,
            cam.rotation * Vector3.up);

            hpSlider.maxValue = enemy.maxHealth;
            hpSlider.value = enemy.curHealth;
            hpText.text = (enemy.curHealth.ToString() + "/" + enemy.maxHealth.ToString());

            if (hpSlider.value == 0)
            {
                gameObject.SetActive(false);

            }
        }

    }
}
