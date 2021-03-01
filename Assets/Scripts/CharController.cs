using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharController : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public MeshRenderer mr;
    [HideInInspector] public CharacterController controller;

    //Effect
    [SerializeField] private Animator anim1;
    [SerializeField] private Object DashRef;
    [SerializeField] private Object GetHitRef;
    [SerializeField] private Object PlayerDeadRef;
    [SerializeField] private Material matWhite;
    private Material matDefault;
    public GrabThrow grabthrow;

    //Stats
    public float HitPoints;
    public float MaxHitPoints;

    private float gravity = -9.81f;
    public float moveSpeed = 4f;
    private float dashSpeed = 20;
    public float dashTime = 0.25f;
    public float dashCD = 0.3f;

    //States

    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool IsOnTheGround;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    private float FreezeTime = 0.4f; // Duration of stop moving during Attack

    // Dash & Movement
    public Vector3 movDir;

    public State PlayerState = State.Idle;

    public enum State
    {
        Idle,
        IdleRecovery,
        Moving,
        Attacking,
        Dashing,
        Grabing,
        Throwing,
        LaunchHook,
        OnHook,
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim1 = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        mr = GetComponent<MeshRenderer>();
        //matWhite = Resources.Load("White", typeof(Material)) as Material;
        matDefault = mr.material;
        grabthrow = GetComponent<GrabThrow>();

    }

    void Update()
    {
        
        switch (PlayerState)
        {
            default:
            case State.Idle:
                Move();
                Dashing();
                Attack();

                break;

            case State.IdleRecovery:
                Move();
                Dashing();
                Attack();

                break;

            case State.Attacking:
                Dashing();
                break;

            case State.Moving:
                Move();
                Dashing();

                break;

            case State.Dashing:
                Move();
                break;

            case State.Grabing:
                Move();
                break;

            case State.LaunchHook:

                break;

            case State.OnHook:

                break;

        }
        

        IsOnTheGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(IsOnTheGround && movDir.y <=0)
        {
            movDir.y = -2f;

        }

        if (movDir.x <= -2)
        {
            anim1.SetTrigger("idle1");
        }

        //Move();
        //Dashing();



        movDir.y += gravity * Time.deltaTime;
        //controller.Move(movDir * Time.deltaTime); // pas besoin de gravité réaliste donc pas besoin de doubler


        //Attack();

        dashCD -= Time.deltaTime;
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
        // animation movement
        if (horizontal == 0 && vertical == 0)
        {
            anim1.SetBool("Run1", false);
            PlayerState = State.Idle;
        }
        else
        {
            anim1.SetBool("Run1", true);
            PlayerState = State.Moving;
        }

    }

    void Attack()
    {
        /*
        if (Input.GetButtonDown("Fire1"))
        {
            // Attack Weapon Animation
            // Attack Pattern Character Animation
            anim1.Play("Standing 1H Magic Attack 01 0");
            PlayerState = State.Attacking;
        }

         PlayerState = State.Idle;
         */
    }

    void Dashing()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (dashCD <= 0)
            {
                //GameObject DashVFX = (GameObject)Instantiate(DashRef);
                //DashVFX.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);

                StartCoroutine(Dash());
                PlayerState = State.Dashing;
            }

        }

        IEnumerator Dash()
        {
            float startTime = Time.time;
            anim1.SetTrigger("Dash");

            while (Time.time < startTime + dashTime)
            {

                controller.Move(movDir * dashSpeed * Time.deltaTime);


                dashCD = 0.3f;

                yield return null;
            }
            PlayerState = State.Idle;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyHitBox"))
        {
                GetComponent<Health>().Damage(1);
                HitPoints--;
                mr.material = matWhite;
                // VFX GetHIt here
                //GameObject GetHitVFX = (GameObject)Instantiate(GetHitRef);
                //GetHitVFX.transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);

                if (HitPoints <= 0)
                {
                    // Dead animation

                    //GameObject Player.Dead.VFX = (GameObject)Instantiate(PlayerDeadRef);
                    //PlayerDead.VFX.transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);

                    //Game over screen
                }
                else
                {
                    Invoke("ResetMaterial", .5f);
                }

        }
    }

    private void ResetMaterial()
    {
        mr.material = matDefault;
    }
}
