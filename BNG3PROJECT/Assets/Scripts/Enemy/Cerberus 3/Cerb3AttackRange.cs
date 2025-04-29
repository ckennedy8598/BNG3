using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Cerb3AttackRange : MonoBehaviour
    {
        public Cerb3 C3;
        void Start()
        {
            C3 = GetComponentInParent<Cerb3>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (C3.State != Cerb3.CerbState.Die)
            {


                if (other.gameObject.CompareTag("Player"))
                {
                    C3.State = Cerb3.CerbState.Attack;
                }
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (C3.State != Cerb3.CerbState.Die)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    C3.State = Cerb3.CerbState.Walk;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
