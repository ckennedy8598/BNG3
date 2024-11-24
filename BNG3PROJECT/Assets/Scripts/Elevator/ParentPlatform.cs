using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//youtu.be/cW-5JYZLlvQ?si=PSUjY8CERavFwhxJ
namespace Platformer
{
    public class ParentPlatform : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            collision.transform.SetParent(transform);
        }
        private void OnCollisionExit(Collision collision)
        {
            collision.transform.SetParent(null);
        }
        // for above, if the object had a parent before, it could be problematic supposedly
        // is that why it won't go back down maybe




       
    }
}
