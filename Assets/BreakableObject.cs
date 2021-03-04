using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject fractured;
    public GameObject particleRef;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerHitBox"))
        {
            Breaking();
        }
    }

    public void Breaking()
    {
        Instantiate(fractured, transform.position, transform.rotation);

        GameObject explosion = (GameObject)Instantiate(particleRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        Destroy(gameObject);
    }
}
