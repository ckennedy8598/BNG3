using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CerbAttackRange : MonoBehaviour
    {
        // Start is called before the first frame update
        public CerberusFinal CF;
        void Start()
        {
        CF = GetComponentInParent<CerberusFinal>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (CF.State != CerberusFinal.CerbState.Die)
            {


                if (other.gameObject.CompareTag("Player"))
                {
                    CF.State = CerberusFinal.CerbState.Attack;
                }
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (CF.State != CerberusFinal.CerbState.Die)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    CF.State = CerberusFinal.CerbState.Walk;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
