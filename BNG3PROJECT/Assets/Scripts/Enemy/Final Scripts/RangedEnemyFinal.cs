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
        bool alreadyAttacked;

        //Attacking Ranges
        public float sightRange, attackingRange;
        public bool playerInSightRange, playerInAttackRange;

        

        private void StateHandler()
        {
            if(State == DeviState.Idle)
            {
                //Idle animation will go here
            }

            if(State == DeviState.Walk)
            {
                //Walking animation will go here

            }

            if(State == DeviState.Attack)
            {
                //Attack animation will go here
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                animator.SetBool("isShooting", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", false);

                Instantiate(enemyBullet, transform.Find("SpawnPoint").position, Quaternion.identity);


                if (!alreadyAttacked)
                {

                    
                    Debug.Log("Attacking Player");

                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
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
        void Start()
        {
            spawnPoint = GetComponent<Transform>();
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();

            animator = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            this.transform.LookAt(targetPostition);
            // Checking attack and sight range


            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackingRange, whatIsPlayer);
        }
    }
}
