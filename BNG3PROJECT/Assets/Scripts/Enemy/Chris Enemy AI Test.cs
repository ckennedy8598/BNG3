using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChrisEnemyAITest : MonoBehaviour
{
    // This is the script I will be making for the POK. 
    // It is pretty basic, but I want to take a shot at
    // writing an AI script before I dissect other ones
    // - Chris



    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;


    // Patroling variables
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //walkspeed does nothing yet
    public float walkSpeed;

    // Attack Timers
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // Enemy States
    public float sightRange, attackingRange;
    public bool playerInSightRange, playerInAttackRange;

    public int health = 1;

    public GameObject enemyBullet;
    public Transform spawnPoint;

    [SerializeField]
    private float timer = 5;

    private float bulletTime;

    public float bulletVelocity;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }
    private void Update()
    {
        // Checking attack and sight range

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackingRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            Debug.Log("Walkpoint Found");
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distancetowalkpoint = transform.position - walkPoint;

        //Walkpoint reached

        if (distancetowalkpoint.magnitude < 1f)
        {
            walkPointSet = false;
            Debug.Log("Walkpont reached successfully");
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Stopping enemy movement during attacking
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        

        if (!alreadyAttacked)
        {

            ShootAtPlayer();
            Debug.Log("Attacking Player");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
   
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0)
        {
            return;
        }

        bulletTime = timer;

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * bulletVelocity);
        
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackingRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}