using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Platformer
{
    public class GameMaster : MonoBehaviour
    {
        // Start is called before the first frame update
        private static GameMaster instance;
        public Vector3 LastCheckpointPOS;

        public GameObject Player;

        public bool isrespawning = true;
        private void Awake()
        {
            
            


            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            } else
            {
                Destroy(gameObject);
            }

            
        }
        void Start()
        {

            

        }

        // Update is called once per frame
        void Update()
        {
            //if (isrespawning == true)
            //{
               // Debug.Log("respawning true");
               // if (Player != null)
                //{
                 //   Player.transform.position = LastCheckpointPOS;
                 //   isrespawning = false;
                //}
            //}

            if (Player == null)
            {
                Player = GameObject.FindWithTag("Player");
            }
            
        }
    }
}
