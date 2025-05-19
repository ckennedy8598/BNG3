using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class REAttackRange : MonoBehaviour
    {
        public RangedEnemyFinal REFinal;
        void Awake()
        {
            REFinal = GetComponentInParent<RangedEnemyFinal>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                REFinal.State = RangedEnemyFinal.DeviState.Attack;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                REFinal.State = RangedEnemyFinal.DeviState.Walk;
            }
        }
        void Update()
        {
        
        }
    }
}
