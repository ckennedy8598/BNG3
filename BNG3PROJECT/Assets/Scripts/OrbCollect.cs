using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class OrbCollect : MonoBehaviour
    {
        private int _sceneNumber;
        public GameObject GameMaster;
        public GameMaster GameMasterScript;
        void Start()
        {
            GameMaster = GameObject.Find("Game Master");
            GameMasterScript = GameMaster.GetComponent<GameMaster>();
            _sceneNumber = SceneManager.GetActiveScene().buildIndex;
            //Debug.Log("Sphere Build Index Number: " +  _sceneNumber);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "PlayerBody")
            {
                // Unlock appropriate ability for scene
                switch (_sceneNumber)
                {
                    case 1:
                        GameMasterScript.Dash = true;
                        GameMasterScript.orbDash = true;
                        Debug.Log("GameMaster Dash Variable from Orb: " + GameMasterScript.Dash);
                        break;
                    case 3:
                        GameMasterScript.SoulOverflow = true;
                        GameMasterScript.orbSoul = true;
                        break;
                    case 5:
                        GameMasterScript.Ashes = true;
                        GameMasterScript.orbAshes = true;
                        break;
                    default:
                        Debug.Log("Sphere Default Case");
                        break;
                }
                this.gameObject.SetActive(false);
            }
        }
    }
}
