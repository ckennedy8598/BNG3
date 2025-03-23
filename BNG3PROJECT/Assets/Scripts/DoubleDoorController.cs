using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{ 
    // Leave all three objects outside of any container, they get funky if theyre in a container. Since its only one cerb door per level, this shouldnt
   // be a huge deal for organizational purposes. Those 3 objects being the left door, right door, and keypad. Assign this script to keypad then
   // plug in the proper things in inspector version of this

    public class DoubleDoorController : MonoBehaviour
    {
        public Rigidbody leftdoorRigidbody;
        public Rigidbody rightdoorRigidbody;

        public HingeJoint leftdoorHinge;
        public HingeJoint rightdoorHinge;

        public Collider keypadCollider;
        public AudioClip TargetHit;

        private bool isDoorLocked = true;

        void Start()
        {
            // Freeze both doors at start
            LockDoor();
        }

        void LockDoor()
        {
            if (leftdoorRigidbody != null)
            {
                leftdoorRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                Debug.Log("Left Door locked.");
            }

            if (rightdoorRigidbody != null)
            {
                rightdoorRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                Debug.Log("Right Door locked.");
            }

            isDoorLocked = true;
        }

        void UnlockDoor()
        {
            if (leftdoorRigidbody != null)
            {
                leftdoorRigidbody.constraints = RigidbodyConstraints.None;
                Debug.Log("Left Door unlocked.");
            }

            if (rightdoorRigidbody != null)
            {
                rightdoorRigidbody.constraints = RigidbodyConstraints.None;
                Debug.Log("Right Door unlocked.");
            }

            isDoorLocked = false;
        }

        public void DamageKeypad()
        {
            if (isDoorLocked)
            {
                UnlockDoor();
                SwingDoors();
                Debug.Log("Keypad damaged, Doors unlocking.");
            }
        }

        void SwingDoors()
        {
            if (leftdoorHinge != null)
            {
                leftdoorHinge.useMotor = true;
                JointMotor leftMotor = leftdoorHinge.motor;
                leftMotor.targetVelocity = -90f;
                leftMotor.force = 100f;
                leftMotor.freeSpin = false;
                leftdoorHinge.motor = leftMotor;
            }

            if (rightdoorHinge != null)
            {
                rightdoorHinge.useMotor = true;
                JointMotor rightMotor = rightdoorHinge.motor;
                rightMotor.targetVelocity = 90f;
                rightMotor.force = 100f;
                rightMotor.freeSpin = false;
                rightdoorHinge.motor = rightMotor;
            }

            Debug.Log("Swinging both doors.");
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Fireball(Clone)")
            {
                if (TargetHit != null)
                    AudioSource.PlayClipAtPoint(TargetHit, transform.position);

                DamageKeypad();
                Destroy(other.gameObject); 
            }
        }
    }
}
