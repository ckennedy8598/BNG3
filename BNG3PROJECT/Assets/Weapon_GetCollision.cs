using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Weapon_GetCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("Collided with " + other.gameObject.name);
                Debug.Log("Collided with " + gameObject.name);
                Destroy(other.gameObject);
            }
        }
    }
}
