using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GymTransition : MonoBehaviour
    {
        public GameMaster gm;

        private void Awake()
        {
            //gm = gameObject.GetComponent<GameMaster>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "PlayerBody")
            {
                // 9.74  1.69  2.33
                gm.LastCheckpointPOS = new Vector3(10, 2, 2);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

    }
}
