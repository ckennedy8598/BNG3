using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PuzzleGateController : MonoBehaviour
    {
        public DoorController doorController;  // Drag your existing DoorController object here
        public int totalTargets = 0;

        private int destroyedTargets = 0;

        public void NotifyTargetDestroyed()
        {
            destroyedTargets++;

            if (destroyedTargets >= totalTargets)
            {
                OpenDoor();
            }
        }

        private void OpenDoor()
        {
            if (doorController != null)
            {
                doorController.isPuzzleGateUnlocked = true; //  Allow projectile interaction
                doorController.DamageKeypad();               // Trigger door unlock
                Debug.Log("Puzzle complete — door unlock triggered.");
            }
        }
    }
}       
   
    
