using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    public float Hitpoints = 1;

    public Transform player;
    public float lookSpeed = 200;
    public float maxAngle = 45;
    public float maxAngleReset = 45;

    public Quaternion targetRotation;
    public Quaternion lookAt;

    public float Damage;
    public GameObject SkullImpactRef;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        targetRotation = Quaternion.LookRotation(direction);
        lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
        transform.rotation = lookAt;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<HUD>().Damage(Damage);

            Dead();
        }

        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            Hitpoints--;

            if (Hitpoints <= 0)
            {
                Dead();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HUD>().Damage(Damage);

            Dead();
        }

        if (collision.gameObject.CompareTag("PlayerHitBox"))
        {
            Hitpoints--;

            if (Hitpoints <= 0)
            {
                Dead();
            }
        }
    }

    private void Dead()
    {
        GameObject explosion = (GameObject)Instantiate(SkullImpactRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);

        Destroy(gameObject);
    }
}
