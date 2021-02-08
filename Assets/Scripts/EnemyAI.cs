using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class EnemyAI : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    //[HideInInspector] public Combatant stats;
    [HideInInspector] public Combatant attacker;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody rb;

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

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //stats = GetComponent<Combatant>();
        anim = GetComponent<Animator>();

        //stats.TakeDamage += Knockback;
    }

    // Update is called once per frame
    void Update()
    {
        //check for sight and attack range

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatISPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatISPlayer);


        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //if (playerInSightRange && playerInAttackRange) AttackPlayer();

        if (HitPoints <= 0)
        {
            Dead();
        }
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
            ///End Attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HitBox"))
        {
            HitPoints -= 1;
        }
    }
    public void TakeDamage(float damage)
    {
        HitPoints -= damage;

    }


    public void Dead()
    {
        //play dead_animation here
        Destroy(gameObject); // add time until death

        if(OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
    }
    /*
    //Override State Methods ?
    private void Knockback(Combatant att, Combatant def)
    {
        attacker = att;

        Vector3 direction = (def.transform.position - att.transform.position).normalized;
        float knockbackSpeed = Mathf.Max(att.knockbackForce - stats.knockbackResist, 0);

        rb.velocity = direction * knockbackSpeed;

    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
