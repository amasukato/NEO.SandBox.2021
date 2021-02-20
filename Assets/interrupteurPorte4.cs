using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterrupteurPorte4 : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
      if ( other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("sesame", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("sesame", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
