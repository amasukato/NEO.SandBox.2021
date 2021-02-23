using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte03 : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)

    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Porte03", true);

        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Porte03", false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
