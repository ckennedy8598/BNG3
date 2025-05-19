using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class CloudScript : MonoBehaviour
    {
        // Start is called before the first frame update
        public NavMeshAgent agent;

        public Transform player;

        
        void Start()
        {
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();

            

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            this.transform.LookAt(targetPostition);
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            agent.SetDestination(player.position);
        }

        
    }
}
