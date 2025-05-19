using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class MeleeSightRange : MonoBehaviour
    {
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
                    MF.State = MeleeEnemyFinal.MeleeState.Walk;
                }
            }

        }
        
        private void OnTriggerExit(Collider other)
        {
            if (MF.State != MeleeEnemyFinal.MeleeState.Die)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    MF.ResetState();
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
