using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Cerb2AttackRange : MonoBehaviour
    {
        // Start is called before the first frame update
        public Cerb2 C2;
        void Start()
        {
            C2 = GetComponentInParent<Cerb2>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (C2.State != Cerb2.CerbState.Die)
            {


                if (other.gameObject.CompareTag("Player"))
                {
                    C2.State = Cerb2.CerbState.Attack;
                }
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (C2.State != Cerb2.CerbState.Die)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    C2.State = Cerb2.CerbState.Walk;
                }
            }
        }
        void Update()
        {
        
        }
    }
}
