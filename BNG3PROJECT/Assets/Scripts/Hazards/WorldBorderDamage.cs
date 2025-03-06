using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class WorldBorderDamage : MonoBehaviour
    {
        public int borderDamageAmount = 1000;
        public Player_Health playerHealth;
        public GameObject player;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
               
                
                playerHealth.TakeDamage(borderDamageAmount);

                Debug.Log("Player should be dead");

            }
        }
    }
}
