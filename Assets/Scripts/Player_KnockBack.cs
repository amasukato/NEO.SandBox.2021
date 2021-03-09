using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_KnockBack : MonoBehaviour
{

    public float thrust;
    public float knockTime;
    //public CameraController cameraShake;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemy = other.gameObject.GetComponent<Rigidbody>();
            if (enemy != null)
            {
                enemy.GetComponent<EnemyAI>().currentState = EnemyState.knockback;
                enemy.isKinematic = false;
                Vector3 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode.Impulse);
                StartCoroutine(KnockCO(enemy));

                //StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
            }
        }
    }

    private IEnumerator KnockCO(Rigidbody enemy)
    {
        yield return new WaitForSeconds(knockTime);
        if (enemy != null)
        {
            enemy.velocity = Vector3.zero;
            enemy.isKinematic = true;
            enemy.GetComponent<EnemyAI>().currentState = EnemyState.idle;
        }

    }
}
