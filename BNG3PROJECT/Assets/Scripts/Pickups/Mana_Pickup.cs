using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Mana_Pickup : MonoBehaviour
    {
        [Header("Player Script Reference")]
        public Player_Attacking PAttack_Script;

        private SpriteRenderer SpriteRender;
        private SphereCollider SphereCollider;
        public float ManaAmount;
        //public TMP_Text Counter_Text;
        //public AudioSource SFX;
        void Start()
        {
            PAttack_Script = FindAnyObjectByType<Player_Attacking>();
            SpriteRender = GetComponent<SpriteRenderer>();
            SphereCollider = GetComponent<SphereCollider>();
        }

        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                PAttack_Script.IncreaseMana(ManaAmount);
                SetDead();
                Destroy(gameObject, 5);
            }
        }

        public void SetDead()
        {
            SpriteRender.enabled = false;
            SphereCollider.enabled = false;
        }
    }
}
