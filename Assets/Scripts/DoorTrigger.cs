using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator animator;
   
    private void Start()
    {
        //animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsOpening", true);
            FindObjectOfType<AudioManager>().Play("DoorOpen");
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsOpening", false);
            FindObjectOfType<AudioManager>().Play("DoorClose");
        }
    }
    

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        foreach (Collider col in GetComponents<Collider>())
        {
            Gizmos.matrix = col.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }

    }
}
