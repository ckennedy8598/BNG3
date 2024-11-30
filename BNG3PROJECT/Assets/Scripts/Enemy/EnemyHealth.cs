using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EnemyHealth : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] float health;
        [SerializeField] float maxHealth = 20f;

        private void Die()
        {
            Debug.Log("Enemy has been slain");
            Destroy(gameObject);
        }
        
        void Start()
        {
        health = maxHealth;
        }

        public void Hurt(float damage)
        {
            Debug.Log("Enemy has been hurt");
            health -= damage;
        }
        private void Update()
        {
            if (health <= 0)
            {
                Die();
            }
        }
    }

        // Update is called once per frame

}
