using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class RangedEnemyFinal : MonoBehaviour
    {
        //Reminder to self: His name is the Devistator
        //This is the script that will be used in the finished game
        // - Chris

        
        public NavMeshAgent agent;

        public Transform player;

        public GameObject enemyBullet;

        public Transform spawnPoint;

        public SpriteRenderer sprite;

        public Animator animator;

        public LayerMask whatIsGround, whatIsPlayer;

        private float bulletTime;

        public bool stateRecheck = true;

        public bool testbool = false;

        public DeviState State;
        public enum DeviState
        {

            //Yeah
            Idle,
            Walk,
            Attack,
            Knockback,
            Die,

        }
        //Attacking timer
        public float timeBetweenAttacks = 5f;
        bool alreadyAttacked = false;

        //Attacking Ranges
        public float sightRange, attackingRange;
        public bool playerInSightRange, playerInAttackRange;

        private void ResetState()
        {
            State = DeviState.Idle;
        }

        private void StateHandler()
        {
            if(State == DeviState.Idle)
            {
                //Idle animation will go here
                //animator.SetBool("isIdle", true);
               
            }

            if(State == DeviState.Walk)
            {
                
                Debug.Log("Walking");
                
                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                agent.SetDestination(player.position);
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isShooting", false);

            }
            //else
            //{
            //    animator.SetBool("isWalking", false);
            //}

            if(State == DeviState.Attack)
            {
                Debug.Log("Attack!");
                stateRecheck = false;
                
                //Attack animation will go here
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                animator.SetBool("isShooting", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", false);

                


                if (!alreadyAttacked)
                {

                    
                    Debug.Log("Attacking Player");
                    ShootAtPlayer();
                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                    //stateRecheck = true;
                }

                
            }

            if(State == DeviState.Die)
            {
                //Death animation will go here
            }
        }

        private void ResetAttack()
        {
            Debug.Log("Resetting Attack");
            alreadyAttacked = false;
        }
        void Awake()
        {
            spawnPoint = GetComponent<Transform>();
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();

            animator = this.GetComponentInChildren<Animator>();
        }
        void Start()
        {
         
        }
        void ShootAtPlayer()
        {
            bulletTime -= Time.deltaTime;

            if (bulletTime > 0)
            {
                return;
            }

            Instantiate(enemyBullet, transform.Find("SpawnPoint").position, Quaternion.identity);
            

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            this.transform.LookAt(targetPostition);
            // Checking attack and sight range
            StateHandler();

           // playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackingRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange)
            {
                State = DeviState.Idle;
                testbool = true;
            }
            //else if (playerInSightRange && !playerInAttackRange && testbool == true)
            //{
            //    testbool = false;
            //    State = DeviState.Walk;   
                  
            //}
            else if (playerInSightRange && playerInAttackRange)
            {

                State = DeviState.Attack;
            }
        }
    }
}
