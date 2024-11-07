using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GymTransition : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "PlayerBody")
            {
                SceneManager.LoadScene(1);
            }
        }

    }
}
