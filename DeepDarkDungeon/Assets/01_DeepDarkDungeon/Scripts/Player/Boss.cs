using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
   

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(!isPoison)
        //{
        //    StartCoroutine(Battle());
        //}
        
    }

    //IEnumerator Battle()
    //{
        
    //    isPoison = true;

    //    animator.SetTrigger("isPosion");

    //    yield return new WaitForSeconds(2f);

    //    yield return new WaitForSeconds(2f);

    //    animator.SetTrigger("isPosion");

    //    isPoison = false;
    //}
}
