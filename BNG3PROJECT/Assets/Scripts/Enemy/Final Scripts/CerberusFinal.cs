using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class CerberusFinal : MonoBehaviour
    {
        // Start is called before the first frame update
        public NavMeshAgent agent;

        public Transform player;

        public Transform spawnPoint;

        public SpriteRenderer sprite;

        public Animator animator;

        public LayerMask whatIsGround, whatIsPlayer;

        public CerbState State;

        public bool isDead = false;

        public enum CerbState
        {
            Idle,
            Walk,
            Attack,
            Shoot,
            Die,
        }
        
        public void ResetState()
        {
            State = CerbState.Idle;
        }

        public void StateHandler()
        {
            if (State == CerbState.Idle && isDead == false)
            {
                animator.SetBool("isIdle", true);
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            }

            if (State == CerbState.Walk && isDead == false)
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isShooting", false);

                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                agent.SetDestination(player.position);
            }

            if(State == CerbState.Attack && isDead == false)
            {
                animator.SetBool("isAttacking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isShooting", false);
                animator.SetBool("isWalking", true);

                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            }

            if(State == CerbState.Shoot && isDead == false)
            {
                animator.SetBool("isShooting", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isShooting", false);
                animator.SetBool("iswWalking", false);

                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            }

            if(State == CerbState.Die)
            {
                animator.SetTrigger("isDead");

            }
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
        
        }
    }
}
