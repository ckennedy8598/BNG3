using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class Hellbatscript : MonoBehaviour
    {

        // Yes, most of these scripts are the same. This will (likely) change later, this is more for the FFP
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
        public float timeBetweenAttacks = 5f;
        bool alreadyAttacked;

        // Enemy States
        public float sightRange, attackingRange;
        public bool playerInSightRange, playerInAttackRange;



        public GameObject enemyMelee;
        public Transform spawnPoint;

        [SerializeField]
        private float timer = 5;

        private float bulletTime;



        private void Awake()
        {
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();

        }
        private void Update()
        {
            //look at player function
            Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            this.transform.LookAt(targetPostition);


            // Checking attack and sight range

            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackingRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange)
            {
                //Patroling();
            }

            if (playerInSightRange)
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
                //Debug.Log("Walkpoint Found");
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
                //Debug.Log("Walkpont reached successfully");
            }
        }     private void ChasePlayer()
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            agent.SetDestination(player.position);
        }

        private void AttackPlayer()
        {
           //Stopping enemy movement during attacking
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;

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
            Debug.Log("Resetting Attack");
            alreadyAttacked = false;
        }

        public void TakeDamage(int damage)
        {
            //health -= damage;

            //if (health <= 0)
            //{
            //    Invoke(nameof(DestroyEnemy), 0.5f);
            //}
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        public float bulletVelocity = 10f;
        void ShootAtPlayer()
        {
            bulletTime -= Time.deltaTime;

            if (bulletTime > 0)
            {
                return;
            }

            Instantiate(enemyMelee, transform.position, Quaternion.identity);

            //bulletRig.AddForce(transform.forward * bulletVelocity, ForceMode.Impulse);

            //bulletTime = timer;

            //GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

            //Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
            //bulletRig.AddForce(bulletRig.transform.forward * bulletVelocity);

        }



        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackingRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }
}

