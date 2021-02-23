using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DéDéTrigger : MonoBehaviour
{
    public Animator animator;

    
   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("OpeningOrClosing", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("OpeningOrClosing", false);
        }
    }
}
