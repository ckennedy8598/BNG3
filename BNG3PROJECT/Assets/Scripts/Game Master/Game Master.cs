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

        private void Awake()
        {
            

            if(instance == null)
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

            Player = GameObject.Find("Player");
            Debug.Log("Player spawned");
            Player.transform.position = LastCheckpointPOS;

        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
