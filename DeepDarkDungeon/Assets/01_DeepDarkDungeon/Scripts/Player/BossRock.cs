using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : Bullet
{
 
    Rigidbody rigid;
    float angularPower = 2f;  //ȸ���Ŀ�
    float scaleValue = 0.1f;  //ũ�� ���ڰ�
    bool isShoot;   //�⸦ ������ ��� Ÿ�̹� ���� ����


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    IEnumerator GainPowerTimer()
    {
        yield return new WaitForSeconds(0.7f);   //�� ������
        isShoot = true;  //���
    }

    IEnumerator GainPower()
    {
        while (!isShoot)
        {

            angularPower += 0.3f;
            
            scaleValue += 0.08f;
            
            transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);  //��� �ӵ� �÷����ؼ�Acceleration


            yield return null;   
        }
    }

    

}

