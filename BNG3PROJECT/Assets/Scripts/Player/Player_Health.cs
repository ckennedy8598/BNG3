using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

namespace Platformer
{
    public class Player_Health : MonoBehaviour
    {
        [Header("Pause_Menu.cs Reference")]
        public Pause_Menu PM_Script;

        [Header("Health Bar")]
        public Slider HealthSlider;

        private bool _canBeDamaged;
        private float _maxHealth = 20f;
        public float PlayerHealth = 20f;
        public TMP_Text HealthReadout;
        // Start is called before the first frame update
        void Start()
        {
            PM_Script = FindAnyObjectByType<Pause_Menu>();
            _canBeDamaged = true;
        }

        // Update is called once per frame
        void Update()
        {
            // Update UI Element
            HealthReadout.text = "Health: " +  PlayerHealth.ToString();
            HealthSlider.value = PlayerHealth;
            _checkDead();
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
            if (_canBeDamaged)
            {
                if (PlayerHealth <= 0)
                {
                    _canBeDamaged = false;
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
