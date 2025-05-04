using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class AzAnimationEvents : MonoBehaviour
    {
        // Start is called before the first frame update
        public AzBaseScript AZ;

        public GameObject CerbFire;

        public GameObject CerbCloud;

        public GameObject Icicle;

        public Animator animator;

        public Transform cerbSpawnPoint;

        public GameObject AR;

        public NavMeshAgent agent;

        public Player_Health playerHealth;

        
        void Start()
        {
            playerHealth = FindAnyObjectByType<Player_Health>();
            AZ = GetComponentInParent<AzBaseScript>();
            animator = GetComponent<Animator>();
            cerbSpawnPoint = GameObject.FindWithTag("CerbSpawnPoint").transform;
            AR = GameObject.FindWithTag("CerberusAttackRange");
            agent = GetComponentInParent<NavMeshAgent>();




        }

        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }

        public void AZFireball()
        {
            
            if (playerHealth.PlayerHealth > 0)
            {
                Instantiate(CerbFire, cerbSpawnPoint.position, Quaternion.identity);
            }

            AZ.isFiring = false;
            AZ.ResetFireTime();
            animator.SetTrigger("endShot");

        }

        public void AZLightningStrike()
        {
            var displacer = new Vector3(0, 3, 0);

            var replacer = new Vector3(0, -3, 0);
            if (playerHealth.PlayerHealth > 0)
            {
                cerbSpawnPoint.transform.position += displacer;
                Instantiate(CerbCloud, cerbSpawnPoint.position, Quaternion.identity);
            }

            AZ.isFiring = false;
            AZ.ResetFireTime();
            cerbSpawnPoint.transform.position += replacer;
            animator.SetTrigger("endShot");

        }

        public void IcicleShoot()
        {
            if (playerHealth.PlayerHealth > 0)
            {
                Instantiate(Icicle, cerbSpawnPoint.position, Quaternion.identity);
            }

            AZ.isFiring = false;
            AZ.ResetFireTime();
            animator.SetTrigger("endShot");
        }

        public void AZAttackActivate()
        {
            AR.SetActive(true);
        }

        public void AZAttackDeactivate()
        {
            AR.SetActive(false);
        }

        public void ResetWalk()
        {
            AZ.State = AzBaseScript.AzState.Walk;
        }

        public void ChooseAttack()
        {
            AZ.shotType = Random.Range(0, 10);
        }

        public void AZStartDeath()
        {
            AZ.isDead = true;
            AZ.State = AzBaseScript.AzState.Die;
            gameObject.GetComponentInParent<NavMeshAgent>().isStopped = true;
        }

        public void ResetFire()
        {
            AZ.ResetFireTime();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
