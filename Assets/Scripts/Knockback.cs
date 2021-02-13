using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockBackForce;
    public float knockTime = 2;
    //public Combatant stats;
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HitBox"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = transform.position - collision.transform.position;
                direction.y = 0;


                rb.AddForce(direction.normalized * knockBackForce, ForceMode.Impulse);
            }
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag ("HitBox"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 difference = transform.position - other.transform.position;
                difference = difference.normalized * knockBackForce;
                difference.y = 0;
                rb.AddForce(difference, ForceMode.Impulse);
                StartCoroutine(KnockCo(rb));

            }
        }
    }

    private IEnumerator KnockCo (Rigidbody rb)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector3.zero;
        }
    }
}
