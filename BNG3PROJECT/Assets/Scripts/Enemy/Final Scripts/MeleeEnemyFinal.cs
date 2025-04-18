using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Platformer.RangedEnemyFinal;

namespace Platformer
{
    public class MeleeEnemyFinal : MonoBehaviour
    {
        public NavMeshAgent agent;

        public Transform player;

        public Transform spawnPoint;

        public SpriteRenderer sprite;

        public Animator animator;

        public LayerMask whatIsGround, whatIsPlayer;

        public MeleeState State;

        public bool isDead = false;

        public enum MeleeState
        {
            Idle,
            Walk,
            Attack,
            Knockback,
            Die,
        }

        private void Awake()
        {
            spawnPoint = GetComponent<Transform>();
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();

            animator = this.GetComponentInChildren<Animator>();
        }

        public void ResetState()
        {
            State = MeleeState.Idle;
            animator.SetBool("isIdle", true);
        }

        void Update()
        {
            Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            this.transform.LookAt(targetPostition);
            // Checking attack and sight range
            StateHandler();

            //// playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            //playerInAttackRange = Physics.CheckSphere(transform.position, attackingRange, whatIsPlayer);
            //if (State != MeleeState.Die)
            //{
            //    if (!playerInSightRange && !playerInAttackRange)
            //    {
            //        State = DeviState.Idle;
            //        testbool = true;
            //    }
            //    //else if (playerInSightRange && !playerInAttackRange && testbool == true)
            //    //{
            //    //    testbool = false;
            //    //    State = DeviState.Walk;

            //    //}
            //    else if (playerInSightRange && playerInAttackRange)
            //    {

            //        State = DeviState.Attack;
            //    }
            //}
            //else
            //{
            //    return;
            //}
        }

        private void StateHandler()
        {
            if (State == MeleeState.Idle && isDead == false)
            {
                //Idle animation will go here
                animator.SetBool("isIdle", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;

            }

            if (State == MeleeState.Walk && isDead == false)
            {

                Debug.Log("Walking");

                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                agent.SetDestination(player.position);
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                animator.SetTrigger("endAttack");

            }
            //else
            //{
            //    animator.SetBool("isWalking", false);
            //}

            if (State == MeleeState.Attack && isDead == false)
            {
                Debug.Log("Attack!");
                

                //Attack animation will go here
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                animator.SetBool("isAttacking", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", false);




                //if (!alreadyAttacked)
                //{


                //    Debug.Log("Attacking Player");
                //    ShootAtPlayer();
                //    alreadyAttacked = true;
                //    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                //    //stateRecheck = true;
                //}


            }

            if (State == MeleeState.Die && isDead == true)
            {
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                //leave this empty
                // lol jk
                
            }
        }
    }

}
