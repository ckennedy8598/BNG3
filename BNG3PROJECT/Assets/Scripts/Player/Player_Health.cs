using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Platformer
{
    public class Player_Health : MonoBehaviour
    {
        [Header("Pause_Menu.cs Reference")]
        public Pause_Menu PM_Script;

        public float PlayerHealth = 20f;
        public TMP_Text HealthReadout;
        // Start is called before the first frame update
        void Start()
        {
            PM_Script = FindAnyObjectByType<Pause_Menu>();
            PlayerHealth = 20f;
        }

        // Update is called once per frame
        void Update()
        {
            // Update UI Element
            HealthReadout.text = "Health: " +  PlayerHealth.ToString();
            _checkDead();
        }

        public void TakeDamage(float damage)
        {
            if(PlayerHealth <= 0)
            {
                return;
            }
            else
            {
                PlayerHealth -= damage;
            }
        }

        private void _checkDead()
        {
            if (PlayerHealth <= 0)
            {
                PM_Script.PlayerDead = true;
                Debug.Log("This is PM_Script.PlayeDead Status: " + PM_Script.PlayerDead);
                gameObject.SetActive(false);
            }
        }
    }
}
