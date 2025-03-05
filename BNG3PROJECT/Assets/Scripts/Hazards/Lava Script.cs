using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class LavaScript : MonoBehaviour
    {
        // Start is called before the first frame update

        public float lavaDamage = 20f;
        public Player_Health playerHealth;
        public GameObject slime;

        public GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            playerHealth = FindAnyObjectByType<Player_Health>();

            slime = GameObject.Find("Slime");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnTriggerEnter(Collider other)
        {


            if (other.gameObject.CompareTag("Player"))
            {
                playerHealth.TakeDamage(lavaDamage);
                Debug.Log("Player has been Killed by Lava");
            }
            if (other.gameObject == slime)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
