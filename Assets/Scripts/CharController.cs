using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public CharacterController controller;

    //Stats
    public float moveSpeed = 4f;
    public float dashSpeed;
    public float dashTime;
    public float JumpForce;

    //States
    public bool IsOnTheGround = true;
    public float turnSmoothTime;
    float turnSmoothVelocity;

    // Dash & Movement
    public Vector3 movDir;


    //Attack
    public Collider[] attackHitboxes;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
            Move();


        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Dash());
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move( movDir.normalized * moveSpeed * Time.deltaTime);

        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            controller.Move(movDir* dashSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsOnTheGround = true;
        }
    }
}
