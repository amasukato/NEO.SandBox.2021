using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockBackForce;
    //public Combatant stats;

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
}
