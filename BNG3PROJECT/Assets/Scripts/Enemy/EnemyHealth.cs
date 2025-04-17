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

        public float deathTimer = 1f;

        public Animator animator;

        //public GameObject AR;

        //public GameObject SR;



        private void Die()
        {
            Debug.Log("Enemy has been slain");
            Destroy(gameObject);
        }
        
        void Start()
        {
            //sprite = GetComponent<SpriteRenderer>();
            health = maxHealth;
            animator = this.GetComponentInChildren<Animator>();

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
                sprite.color = Color.white;
                animator.SetTrigger("isDead");
                
                //Invoke(nameof(Die), deathTimer);
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
