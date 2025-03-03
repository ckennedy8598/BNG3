using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//youtu.be/cW-5JYZLlvQ?si=PSUjY8CERavFwhxJ
//the vertically moving elevators that i first made use this script as well as TriggerPlatform & ParentPlatform

//this was here by default
namespace Platformer
{
    public class PlatformMoving : MonoBehaviour
    {
        public bool canMove;

        [SerializeField] float speed;
        [SerializeField] int startPoint;
        [SerializeField] Transform[] points;
        [SerializeField] float checkDistance = 2f; //distance to check for the player
        [SerializeField] LayerMask playerLayer;   // Assign "Player" layer in the Inspector
        [SerializeField] Transform resetPosition; // Assign a reset position for safety

        int i;
        bool reverse;


        // Start is called before the first frame update
        void Start()
        {
            transform.position = points[startPoint].position;
            i = startPoint;

        
        }

        // Update is called once per frame
       // changing this from void Update() to void FixedUpdate() fixes the jutter
        void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, points[i].position) < 0.01f)
            {
                canMove = false;

                if(i == points.Length - 1)
                {
                    reverse = true;
                    i--;
                    return;
                    
                } else if (i == 0)
                {
                    reverse = false;
                    i++;
                    return;
                }

                if (reverse)
                {
                    i--;
                }
                else
                {
                    i++;
                }
            }

            // **Check if moving down AND if a player is detected**
            if (canMove && reverse)
            {
                if (CheckForPlayerUnderneath())
                {
                    Debug.Log("Player detected under elevator! Stopping.");
                    canMove = false;
                    return;
                }
            }

            if (canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
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
}
