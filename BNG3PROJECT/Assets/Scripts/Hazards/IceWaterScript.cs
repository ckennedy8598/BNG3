using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class IceWaterScript : MonoBehaviour
    {
        public float lavaDamage = 10f;
        public Player_Health playerHealth;
        public GameObject slime;
        public PlayerTickDamage ptd;

        public GameObject player;

        //ck add
        private bool playerInLava = false;
        private Coroutine delayedDamageCoroutine = null;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            playerHealth = FindAnyObjectByType<Player_Health>();

            slime = GameObject.Find("Slime");

            ptd = FindAnyObjectByType<PlayerTickDamage>();
        }

        //ck edited 4-14-25
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerHealth.TakeDamage(lavaDamage);
               // ptd.isFrosted = true;
                playerInLava = true;
                delayedDamageCoroutine = StartCoroutine(DelayedSecondHit());
                Debug.Log("Player hit from lava hit");
            }
            if (other.gameObject == slime)
            {
                Destroy(other.gameObject);
            }
        }

        //ck add
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerInLava = false;
                if (delayedDamageCoroutine != null)
                {
                    StopCoroutine(delayedDamageCoroutine);
                    delayedDamageCoroutine = null;
                }
            }
        }

        //ck add
        private IEnumerator DelayedSecondHit()

        {
            while (playerInLava)

            {
                yield return new WaitForSeconds(3f);

                if (playerInLava)
                {
                    playerHealth.TakeDamage(lavaDamage);
                    Debug.Log("Player hit with another wave of main lava damage");
                }
            }

            delayedDamageCoroutine = null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
