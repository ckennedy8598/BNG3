using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class MeleeAttackRange : MonoBehaviour
    {
        // Start is called before the first frame update
        public MeleeEnemyFinal MF;
        void Start()
        {
            MF = GetComponentInParent<MeleeEnemyFinal>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (MF.State != MeleeEnemyFinal.MeleeState.Die)
            {


                if (other.gameObject.CompareTag("Player"))
                {
                    MF.State = MeleeEnemyFinal.MeleeState.Attack;
                }
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (MF.State != MeleeEnemyFinal.MeleeState.Die)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    MF.State = MeleeEnemyFinal.MeleeState.Walk;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
