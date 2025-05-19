using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Platformer.RangedEnemyFinal;

namespace Platformer
{
    public class BatFinal : MonoBehaviour
    {
        // Going to have to attach the new attack script to the sprite. 
        public NavMeshAgent agent;

        public Transform player;

        public Animator animator;

        public LayerMask whatIsGround, whatIsPlayer;

        public float sightRange, attackingRange;
        //public bool playerInSightRange, playerInAttackRange;

        public bool isDead = false;

        public float timeBetweenAttacks = 5f;
        bool alreadyAttacked;

        public BatState state;
        public enum BatState
        {
            Idle,
            Walk,
            Attack,
            Die,

        }
        private void StateHandler()
        {
            if (state == BatState.Idle && isDead == false)
            {
                //Idle animation will go here
                animator.SetBool("isIdle", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;

            }

            if (state == BatState.Walk && isDead == false)
            {

                //Debug.Log("Walking");

                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                agent.SetDestination(player.position);
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                //animator.SetTrigger("endAttack");

            }
            //else
            //{
            //    animator.SetBool("isWalking", false);
            //}

            if (state == BatState.Attack && isDead == false)
            {
                //Debug.Log("Attack!");
                

                //Attack animation will go here
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                animator.SetBool("isAttacking", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", false);




                //if (!alreadyAttacked)
                //{


                //    Debug.Log("Attacking Player");
                //    alreadyAttacked = true;
                //    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                //    //stateRecheck = true;
                //}


            }

            if (state == BatState.Die && isDead == true)
            {
                //Death animation will go here
                animator.SetTrigger("isDead");
            }
        }
        public void ResetState()
        {
            state = BatState.Idle;
        }
        void Awake()
        {
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            animator = this.GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            //look at player function
            Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            this.transform.LookAt(targetPostition);

            StateHandler();

            // Attack and Sight range checkspheres.

            //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            //playerInAttackRange = Physics.CheckSphere(transform.position, attackingRange, whatIsPlayer);
        }

        
    }
}
