using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//youtu.be/cW-5JYZLlvQ?si=PSUjY8CERavFwhxJ
namespace Platformer
{
    public class ParentPlatform : MonoBehaviour
    {
        public bool onPlatform;
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
    }
}
