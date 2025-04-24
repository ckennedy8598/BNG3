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
        void Start()
        {
            animator = this.GetComponent<Animator>();

            cloudSpawnPoint = GameObject.FindWithTag("CloudSpawnPoint").transform;

            agent = GetComponentInParent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameObject.GetComponentInParent<NavMeshAgent>().isStopped = true;
                animator.SetTrigger("Attack");
                
            }
        }
        public void Strike()
        {
            Instantiate(Lightning, cloudSpawnPoint.position, Quaternion.identity);
        }

        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
