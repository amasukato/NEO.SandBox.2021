using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractForce : MonoBehaviour
{
    public float AttractSpeed;

    void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, AttractSpeed * Time.deltaTime);
        }
    }

    void Update()
    {
        if (transform.childCount < 1)
        {
            Destroy(gameObject);
        }
    }

}
