using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer
{
    public class PlayerTickDamage : MonoBehaviour
    {
        // Poison timer port from other script
        private float poisonTimer = 4f;
        public bool isPoisoned = false;
        public bool poisonTick = true;
        private float timeBetweenPoison = 4f;

        // Burn timer is going to change. I'm thinking more damage but less ticks to make it different
        private float burnTimer = 3f;
        public bool isBurned = false;
        public bool burnTick = true;
        private float timeBetweenBurn = 5f;

        // Frost timer is going to change. i'm thinking less damage but the ticks are quicker.
        private float frostTimer = 5f;
        public bool isFrosted = false;
        public bool frostTick = true;
        private float timeBetweenFrost = 2f;


        public Player_Health playerHealth;
        void Start()
        {
            playerHealth = FindAnyObjectByType<Player_Health>();
        }
        private void ResetPoisonTick()
        {
            poisonTick = true;
        }

        private void ResetBurnTick()
        {
            burnTick = true;
        }

        private void ResetFrostTick()
        {
            frostTick = true;
        }
        private void PoisonDamage()
        {
            if (playerHealth.CanBeDamaged == true && poisonTick == true)
            {
                isBurned = false;
                isFrosted = false;
                playerHealth.TakeDamage(4);

                poisonTimer -= 1f;
                Debug.Log("Poison Damage");
                poisonTick = false;

                Invoke(nameof(ResetPoisonTick), timeBetweenPoison);
            }



            if (poisonTimer <= 0)
            {
                isPoisoned = false;
            }
        }

        private void BurnDamage()
        {
            if (playerHealth.CanBeDamaged == true && burnTick == true)
            {
                isPoisoned = false;
                isFrosted = false;
                playerHealth.PlayerHealth -= 7f;

                burnTimer -= 1f;
                Debug.Log("Burn Damage");
                burnTick = false;

                Invoke(nameof(ResetBurnTick), timeBetweenBurn);
            }

            if (burnTimer <= 0)
            {
                isBurned = false;
            }
        }

        private void FrostDamage()
        {
            if (playerHealth.CanBeDamaged == true && frostTick == true)
            {
                isPoisoned = false;
                isBurned = false;
                playerHealth.PlayerHealth -= 3f;
                frostTimer -= 1f;
                Debug.Log("Frost Damage");
                frostTick = false;

                Invoke(nameof(ResetFrostTick), timeBetweenFrost);
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (isPoisoned == true)
            {
                PoisonDamage();

            }

            if (isBurned == true)
            {
                BurnDamage();
            }

            if (isFrosted == true)
            {
                FrostDamage();
            }
        }
    }
}
