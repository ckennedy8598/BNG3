using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//youtu.be/cW-5JYZLlvQ?si=PSUjY8CERavFwhxJ
namespace Platformer
{
    public class TriggerPlatform : MonoBehaviour
    {

        PlatformMoving platform;

        private void Start ()
        {
            platform = GetComponent<PlatformMoving>();
        }

        private void OnTriggerEnter(Collider other)
        {
            platform.canMove = true;
        }
        


    }
}
