using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CerberusDeahWatcher : MonoBehaviour
    {
        public GameObject cerberusObject; // Assign the actual GameObject with CerberusFinal
        public GameObject bridgeToActivate;

        private bool hasFired = false;

        void Update()
        {
            if (!hasFired && cerberusObject == null)
            {
                Debug.Log("Cerberus object destroyed. Activating bridge.");
                bridgeToActivate.SetActive(true);
                hasFired = true;
            }
        }
    }
}
//yes i know death is spelled wrong idc