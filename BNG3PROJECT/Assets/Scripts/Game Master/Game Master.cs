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
        public Player_Movement PlayerMovement_Script;

        private bool _gotPlayer;
        private bool _scoreUpdated;
        
        // CheckedAbilities referenced in PlayerPOS script
        public bool CheckedAbilities = false;
        public bool isrespawning = true;
        public bool MainMenu = true;

        private int _sceneNumber;
        private int _oldScore = 0;
        public int NewScore;

        // Variables for player abilities
        [Header("Collected Abilities")]
        public bool Dash;
        public bool SoulOverflow;
        public bool Ashes;

        public bool orbDash;
        public bool orbSoul;
        public bool orbAshes;
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

            _oldScore = 0;
            NewScore = 0;
            LastCheckpointPOS = new Vector3(4f, 1.5f, 5.5f);

            _gotPlayer = false;
            _scoreUpdated = false;
        }
        void Start()
        {
            _sceneNumber = SceneManager.GetActiveScene().buildIndex;
            if (MainMenu)
            {
                return;
            }
            else
            {
                Player = GameObject.FindWithTag("Player");
                PlayerMovement_Script = Player.GetComponent<Player_Movement>();
                _sceneAbilities();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (MainMenu)
            {
                return;
            }
            else
            {
                if (Player == null)
                {
                    Player = GameObject.Find("Player");
                }

                if (!CheckedAbilities)
                {
                    _sceneNumber = SceneManager.GetActiveScene().buildIndex;
                    _sceneAbilities();
                    CheckedAbilities = true;
                }
            }            
        }

        private void LateUpdate()
        {
            if (MainMenu)
            {
                return;
            }
            else
            {
                if (!_gotPlayer)
                {
                    Player = GameObject.Find("Player");
                    PlayerMovement_Script = Player.GetComponent<Player_Movement>();
                    _gotPlayer = true;
                }

                if (PlayerMovement_Script == null)
                {
                    PlayerMovement_Script = Player.GetComponent<Player_Movement>();
                }

                if (PlayerMovement_Script.PlayerScore < NewScore)
                {
                    PlayerMovement_Script.PlayerScore = NewScore;
                }
            }
        }
        

        // Set player abilities unlocked by scene
        private void _sceneAbilities()
        {
            // Infernus
            if (_sceneNumber == 1)
            {
                Dash = false;
                if (orbDash)
                {
                    Dash = true;
                }
                SoulOverflow = false;
                Ashes = false;
            }
            // Infernus Cerb Arena or Ergus
            else if (_sceneNumber == 2 || _sceneNumber == 3)
            {
                Dash = true;
                SoulOverflow = false;
                if (orbSoul)
                {
                    SoulOverflow |= true;
                }
                Ashes = false;
            }
            // Ergus Cerb Arena or Tundrus
            else if (_sceneNumber == 4 || _sceneNumber == 5)
            {
                Dash = true;
                SoulOverflow = true;
                Ashes = false;
                if (orbAshes)
                {
                    Ashes = true;
                }
            }
            // Tundrus Cerb Arena or Azrael Arena
            else if (_sceneNumber == 6 || _sceneNumber == 7)
            {
                Dash = true;
                SoulOverflow = true;
                Ashes = true;
            }
            // Default state when not on valid scene
            else
            {
                Dash = false;
                SoulOverflow = false;
                Ashes = false;
            }
            // else if last scene set do something
        }

        /*
        // Set initial respawn coordinates by scene
        private void _setLevelRespawnCoords()
        {
            switch (_sceneNumber)
            {
                // Infernus
                case 1:
                    LastCheckpointPOS = new Vector3(4f, 1.5f, 5.5f);
                    break;
                // Infernus Cerb Arena
                case 2:
                    LastCheckpointPOS = new Vector3(9.74f, 1.11f, 2.33f);
                    break;
                // Ergus
                case 3:
                    LastCheckpointPOS = new Vector3(23f, 1.3f, 0f);
                    break;
                // Ergus Cerb Arena
                case 4:
                    LastCheckpointPOS = new Vector3(9.74f, 1.69f, 2.33f);
                    break;
                // Tundrus - Change later?
                case 5:
                    LastCheckpointPOS = new Vector3(154f, 11.3f, 126f);
                    break;
                // Tundrus Cerb Arena
                case 6:
                    LastCheckpointPOS = new Vector3(9.74f, 1.69f, 2.33f);
                    break;
                default:
                    break;
            }
        }*/

        public void SetNewGameSettings()
        {
            LastCheckpointPOS = new Vector3(4f, 1.5f, 5.5f);
            _oldScore = 0;
            NewScore = 0;

            orbDash = false;
            orbSoul = false;
            orbAshes = false;
            //MainMenu = false; // Testing Main Menu Anim
            Debug.Log("SET NEW GAME SETTINGS!");
        }

        public void SetMainMenuTrue()
        {
            MainMenu = true;
        }
        public void GetNewScore()
        {
            NewScore = PlayerMovement_Script.PlayerScore;
            Debug.Log("Got new score!: " + PlayerMovement_Script.PlayerScore + " || Old Score!: " + _oldScore);
        }

        public void UpdateScore()
        {
            if (NewScore > _oldScore)
            {
                _oldScore = NewScore;
                PlayerMovement_Script.PlayerScore = NewScore;
                PlayerMovement_Script.CheckScore = true;
                Debug.Log("Updated Score!: " + PlayerMovement_Script.PlayerScore);
            }
        }
    }
}
