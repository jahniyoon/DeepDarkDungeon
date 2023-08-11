using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    public ParticleSystem particle;

    public bool isLook;

    Vector3 lookVec;     //�÷��̾� ������ ���� ���� ����


    // Start is called before the first frame update
    void Awake()   //awake�Լ��� �ڽ� ��ũ��Ʈ�� �ܵ� ���� enemy awke��ũ��Ʈ ��� �ȵ�
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();  // Material�� MeshRenderer�� �����;ߵȴ�
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        StartCoroutine(Think());
    }


    // Update is called once per frame
    void Update()
    {
        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;   //�÷��̾� �Է°����� ���� ���Ͱ� ����
            transform.LookAt(target.position + lookVec);
        }
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 5);
        switch (ranAction)
        {
            case 0:
            case 1:
                StartCoroutine(MissileShot());
                break;
            case 2:
            case 3:
                //�� �������� ����
                break;
            case 4:
                //���� ���� ����
                break;

        }

    }

    IEnumerator MissileShot()
    {
        anim.SetTrigger("doShot");
        yield return new WaitForSeconds(0.2f);
        //GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        //BossMissile bossMissileA = instantMissileA.GetComponent<BossMissile>();     //�̻��� ��ũ��Ʈ���� �����Ͽ� ��ǥ�� ����(����)
        //bossMissileA.target = target;

        //yield return new WaitForSeconds(0.3f);
        //GameObject instantMissileB = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        //BossMissile bossMissileB = instantMissileA.GetComponent<BossMissile>();     //�̻��� ��ũ��Ʈ���� �����Ͽ� ��ǥ�� ����(����)
        //bossMissileA.target = target;

        //yield return new WaitForSeconds(2f);

        //StartCoroutine(Think());
    }

}
