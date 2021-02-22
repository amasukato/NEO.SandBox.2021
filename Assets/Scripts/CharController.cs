using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharController : MonoBehaviour
{

    [HideInInspector] public Animator anim1;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public MeshRenderer mr;
    [HideInInspector] public CharacterController controller;

    //Effect
    [SerializeField] private Object DashRef;
    [SerializeField] private Object GetHitRef;
    [SerializeField] private Object PlayerDeadRef;
    [SerializeField] private Material matWhite;
    private Material matDefault;


    //Stats
    public float HitPoints;
    public float MaxHitPoints;

    private float gravity = -9.81f;
    public float moveSpeed = 4f;
    private float dashSpeed = 20;
    public float dashTime = 0.25f;
    public float dashCD = 0.3f;

    private float JumpForce;

    //States

    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool IsOnTheGround;

    public float turnSmoothTime;
    float turnSmoothVelocity;
    private bool alreadyAttacked;
    private float FreezeTime = 0.4f; // Duration of stop moving during Attack

    // Dash & Movement
    public Vector3 movDir;

    // Grab & Thrown
    public Transform ObjectHolder;
    public float ThrowForce;
    public bool CarryObject;
    public GameObject Item;
    public bool IsThrowable;
    public float grabDistance = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim1 = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        mr = GetComponent<MeshRenderer>();
        //matWhite = Resources.Load("White", typeof(Material)) as Material;
        matDefault = mr.material;

    }

    // Update is called once per frame
    void Update()
    {

        IsOnTheGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (IsOnTheGround && movDir.y <= 0)
        {
            movDir.y = -4f;
        }

        Move();

        movDir.y += gravity * Time.deltaTime;
        //movDir.y += gravity * Time.deltaTime;
        //controller.Move(movDir * Time.deltaTime); // pas besoin de gravité réaliste donc pas besoin de doubler

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
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
        // Movement Animation
        //anim2.SetTrigger("Run");

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
        // Attack Weapon Animation
        anim1.SetTrigger("Attack");

        // Attack Pattern Character Animation
        // anim2.SetTrigger("AttackPattern");
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            StartCoroutine(FreezePosition());

        }
    }

    IEnumerator FreezePosition()
    {
        yield return new WaitForSeconds(FreezeTime);

        alreadyAttacked = false;
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
