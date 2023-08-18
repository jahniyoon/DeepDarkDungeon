//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Fire : MonoBehaviour
//{

//    [SerializeField] private int damage;

//    [SerializeField] private float damageTime;
//    private float currentDamageTime;

//    [SerializeField] private float durationTime;
//    private float currentDurationTime;

//    [SerializeField] private ParticleSystem ps_Flame;

//    private bool isFire = true;

//    private Player player;

//    // Start is called before the first frame update
//    void Start()
//    {
//        currentDurationTime = durationTime;
//        player = FindObjectOfType<Player>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (isFire)
//        {
//            ElapseTime();
//        }
//    }

//    private void ElapseTime()
//    {
//        currentDurationTime -= Time.deltaTime;

//        if (currentDurationTime <= 0)
//            Off();


//        if (currentDamageTime > 0)
//            currentDamageTime -= Time.deltaTime;  // 1ÃÊ¿¡ 1¾¿


//    }

//    private void Off()
//    {
//        ps_Flame.Stop();
//        isFire = false;
//    }

//    private void OnTriggerStay(Collider other)
//    {
//        if (isFire && other.transform.tag == "Player")
//        {
//            if (currentDamageTime <= 0)
//            {
//                player.takedamage(damage);
//                currentDamageTime = damageTime;
//            }
//        }
//    }
//}
