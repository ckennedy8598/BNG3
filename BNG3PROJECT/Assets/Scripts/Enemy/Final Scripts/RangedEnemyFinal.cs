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


        public Animator animator;

        public LayerMask whatIsGround, whatIsPlayer;

        public DeviState State;
        public enum DeviState
        {

            //Yeah
            Idle,
            Walk,
            Attack,
            Hurt,
            Knockback,
            Die,

        }
        //Attacking timer
        public float timeBetweenAttacks = 5f;
        bool alreadyAttacked;

        //Attacking Ranges
        public float sightRange, attackingRange;
        public bool playerInSightRange, playerInAttackRange;

        



        
        void Start()
        {
            spawnPoint = GetComponent<Transform>();
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
