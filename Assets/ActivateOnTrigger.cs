using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTrigger : MonoBehaviour
{

    public GameObject OnTrigger;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {


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
