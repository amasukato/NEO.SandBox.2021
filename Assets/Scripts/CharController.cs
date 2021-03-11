using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharController : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public CharacterController controller;
    public CameraController cameraShake;


    //Effect
    [SerializeField] private Animator anim1;
    [SerializeField] private Object GetHitRef;
    [SerializeField] private Object PlayerDeadRef;
    [SerializeField] private Object SpecialAttackRef;

    private SkinnedMeshRenderer smr;
    private Material DefaultMat;
    private Material WhiteMat;

    [Header("Stats")]
    public float HitPoints;
    public float MaxHitPoints;
    public float ManaPoints;
    public float MaxManaPoints;


    [Header("Dash")]
    public float dashSpeed = 50;
    public float dashTime = 0.25f;
    public float dashCD = 0.3f;
    public float dashMaxCD = 0.5f;

    //Detector
    public Transform groundCheck;
    private float groundDistance = 0.3f;
    public LayerMask groundMask;
    private bool IsOnTheGround;

    // Base Mouvement
    private float gravity = -9.81f;
    public float moveSpeed = 4f;
    public float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;
    public Vector3 movDir;
    Vector3 velocity;

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
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
        DefaultMat = smr.material;
        WhiteMat = Resources.Load("White", typeof(Material)) as Material;


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
                SpecialAttack();

                break;

            case State.IdleRecovery:
                Move();
                Dashing();
                Attack();
                SpecialAttack();

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
            movDir.y = -10f;

        }

        if (movDir.x <= -2)
        {
            anim1.SetTrigger("idle1");
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        dashCD -= Time.deltaTime;


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

    void SpecialAttack()
    {
        if (Input.GetButtonDown("Fire4") && ManaPoints >= 20 )
        {
            GetComponent<HUD>().SpendMana(20);
            anim1.Play("Sword And Shield Slash 0");

            FindObjectOfType<AudioManager>().Play("PlayerSpecialAttack");

            GameObject SpecialAttack_VFX = (GameObject)Instantiate(SpecialAttackRef);
            SpecialAttack_VFX.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);

            PlayerState = State.Attacking;
        }
    }

    void Dashing()
    {
        if (Input.GetButtonDown("Fire2"))
        {

            if (dashCD <= 0)
            {
                StartCoroutine(Dash());
                //FindObjectOfType<AudioManager>().Play("PlayerDash");
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


                dashCD = 0.5f;

                yield return null;
            }
            PlayerState = State.Idle;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyHitBox"))
        {
            //GetComponent<HUD>().Damage(1);

            smr.material = WhiteMat;
            // VFX GetHit here
            GameObject GetHitVFX = (GameObject)Instantiate(GetHitRef);
            GetHitVFX.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
            FindObjectOfType<AudioManager>().Play("PlayerGetHit01");

            if (HitPoints <= 0)
            {

            }
            else
            {
                Invoke("ResetMaterial", .3f);
            }

        }
    }

    private void ResetMaterial()
    {
        smr.material = DefaultMat;
    }
    /*
    public void TakeDamage(float damage)
    {
        HitPoints -= damage;

    }

    public void TakeHeal (float healing)
    {
        HitPoints += healing;
    }
    */

}
