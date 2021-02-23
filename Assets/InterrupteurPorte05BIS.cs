using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterrupteurPorte05BIS : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("luffy", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("luffy", false);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
