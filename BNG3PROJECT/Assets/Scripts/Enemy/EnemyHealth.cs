using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EnemyHealth : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] float health;
        [SerializeField] float maxHealth = 100f;

        private bool isHurt = false;

        public SpriteRenderer sprite;

        


        private void Die()
        {
            Debug.Log("Enemy has been slain");
            Destroy(gameObject);
        }
        
        void Start()
        {
            //sprite = GetComponent<SpriteRenderer>();
            health = maxHealth;
        }

        public void Hurt(float damage)
        {
            isHurt = true;
            damageFlash();
            //Debug.Log("Enemy has been hurt");
            health -= damage;

            
            
        }
        private void Update()
        {
            if (health <= 0)
            {
                Die();
            }
        }

        public void damageFlash()
        {
            if(isHurt == true)
            {
                Debug.Log("Damage Flash");
                sprite.color = Color.red;
                Invoke(nameof(resetFlash), 0.1f);
            }
        }

        public void resetFlash()
        {
            Debug.Log("Damage Flash Reset");
            sprite.color = Color.white;
            isHurt = false;
        }
    }

        // Update is called once per frame

}
