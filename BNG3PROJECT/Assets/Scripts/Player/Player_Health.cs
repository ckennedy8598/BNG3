using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using System.Runtime.CompilerServices;

namespace Platformer
{
    public class Player_Health : MonoBehaviour
    {
        [Header("Pause_Menu.cs Reference")]
        public Pause_Menu PM_Script;

        [Header("Health Bar")]
        public Slider HealthSlider;

        private float _maxHealth = 100f;
        public bool CanBeDamaged;
        public float PlayerHealth = 20f;
        public TMP_Text HealthReadout;


        // These are variables for my poison damage method - Chris
        private float poisonTimer = 4f;
        public bool isPoisoned = false;
        public bool poisonTick = true;
        private float timeBetweenPoison = 4f;

        // Start is called before the first frame update
        void Start()
        {
            PM_Script = FindAnyObjectByType<Pause_Menu>();
            CanBeDamaged = true;
        }

        // Update is called once per frame
        void Update()
        {
            // Update UI Element
            HealthReadout.text = "Health: " +  PlayerHealth.ToString();
            HealthSlider.value = PlayerHealth;
            _checkDead();
            if (isPoisoned == true)
            {
                PoisonDamage();
                
            }
        }

        //Poison damage. Gonna rework this in the future. I want this to do a certian amount of damage a second - Chris
        private void ResetPoisonTick()
        {
            poisonTick = true;
        }
        private void PoisonDamage()
        {
            if (CanBeDamaged == true && poisonTick == true)
            {

                PlayerHealth -= 3f;

                poisonTimer -= 1f;
                Debug.Log("Poison Damage");
                poisonTick = false;

                Invoke(nameof(ResetPoisonTick), timeBetweenPoison);
            }
            

            
            if (poisonTimer <= 0 )
            {
                isPoisoned = false;
            }
        }

        private void LateUpdate()
        {
            _setDead();
        }

        public void IncreaseHealth(float amount)
        {
            if (PlayerHealth > 0)
            {
                PlayerHealth += amount;
                if (PlayerHealth >= _maxHealth)
                {
                    PlayerHealth = _maxHealth;
                }
            }
        }

        public void TakeDamage(float damage)
        {
            if (CanBeDamaged)
            {
                if (PlayerHealth <= 0)
                {
                    CanBeDamaged = false;
                    return;
                }
                else
                {
                    PlayerHealth -= damage;
                }
            }          
        }

        private void _setDead()
        {
            if (PM_Script.PlayerDead)
            {
                gameObject.SetActive(false);
            }
        }

        private void _checkDead()
        {
            if (PlayerHealth <= 0)
            {
                HealthSlider.enabled = false;
                PM_Script.PlayerDead = true;
                PlayerHealth = 0;
            }
        }
    }
}
