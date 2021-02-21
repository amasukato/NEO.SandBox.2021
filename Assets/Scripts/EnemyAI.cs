using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyState
{
    idle,
    walk,
    attack,
    knocback
}

public class EnemyAI : MonoBehaviour
{
    public EnemyState currentState;

    [HideInInspector] public NavMeshAgent agent;
    //[HideInInspector] public Combatant stats;
    [HideInInspector] public Combatant attacker;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody rb;

    [SerializeField] private Material matWhite;
    private Material matDefault;
    public Object explosionRef;
    MeshRenderer mr;

    public Transform player;

    public LayerMask whatIsGround, whatISPlayer;


    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    //Stats
    public float HitPoints = 3;
    public float MaxHitPoints;
    
    // Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // StateRange
    public float sightRange, attackRange;
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
        //matWhite = Resources.Load("White", typeof(Material)) as Material;
        matDefault = mr.material;

        //explosionRef = Resources.Load("Explosion");
        //stats.TakeDamage += Knockback;
    }

    void Update()
    {
        //check for sight and attack range

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatISPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatISPlayer);


        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();


    }

    private void Patroling()
    {

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {

        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;

    }

    private void ChasePlayer()
    {

        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack Code
            anim.SetTrigger("Swing");
            ///End Attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            HitPoints--;
            mr.material = matWhite;

            if (HitPoints <= 0)
            {
                Dead();
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

    public void TakeDamage(float damage)
    {
        HitPoints -= damage;

    }

    
    public void Dead()
    {
        //play dead_animation here
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
