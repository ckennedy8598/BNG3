using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{


    public class DoorController : MonoBehaviour
    {
        private Rigidbody doorRigidbody;
        public Collider keypadCollider;
        public AudioClip TargetHit;
        private HingeJoint doorHinge;
        private bool isDoorLocked = true; // Door is locked initially

        public bool isPuzzleGateUnlocked = false;

        // Start is called before the first frame update
        void Start()
        {
            doorRigidbody = GetComponent<Rigidbody>();

            // Lock the door initially by freezing position and rotation
            LockDoor();
            Debug.Log("Door has been initially locked");
        }


        void UnlockDoor()
        {
            // Unfreeze position and rotation so the door can move
            doorRigidbody.constraints = RigidbodyConstraints.None;
            isDoorLocked = false;
            Debug.Log("Door has been unlocked");
        }

        // Call this function to lock the door
        void LockDoor()
        {
            // Lock position and rotation to prevent movement
            doorRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            isDoorLocked = true;
            Debug.Log("Door has been locked");
        }

        // Function to handle keypad damage
        public void DamageKeypad()
        {
            // If the keypad is damaged, unlock the door
            if (isDoorLocked)
            {
                UnlockDoor();
                Debug.Log("Door has been unlocked due to damage");
              
            }
        }

        // only works with fireball for the time being since I adapted a portion of the Target's code here
        void OnTriggerEnter(Collider other)
        {
            if (!isPuzzleGateUnlocked) return;

            if (other.gameObject.name == "Fireball(Clone)" || other.CompareTag("Reflected"))
            {
                // Simulate damage on keypad and unlock the door
                DamageKeypad();
                Debug.Log("Keypad has been damaged by fireball");
                if (TargetHit != null)
                {
                    AudioSource.PlayClipAtPoint(TargetHit, gameObject.transform.position);
                }
                
            }
        }
    }
}
    
   

