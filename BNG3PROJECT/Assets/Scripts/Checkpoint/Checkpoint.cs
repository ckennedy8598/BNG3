using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class Checkpoint : MonoBehaviour
    {
        // Start is called before the first frame update\

        private GameMaster gm;
        void Start()
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Checkpoint activated");
                gm.LastCheckpointPOS = transform.position;
            }
        }
    }
}
