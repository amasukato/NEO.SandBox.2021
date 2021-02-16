using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    iddle,
    walk,
    attack,
    interact,
    hurt
}

public class CharController : MonoBehaviour
{

    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public CharacterController controller;

    public PlayerState currentState;

    //Stats
    private float gravity = -9.81f;
    public float moveSpeed = 4f;
    public float dashSpeed;
    public float dashTime;
    public float dashCD;
    private float JumpForce;

    //States

    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool IsOnTheGround;

    public float turnSmoothTime;
    float turnSmoothVelocity;

    // Dash & Movement
    public Vector3 movDir;


    //Attack
    public Collider[] attackHitboxes;

    void Start()
    {
        currentState = PlayerState.iddle;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

    }


    // Update is called once per frame
    void Update()
    {       
        
        IsOnTheGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(IsOnTheGround && movDir.y <=0)
        {
            movDir.y = -2f;
        }

        Move();
        /*
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                controller.Move(movDir.normalized * moveSpeed * Time.deltaTime);

            }

        */

        movDir.y += gravity * Time.deltaTime;
        //controller.Move(movDir * Time.deltaTime); // pas besoin de gravité réaliste donc pas besoin de doubler

        if (Input.GetButtonDown("Fire1"))
        {
            //Attack();
        }

        dashCD -= Time.deltaTime;
        if (Input.GetButtonDown("Fire2"))
        {
            if (dashCD <= 0)
            {
                StartCoroutine(Dash());
            }

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
            dashCD = 0.3f;

            yield return null;
        }
    }

}
