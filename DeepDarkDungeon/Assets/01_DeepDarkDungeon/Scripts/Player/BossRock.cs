using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : Bullet
{
 
    Rigidbody rigid;
    float angularPower = 2f;  //회전파워
    float scaleValue = 0.1f;  //크기 숫자값
    bool isShoot;   //기를 모으고 쏘는 타이밍 관리 변수


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    IEnumerator GainPowerTimer()
    {
        yield return new WaitForSeconds(0.7f);   //기 모으는
        isShoot = true;  //쏜다
    }

    IEnumerator GainPower()
    {
        while (!isShoot)
        {

            angularPower += 0.3f;
            
            scaleValue += 0.08f;
            
            transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);  //계속 속도 올려야해서Acceleration


            yield return null;   
        }
    }

    

}

