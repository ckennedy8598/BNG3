using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Weapon_GetCollision : MonoBehaviour
    {
        public EnemyHealth ehealth;
        public int Damage;

        private void Start()
        {
            Damage = 0;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("Collided with " + other.gameObject.name);
                Debug.Log("Collided with " + gameObject.name);

                other.gameObject.GetComponent<EnemyHealth>().Hurt(Damage);
                //Destroy(other.gameObject);
            }
        }

        public void DealDamage(int dam)
        {
            Damage = dam;
        }
    }
}
