using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class GroundFireScript : MonoBehaviour
    {
        // Start is called before the first frame update
        public PlayerTickDamage ptd;
        public Player_Health playerHealth;
        public GameObject player;

        public float despawnTimer = 3f;
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            playerHealth = FindAnyObjectByType<Player_Health>();

            ptd = FindAnyObjectByType<PlayerTickDamage>();

            despawnTimer = 3f;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player was burned by fire");
                ptd.isBurned = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            despawnTimer -= Time.deltaTime;
            if (despawnTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
