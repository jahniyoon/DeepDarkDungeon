using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = default; // speed 를 default로 수정 : 지환

    Rigidbody PlayerRigid;     //리지드 바디 넣음 : 영수
    Animator animator;        //애니메이터 넣음 : 영수

    // Start is called before the first frame update
    void Start()
    {
        // 지환 수정함;

        PlayerRigid = GetComponent<Rigidbody>();    //리지드 불러옴 : 진
        animator = GetComponent<Animator>();        //애니메이터 불러옴 : 진
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk()
    {
        float playerSpeed;
        playerSpeed = speed;

    }
}
