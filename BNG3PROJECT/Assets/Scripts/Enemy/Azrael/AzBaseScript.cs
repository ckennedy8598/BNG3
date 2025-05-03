using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class AzBaseScript : MonoBehaviour
    {
        public NavMeshAgent agent;

        public Transform player;

        public Transform spawnPoint;

        public SpriteRenderer sprite;

        public Animator animator;

        public LayerMask whatIsGround, whatIsPlayer;

        public AzState State;

        public bool isDead = false;

        public float fireTime = 20f;

        public bool isFiring = false;

        public float shotType;

        public enum AzState
        {
            Idle,
            Walk,
            Attack,
            ShootFire,
            ShootIce,
            ShootLight,
            Die,
        }

        public void ResetState()
        {
            State = AzState.Idle;
        }

        public void StateHandler()
        {
            if (State == AzState.Idle && isDead == false && isFiring == false)
            {
                animator.SetBool("isIdle", true);
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;

                
            }

            if (State == AzState.Walk && isDead == false && isFiring == false)
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isShootingFire", false);
                animator.SetBool("isShootingLight", false);
                animator.SetBool("isShootingIce", false);

                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                agent.SetDestination(player.position);
                
            }

            if (State == AzState.Attack && isDead == false && isFiring == false)
            {
                animator.SetBool("isAttacking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isShootingFire", false);
                animator.SetBool("isShootingLight", false);
                animator.SetBool("isShootingIce", false);
                animator.SetBool("isWalking", true);

                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                Debug.Log("Az is attackin");
            }

            if (State == AzState.ShootFire && isDead == false && isFiring == true)
            {
                animator.SetBool("isShootingFire", true);
                animator.SetBool("isShootingLight", false);
                animator.SetBool("isShootingIce", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWalking", false);

                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                Debug.Log("Az is shooting fire");
            }
            if (State == AzState.ShootLight && isDead == false && isFiring == true)
            {
                animator.SetBool("isShootingLight", true);
                animator.SetBool("isShootingFire", false);
                animator.SetBool("isShootingIce", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWalking", false);

                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                Debug.Log("Az is shooting light");
            }
            if (State == AzState.ShootIce && isDead == false && isFiring == true)
            {
                animator.SetBool("isShootingIce", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWalking", false);
                

                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                Debug.Log("Az is shooting ice");
            }
            if (State == AzState.Die)
            {
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                isFiring = false;
                isDead = true;
                animator.SetTrigger("isDead");
                Debug.Log("Az is dead");

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
            State = AzState.Walk;
        }
        public void ResetFireTime()
        {

            fireTime = 20f;
        }

        // Update is called once per frame

        public void ShotTypeManager()
        {
            

            if (shotType <= 1)
            {
                //Lightning
                Debug.Log("Shooting Lightning");
                isFiring = true;
                State = AzState.ShootLight;

            }
            if (shotType > 2 && shotType < 5)
            {
                //Fireball
                Debug.Log("Shooting Fire");
                isFiring = true;
                State = AzState.ShootFire;
            }
            if (shotType >= 5)
            {
                //Icicle
                Debug.Log("Shooting Ice");
                isFiring = true;
                State = AzState.ShootIce;
            }

        }
        void Update()
        {
            Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            this.transform.LookAt(targetPostition);
            StateHandler();

            fireTime -= Time.deltaTime;
            if (fireTime <= 0f)
            {
                Debug.Log("fireTime is 0");
                
                ShotTypeManager();
            }
        }
    }
}
