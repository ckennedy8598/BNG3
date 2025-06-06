using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class CerbAnimationEvents : MonoBehaviour
    {
        // Start is called before the first frame update

        public CerberusFinal CF;

        public GameObject CerbFire;

        public Animator animator;

        public Transform cerbSpawnPoint;

        public GameObject AR;

        public NavMeshAgent agent;

        public Player_Health playerHealth;

        public AudioClip AU;
        void Start()
        {
            playerHealth = FindAnyObjectByType<Player_Health>();
            CF = GetComponentInParent<CerberusFinal>();
            animator = GetComponent<Animator>();
            cerbSpawnPoint = GameObject.FindWithTag("CerbSpawnPoint").transform;
            AR = GameObject.FindWithTag("CerberusAttackRange");
            agent = GetComponentInParent<NavMeshAgent>();

            

        }

        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }

        public void Fireball()
        {
            if (playerHealth != null)
            {
                if (playerHealth.PlayerHealth > 0)
                {
                    Instantiate(CerbFire, cerbSpawnPoint.position, Quaternion.identity);
                }
            }
            
            CF.isFiring = false;
            CF.ResetFireTime();
            animator.SetTrigger("endShot");

        }

        
        public void PlayBreathSound()
        {
            AudioSource.PlayClipAtPoint(AU, transform.position, 1f);
        }


        public void AttackActivate()
        {
            AR.SetActive(true);
        }

        public void AttackDeactivate()
        {
            AR.SetActive(false);
        }

        public void ResetWalk()
        {
            CF.State = CerberusFinal.CerbState.Walk;
        }

        

        public void StartDeath()
        {
            CF.isDead = true;
            CF.State = CerberusFinal.CerbState.Die;
            gameObject.GetComponentInParent<NavMeshAgent>().isStopped = true;
        }

        

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
