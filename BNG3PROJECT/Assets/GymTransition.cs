using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GymTransition : MonoBehaviour
    {
        public GameObject GameMaster;
        public GameMaster gm_script;

        private bool _gotGM = false;
        private int _sceneNumber;

        private void Awake()
        {
            _sceneNumber = SceneManager.GetActiveScene().buildIndex;
        }

        private void LateUpdate()
        {
            if (!_gotGM || GameMaster == null)
            {
                GameMaster = GameObject.Find("Game Master");
                gm_script = GameMaster.GetComponent<GameMaster>();
                _gotGM = true;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "PlayerBody")
            {
                gm_script.GetNewScore();
                _setLevelRespawnCoords();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        // Set respawn coords with index offset of +1
        public void _setLevelRespawnCoords()
        {
            switch (_sceneNumber)
            {
                // Infernus Cerb Arena
                case 1:
                    gm_script.LastCheckpointPOS = new Vector3(9.74f, 1.11f, 2.33f);
                    break;
                // Ergus ck adjust
                case 2:
                    gm_script.LastCheckpointPOS = new Vector3(14.91f, 1.224f, 0f);
                    break;
                // Ergus Cerb Arena
                case 3:
                    gm_script.LastCheckpointPOS = new Vector3(9.74f, 1.69f, 2.33f);
                    break;
                // Tundrus - Change later? ck fix :)
                case 4:
                    gm_script.LastCheckpointPOS = new Vector3(-25.66f, -30.85f, 101.97f);
                    break;
                // Tundrus Cerb Arena
                case 5:
                    gm_script.LastCheckpointPOS = new Vector3(9.74f, 1.69f, 2.33f);
                    break;
                // Azrael Arena
                case 6:
                    gm_script.LastCheckpointPOS = new Vector3(9.74f, 1.69f, 2.33f);
                    break;
                default:
                    break;
            }
        }

    }
}
