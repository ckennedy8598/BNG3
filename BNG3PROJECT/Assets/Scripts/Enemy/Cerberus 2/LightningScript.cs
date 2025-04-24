using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class LightningScript : MonoBehaviour
    {
        // Start is called before the first frame update
        public Transform player;
        public Player_Health playerHealth;
        public float damage = 30f;
        void Start()
        {
            player = GameObject.Find("Player").transform;
            playerHealth = FindAnyObjectByType<Player_Health>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            this.transform.LookAt(targetPostition);
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerHealth.TakeDamage(damage);
            }
        }

        public void lightningEnd()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
