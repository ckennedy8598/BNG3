using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GameMaster : MonoBehaviour
    {
        // Start is called before the first frame update
        private static GameMaster instance;
        public Vector3 LastCheckpointPOS;

        public GameObject Player;

        public bool isrespawning = true;

        private int _sceneNumber;

        // Variables for player abilities
        [Header("Collected Abilities")]
        public bool Dash;
        public bool SoulOverflow;
        public bool Ashes;
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
            _sceneNumber = SceneManager.GetActiveScene().buildIndex;
            _sceneAbilities();
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

        private void _sceneAbilities()
        {
            if (_sceneNumber == 1)
            {
                Dash = false;
                SoulOverflow = false;
                Ashes = false;
            }
            else if (_sceneNumber == 2 || _sceneNumber == 3)
            {
                Dash = true;
                SoulOverflow = false;
                Ashes = false;
            }
            // else if last scene set do something
        }
    }
}
