using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyState
{
    idle,
    Searching,
    Chasing,
    Patroling,
    Attacking,
    Runaway,
    GetHit,
    knockback,
}

public class EnemyAI : MonoBehaviour
{
    public EnemyState currentState;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody rb;

    [SerializeField] private Material matWhite;
    private Material matDefault;
    private Material SkinDefault;
    public Object explosionRef;
    MeshRenderer mr;
    SkinnedMeshRenderer smr;

    public Transform player;

    public LayerMask whatIsGround, whatISPlayer;


    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    //Stats
    [Header("Stats")]
    public float HitPoints = 3;
    public float MaxHitPoints;
    
    // Patroling
    [Header("Patrol")]
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    public float DistanceToRunaway = 10f;


    //Attacking
    [Header("Attack")]

    public bool SetRandomTBA;
    public float timeBetweenAttacks = 1.5f;
    public float timeBetweenAttacksMIN;
    public float timeBetweenAttacksMAX;
    bool alreadyAttacked;
    public float knockbackTime;
    public bool Attacker, Caster, Summoner;

    // StateRange
    [Header("Range")]
    public float sightRange; 
    public float attackRange;
    private bool playerInSightRange, playerInAttackRange, RunAway;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //stats = GetComponent<Combatant>();
        anim = GetComponentInChildren<Animator>();
        mr = GetComponent<MeshRenderer>();
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
        //matWhite = Resources.Load("White", typeof(Material)) as Material;
        matDefault = mr.material;
        SkinDefault = smr.material;

        //explosionRef = Resources.Load("Explosion");
    }

    void Update()
    {
        //check for sight and attack range

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatISPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatISPlayer);




        switch (currentState)
        {
            default:
            case EnemyState.idle:

                if (!playerInSightRange && !playerInAttackRange) Patroling();
                if (playerInSightRange && !playerInAttackRange) ChasePlayer();
                if (playerInSightRange && playerInAttackRange) AttackPlayer();


                break;
            case EnemyState.Attacking:
                break;
            case EnemyState.Chasing:
                break;
            case EnemyState.GetHit:
                break;
            case EnemyState.knockback:

                StartCoroutine(KnockBacked());
                break;
            case EnemyState.Runaway:
                if (!playerInSightRange && !playerInAttackRange) Patroling();
                if (playerInSightRange) Runaway();
                //if (playerInAttackRange) AttackPlayer();
                break;
            case EnemyState.Patroling:
                break;
            case EnemyState.Searching:
                break;
        }

    }

     private void LateUpdate()
    {
        if (currentState == EnemyState.Runaway && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {

        if (!walkPointSet)
        {
            anim.SetTrigger("Search");
            StartCoroutine(SearchWalkPoint());

        }

        if (walkPointSet)
        {
            anim.SetTrigger("Walk");


            agent.SetDestination(walkPoint);

        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 0.5f)
        {
            walkPointSet = false;

        }


    }

    IEnumerator SearchWalkPoint()
    {
        agent.SetDestination(transform.position);

        yield return new WaitForSeconds(4f);
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;

        }


    }

    private void ChasePlayer()
    {
        anim.SetTrigger("Walk");

        agent.SetDestination(player.position);


    }

    public void Runaway()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < DistanceToRunaway)
        {
            Vector3 distanceToPlayer = transform.position - player.transform.position;

            Vector3 newPos = transform.position + distanceToPlayer;

            agent.SetDestination(newPos);
            anim.SetTrigger("Walk");
        }
        RunAway = true;
        currentState = EnemyState.Runaway;


    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack Code
            //anim.SetTrigger("Swing");
            if (Attacker == true || Caster == true)
            {
                if (Attacker == true)
                {
                    anim.Play("Attack");
                }

                if (Caster == true)
                {
                    anim.Play("MagicAttack");
                }

                if (SetRandomTBA == true)
                {
                    timeBetweenAttacks = Random.Range(timeBetweenAttacksMIN, timeBetweenAttacksMAX);

                }

                alreadyAttacked = true;
                currentState = EnemyState.Attacking;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }

            //StartCoroutine(ResetAttacking());
            if (Summoner == true)
            {
                anim.Play("Summon");
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                currentState = EnemyState.Runaway;
            }
        }

    }


    IEnumerator ResetAttacking()
    {
        currentState = EnemyState.idle;
        yield return new WaitForSeconds(timeBetweenAttacks);
        alreadyAttacked = false;
        currentState = EnemyState.idle;
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
        if (!Summoner)
        {
            currentState = EnemyState.idle;
        }
        else
        {
            currentState = EnemyState.Runaway;
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            anim.Play("GetHit");

            HitPoints--;
            mr.material = matWhite;

            smr.material = matWhite;



            if (HitPoints <= 0)
            {
                Dead();
            }
            else
            {
                Invoke("ResetMaterial", .3f);
            }


        }
    }

    IEnumerator KnockBacked()
    {
        yield return new WaitForSeconds(knockbackTime);

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        currentState = EnemyState.idle;
    }

    private void ResetMaterial()
    {
        mr.material = matDefault;
        smr.material = SkinDefault;
    }

    public void TakeDamage(float damage)
    {
        HitPoints -= damage;

    }
    
    public void Dead()
    {
        //play dead_animation here
        anim.Play("GetHit");

        FindObjectOfType<AudioManager>().Play("Enemy_Dead");

        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);

        Destroy(gameObject); // add time until death


        // To spawn enemies on dead
        if(OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
