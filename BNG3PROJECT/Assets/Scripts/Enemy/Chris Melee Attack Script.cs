using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ChrisMeleeAttackScript : MonoBehaviour
    {
        // Start is called before the first frame update
        public float despawnTimer = 1f;
        public float force = 2f;
        private float timer;
        public float meleeDamage = 25f;

        public Player_Health playerHealth;
        Rigidbody rb;

        public GameObject enemyMelee;
        public GameObject player;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            player = GameObject.FindGameObjectWithTag("Player");

            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector3(direction.x, direction.y, direction.z).normalized * force;
            playerHealth = FindAnyObjectByType<Player_Health>();
        }
        void Update()
        {
            timer += Time.deltaTime;
            if (timer > despawnTimer)
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {


            if (other.gameObject.CompareTag("Player"))
            {
                playerHealth.TakeDamage(meleeDamage);
                Debug.Log("Player has been hit D:");
                Destroy(gameObject);
            }
        }
    }
}
