using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken : MonoBehaviour
{

    public float Hitpoints = 1;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerHitBox"))
        {
            Hitpoints--;
            FindObjectOfType<AudioManager>().Play("HitRock");

            if (Hitpoints <= 0)
            {
                Breaking();
            }

        }
    }

    public void Breaking()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigidbodies.Length; i++)
            rigidbodies[i].constraints = RigidbodyConstraints.None;

        FindObjectOfType<AudioManager>().Play("Broken_Wall");

        Collider col = GetComponent<Collider>();
        col.enabled = false;

        StartCoroutine(Disappear());

    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3.3f);

        Destroy(gameObject);
    }
}
