using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class CloudAnimationEvents : MonoBehaviour
    {
        // Start is called before the first frame update
        public Animator animator;

        public Transform cloudSpawnPoint;

        public GameObject Lightning;

        public NavMeshAgent agent;

        public BoxCollider BC;

        public Player_Health playerHealth;

        public float strikeTimer = 10f;
        void Start()
        {
            animator = this.GetComponent<Animator>();

            cloudSpawnPoint = GameObject.FindWithTag("CloudSpawnPoint").transform;

            agent = GetComponentInParent<NavMeshAgent>();

            //Lightning = GameObject.FindWithTag("Lightning");

            BC = GetComponent<BoxCollider>();

            BC.enabled = true;

            playerHealth = FindAnyObjectByType<Player_Health>();
        }

        // Update is called once per frame
        void Update()
        {
        
            strikeTimer -= Time.deltaTime;
            if (playerHealth.PlayerHealth > 0)
            {
                if (strikeTimer <= 0)
                {
                    BC.enabled = false;
                    animator.SetTrigger("Attack");
                }
            }
        }
        public void OnTriggerEnter(Collider other)
        {
            if (playerHealth != null)
            {
                if (playerHealth.PlayerHealth > 0)
                {
                    if (other.gameObject.CompareTag("Player"))
                    {
                        gameObject.GetComponentInParent<NavMeshAgent>().isStopped = true;
                        animator.SetTrigger("Attack");

                    }
                }
            }
        }
        public void Strike()
        {
            Instantiate( Lightning, cloudSpawnPoint.position, Quaternion.identity);
        }

        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
