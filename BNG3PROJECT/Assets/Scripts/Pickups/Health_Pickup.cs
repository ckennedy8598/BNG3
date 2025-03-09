using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platformer
{
    public class Health_Pickup : MonoBehaviour
    {
        [Header("Player Script Reference")]
        public Player_Health PHScript;

        private GameObject HealthAnimUIObj;
        public Animator HealthAnimator;

        private SpriteRenderer SpriteRender;
        private SphereCollider SphereCollider;
        public float HealingAmount;
        //public TMP_Text Counter_Text;
        //public AudioSource SFX;
        void Start()
        {
            HealthAnimUIObj = GameObject.Find("Health_Pickup_Anim");
            HealthAnimator = HealthAnimator.GetComponent<Animator>();

            PHScript = FindAnyObjectByType<Player_Health>();
            //SFX = GetComponent<AudioSource>();
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
                //SFX.Play();
                HealthAnimator.SetTrigger("PickedUp");
                PHScript.IncreaseHealth(HealingAmount);
                SetDead();
                Destroy(gameObject, 5);

                // TESTING DAMAGE ONLY
                //PHScript.TakeDamage(5);
            }
        }

        public void SetDead()
        {
            SpriteRender.enabled = false;
            SphereCollider.enabled = false;
        }
    }
}
