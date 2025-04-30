using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class Cerb2AnimationEvents : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject CerbCloud;

        public Cerb2 C2;

        public Animator animator;

        public Transform cerbSpawnPoint;

        public GameObject AR;

        public NavMeshAgent agent;

        public Player_Health playerHealth;
        void Start()
        {
            playerHealth = FindAnyObjectByType<Player_Health>();
            C2 = GetComponentInParent<Cerb2>();
            animator = GetComponent<Animator>();
            cerbSpawnPoint = GameObject.FindWithTag("CerbSpawnPoint").transform;
            AR = GameObject.FindWithTag("CerberusAttackRange");
            agent = GetComponentInParent<NavMeshAgent>();
        }

        public void LightningStrike()
        {
            if(playerHealth.PlayerHealth > 0 )
            {
               Instantiate(CerbCloud, cerbSpawnPoint.position, Quaternion.identity);
            }
            
            C2.isFiring = false;
            C2.ResetFireTime();
            animator.SetTrigger("endShot");

        }

        public void Cerb2ResetWalk()
        {
            C2.State = Cerb2.CerbState.Walk;
        }

        public void Cerb2StartDeath()
        {
            C2.isDead = true;
            C2.State = Cerb2.CerbState.Die;
            gameObject.GetComponentInParent<NavMeshAgent>().isStopped = true;
        }

        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }

        public void AttackActivate()
        {
            AR.SetActive(true);
        }

        public void AttackDeactivate()
        {
            AR.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
