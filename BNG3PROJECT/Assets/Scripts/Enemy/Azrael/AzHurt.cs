using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class AzHurt : MonoBehaviour
    {
        public float meleeDamage = 55f;
        public Player_Health ph;

        void Start()
        {

            ph = FindAnyObjectByType<Player_Health>();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Az Hits Hard!");
                ph.TakeDamage(meleeDamage);

            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
