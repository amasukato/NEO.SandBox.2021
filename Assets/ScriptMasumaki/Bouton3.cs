using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouton3 : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Open", true);
        }          
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Open", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
