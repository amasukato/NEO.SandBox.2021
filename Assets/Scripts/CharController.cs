using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
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
    public Object DashRef;
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

    // Grab & Thrown
    public Transform ObjectHolder;
    public float ThrowForce;
    public bool CarryObject;
    public GameObject Item;
    public bool IsThrowable;
    public float grabDistance = 2f;


    //Attack
    public Collider[] attackHitboxes;

    void Start()
    {
        currentState = PlayerState.idle;
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
              //GameObject DashVFX = (GameObject)Instantiate(DashRef);
              //DashVFX.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);

                StartCoroutine(Dash());
            }

        }

        //Grab();

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

    /*
    void Grab()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            RaycastHit hit;
            Ray directionRay = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(directionRay, out hit, grabDistance))
            {
                if (hit.collider.tag == "MobileObject")
                {
                    CarryObject = true;
                    IsThrowable = true;
                    {
                        if (CarryObject == true)
                        {
                            Item = hit.collider.gameObject;
                            Item.transform.SetParent(ObjectHolder);
                            Item.gameObject.transform.position = ObjectHolder.position;
                            Item.GetComponent<Rigidbody>().isKinematic = true;
                            Item.GetComponent<Rigidbody>().useGravity = false;
                        }
                    }
                }
                if (hit.collider.tag == "HookHolder")
                {
                    Item = hit.collider.gameObject;
                    Item.transform.SetParent(ObjectHolder);

                    movDir = transform.position + Item.transform.position;
                    controller.Move(movDir * dashSpeed * Time.deltaTime);
                }
            }

            if (Input.GetButtonUp("Fire3"))
            {
                CarryObject = false;
                IsThrowable = false;
                ObjectHolder.DetachChildren();
                if (Item.gameObject.tag != "MobileObject")
                {
                    StartCoroutine(Throw());
                }
            }
        }
    }
    
    IEnumerator Throw()
    {
        yield return null;
        Item.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce, ForceMode.Impulse);

    }

    */
}
