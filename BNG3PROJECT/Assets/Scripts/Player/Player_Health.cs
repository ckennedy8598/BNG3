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
        private float _dmgTimerCheck;
        private float _dmgTimer = 1f;
        public float HalfDamageValue = 1f;
        private bool _damageCooldown;
        public bool CanBeDamaged;
        public bool HalfDamage;
        public float PlayerHealth = 20f;
        public TMP_Text HealthReadout;
        public AudioSource HealthPickupSFX;
        public AudioSource OnHitSFX;
        public Animator anim;

        public GameObject UI;
        private bool _UIBool = true;


        // I moved the poison method to it's own script, as well as adding burn damage and frost damage. Nothing in the game
        // currently calls those, but they will in the future. - Chris
        //
        // P.S. I hope I didn't break anything by removing my method. I always get afraid that if I go in someone else's script I will accidently
        // mess something up lol

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
            HealthReadout.text = "Health: " + PlayerHealth.ToString();
            HealthSlider.value = PlayerHealth;
            _checkDead();

            if (Input.GetKeyDown(KeyCode.K))
            {
                _UIBool = !_UIBool;
                UI.SetActive(_UIBool);
            }

            if (HalfDamage)
            {
                HalfDamageValue = .5f;
            }
            else
                HalfDamageValue = 1f;
        }

        //Poison damage. Gonna rework this in the future. I want this to do a certian amount of damage a second - Chris
        
        

        private void LateUpdate()
        {
            _setDead();
        }

        public void IncreaseHealth(float amount)
        {
            if (PlayerHealth > 0)
            {
                PlayerHealth += amount;
                HealthPickupSFX.Play();
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
                    OnHitSFX.Play();
                    anim.SetTrigger("Player_Hurt");
                    PlayerHealth -= damage * HalfDamageValue;
                    _dmgCooldown();
                    //Debug.Log("!!!!!!CALLED DAMAGE COOLDOWN: PLAYER STATE INVULNERABLE!!!!!!");
                }
            }
        }

        private void _dmgCooldown()
        {
            CanBeDamaged = false;
            Invoke("_resetDmgCooldown", 1f);
        }

        private void _resetDmgCooldown()
        {
            CanBeDamaged = true;
            //Debug.Log("!!!!!PLAYER STATE CAN BE DAMAGED!!!!!");
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
