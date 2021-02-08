using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody controller;

    public float speed;
    public float turnSpeed;

    public Combatant stats;
    public float knockbackEnd = 0.1f;


    private void Start()
    {
        stats.TakeDamage += DamageEvent;
        controller = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        switch(stats.status)
        {
            case Status.Normal:
                CanMove();

                break;

            case Status.Knockback:
                Knockbacked();

                break;

            default:
                break;
        }


        //heatlhText.text = stats.health.ToString();

        if (stats.health <= 0)
        {

        }


    }

    private void CanMove()
    {
        float vInput = Input.GetAxisRaw("Vertical");
        float hInput = Input.GetAxisRaw("Horizontal");

        Vector3 direction = new Vector3(hInput, 0f, vInput).normalized;


        if (direction.magnitude <= 0.1)
            if (direction.magnitude >= 0.1)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, 1f);

                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                controller.velocity = direction * speed * Time.deltaTime;
            }
                /*if (Mathf.Abs(vInput) < .1f && Mathf.Abs(hInput) < .1f)
                {
                    //add animation.Moving
                    // animation.Moving = false

                }
                else
                {
                    controller.velocity = (transform.forward * speed * vInput);
                    if (Mathf.Abs(hInput) > 0.1f)
                    {
                       // controller.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                        transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime * hInput));
                    }
                    else
                    {
                        //controller.constraints = RigidbodyConstraints.FreezeRotation;
                    }
                    // animation.Moving = true
                }*/

                if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }

    private void DamageEvent(Combatant attacker, Combatant defender)
    {
        stats.status = Status.Knockback;

        Vector3 direction = (defender.transform.position - attacker.transform.position).normalized;
        float knockbackSpeed = Mathf.Max(attacker.knockbackForce - defender.knockbackResist, 0);

        controller.velocity = direction * knockbackSpeed;
    }

    private void Knockbacked()
    {
        if (Mathf.Abs(controller.velocity.x) < knockbackEnd && Mathf.Abs(controller.velocity.z) < knockbackEnd)
        {
            stats.status = Status.Normal;
        }
    }
}
