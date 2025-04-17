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

        public float fireTime = 20f;

        public bool isFiring = false;

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
            if (State == CerbState.Idle && isDead == false && isFiring == false)
            {
                animator.SetBool("isIdle", true);
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            }

            if (State == CerbState.Walk && isDead == false && isFiring == false)
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isShooting", false);

                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                agent.SetDestination(player.position);
                //Debug.Log("Cerb is walkin");
            }

            if(State == CerbState.Attack && isDead == false && isFiring == false)
            {
                animator.SetBool("isAttacking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isShooting", false);
                animator.SetBool("isWalking", true);

                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                Debug.Log("Cerb is attackin");
            }

            if(State == CerbState.Shoot && isDead == false && isFiring == true)
            {
                animator.SetBool("isShooting", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWalking", false);

                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                Debug.Log("Cerb is shootin");
            }

            if(State == CerbState.Die)
            {
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                isFiring = false;
                isDead = true;
                animator.SetTrigger("isDead");
                Debug.Log("Cerb is dead");

            }
        }
        void Awake()
        {
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();

            animator = this.GetComponentInChildren<Animator>();

            
        }

         void Start()
        {
            fireTime = 20f;
            State = CerbState.Walk;
        }
        public void ResetFireTime()
        {
            fireTime = 20f;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            this.transform.LookAt(targetPostition);
            StateHandler();

            fireTime -= Time.deltaTime;
            if (fireTime <= 0f) 
            {
                Debug.Log("fireTime is 0");
                isFiring = true;
                State = CerbState.Shoot;
            }
        }
    }
}
