using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuvrirLaPorte : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("OpenDoor", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("OpenDoor", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
