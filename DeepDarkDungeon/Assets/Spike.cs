using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Animator animator;
    bool isActive = false;
    public float rate;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive && other.tag.Equals("Player") || other.tag.Equals("Enemy"))
        {
            isActive = true;
            animator.SetBool("isActive", isActive);
            Invoke("SpikeReset", rate);
        }
    }


    public void SpikeReset()
    {
        isActive = false;
        animator.SetBool("isActive", isActive);
    }
}
