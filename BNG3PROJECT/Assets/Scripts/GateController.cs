using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class GateController : MonoBehaviour
    {
        public GameObject[] lockVisuals = new GameObject[3];
        public DoubleDoorController doubleDoorController;

        void Start()
        {
            for (int i = 0; i < PuzzleProgress.keyUnlocked.Length; i++)
            {
                if (PuzzleProgress.keyUnlocked[i] && lockVisuals[i] != null)
                {
                    lockVisuals[i].SetActive(false);
                }
            }

            if (AllUnlocked())
            {
                doubleDoorController.isPuzzleGateUnlocked = true; 
            }
        }

        public void Unlock(int keyID)
        {
            if (AllUnlocked())
            {
                Debug.Log("All skull key locks removed. Gate is now able to be opened at Midway.");
                doubleDoorController.isPuzzleGateUnlocked = true;
                
            }
        }

        private bool AllUnlocked()
        {
            foreach (bool b in PuzzleProgress.keyUnlocked)
                if (!b) return false;
            return true;
        }
    }
}
