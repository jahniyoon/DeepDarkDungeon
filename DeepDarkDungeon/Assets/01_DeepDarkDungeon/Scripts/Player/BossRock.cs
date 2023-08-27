using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

            angularPower += 0.5f;
            
            scaleValue += 0.05f;
            
            transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);  //��� �ӵ� �÷����ؼ�Acceleration


            yield return null;   
        }
    }
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))  //ź��  //!isRock ���ӻ󿡼� isRocküũ�ؼ� ������� �ʰ�
        {
            Destroy(gameObject, 2.0f);
        }


    }


    public override void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }

        
    }

}

