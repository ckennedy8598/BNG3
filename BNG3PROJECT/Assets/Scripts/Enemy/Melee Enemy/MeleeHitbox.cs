using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class MeleeHitbox : MonoBehaviour
    {
        public float meleeDamage = 30f;
        public Player_Health ph;
        void Start ()
        {
            ph = FindAnyObjectByType<Player_Health>();
        }
        // Update is called once per frame

        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (ph != null)
                {
                    ph.TakeDamage(meleeDamage);
                }
            }
        }
        void Update()
        {
        
        }
    }
}
