using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte01script : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Openporte", true);
        }
     
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Openporte", false);
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
