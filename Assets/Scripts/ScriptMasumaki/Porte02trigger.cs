using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte02trigger : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("naruto", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("naruto", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
