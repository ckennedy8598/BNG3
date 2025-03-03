using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//youtu.be/cW-5JYZLlvQ?si=PSUjY8CERavFwhxJ
namespace Platformer
{
    public class ParentPlatform : MonoBehaviour
    {
        public bool onPlatform;
        private PlatformMoving platformScript;

        [SerializeField] float checkDistance = 2f; // Adjust based on elevator size
        [SerializeField] LayerMask playerLayer;   // Assign "Player" layer in Inspector
        [SerializeField] Transform resetPosition; // Assign a reset point in Unity Inspector

        private void Start()
        {
            platformScript = GetComponent<PlatformMoving>(); // Get reference to PlatformMoving script
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Player")
            {
                onPlatform = true;
                collision.transform.SetParent(transform);
            }
            
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.name == "Player")
            {
                onPlatform = false;
                collision.transform.SetParent(null);
            }
        }
        private void FixedUpdate()
        {
            if (CheckForPlayerUnderneath())
            {
                platformScript.canMove = false; // Stop the elevator if player is below
                Debug.Log("Player detected under elevator! Stopping movement and reset.");
                transform.position = resetPosition.position; // Move elevator to reset position
            }
        }

        bool CheckForPlayerUnderneath()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, checkDistance, playerLayer))
            {
                return true; // Player detected under the elevator
            }
            return false;
        }
    }
}
