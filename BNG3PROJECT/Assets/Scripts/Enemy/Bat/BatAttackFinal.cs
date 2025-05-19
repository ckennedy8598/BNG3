using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BatAttackFinal : MonoBehaviour
    {
        public PlayerTickDamage ptd;
        public float meleeDamage = 15f;
        public Player_Health ph;
        //public Animator animator;
        void Start()
        {
            ptd = FindAnyObjectByType<PlayerTickDamage>();
            ph = FindAnyObjectByType<Player_Health>();
            //animator = this.GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (ph != null)
                {
                    Debug.Log("Ouchie");
                    ph.TakeDamage(meleeDamage);
                    ptd.isPoisoned = true;
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
