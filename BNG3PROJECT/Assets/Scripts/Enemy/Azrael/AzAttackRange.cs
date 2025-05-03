using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class AzAttackRange : MonoBehaviour
    {
        // Start is called before the first frame update
        public AzBaseScript AZ;
        void Start()
        {
        AZ = GetComponentInParent<AzBaseScript>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (AZ.State != AzBaseScript.AzState.Die)
            {


                if (other.gameObject.CompareTag("Player"))
                {
                    AZ.State = AzBaseScript.AzState.Attack;
                }
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (AZ.State != AzBaseScript.AzState.Die)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    AZ.State = AzBaseScript.AzState.Walk;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
