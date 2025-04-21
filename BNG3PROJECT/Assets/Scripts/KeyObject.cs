using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class KeyObject : MonoBehaviour
    {
        public int keyID; // 0, 1, or 2 – manually assign in inspector
        public GameObject lockVisual; // light for this key here
        public GateController gateController; // central controller

        private void Start()
        {
            // Check if already collected previously
            if (PuzzleProgress.keyUnlocked[keyID])
            {
                if (lockVisual != null)
                    lockVisual.SetActive(false); // hide light visual
               
                Destroy(gameObject); // key already picked up
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PuzzleProgress.keyUnlocked[keyID] = true;

                if (lockVisual != null)
                    lockVisual.SetActive(false);

                gateController.Unlock(keyID);
                Destroy(gameObject);

                Debug.Log("A skull key was gathered");
            }
        }
    }
}