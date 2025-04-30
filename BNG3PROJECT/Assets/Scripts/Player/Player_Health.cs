using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using System.Runtime.CompilerServices;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class Player_Health : MonoBehaviour
    {
        [Header("Pause_Menu.cs Reference")]
        /*private Player_Health _ph_Script;
        
        private Player_Dashing _pd_Script;
        private Player_Attacking _pa_Script;
        private Player_Movement _player_M_Script;*/
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
        public GameObject Weapon;
        private bool _UIBool = true;


        // I moved the poison method to it's own script, as well as adding burn damage and frost damage. Nothing in the game
        // currently calls those, but they will in the future. - Chris
        //
        // P.S. I hope I didn't break anything by removing my method. I always get afraid that if I go in someone else's script I will accidently
        // mess something up lol

        // Start is called before the first frame update
        void Start()
        {
            /*_ph_Script = GetComponent<Player_Health>();
            
            _pd_Script = GetComponent<Player_Dashing>();
            _pa_Script = GetComponent<Player_Attacking>();
            _player_M_Script = GetComponent<Player_Movement>();*/
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


            // Turn off HUD
            if (Input.GetKeyDown(KeyCode.K))
            {
                _UIBool = !_UIBool;
                Weapon.SetActive(_UIBool);
                UI.SetActive(_UIBool);
            }

            // Button for Cerb Arena
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene(2);
                this.transform.position = new Vector3(9.74f, 1.11f, 2.33f);
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
                /*
                _ph_Script.enabled = false;
                _pa_Script.enabled = false;
                _player_M_Script.enabled = false;
                _pd_Script.enabled = false;*/

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
