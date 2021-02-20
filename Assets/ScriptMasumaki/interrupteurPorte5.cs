using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interrupteurPorte5 : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("sangoku", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("sangoku", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
