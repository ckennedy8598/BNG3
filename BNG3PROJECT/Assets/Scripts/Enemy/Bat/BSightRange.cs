using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BSightRange : MonoBehaviour
    {
        public BatFinal BF;

        
        void Awake()
        {
            BF = GetComponentInParent<BatFinal>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (BF.state != BatFinal.BatState.Die)
            {


                if (other.gameObject.CompareTag("Player"))
                {
                    BF.state = BatFinal.BatState.Walk;
                }
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (BF.state != BatFinal.BatState.Die)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    BF.ResetState();
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
