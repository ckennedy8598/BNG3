using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class Checkpoint : MonoBehaviour
    {
        // Start is called before the first frame update

        private int _sceneNumber;
        private GameMaster gm;
        public AudioClip CheckpointSFX;

        private void Awake()
        {

        }
        void Start()
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyUp(KeyCode.I)) 
            {
                gm.isrespawning = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Checkpoint activated");
                gm.LastCheckpointPOS = transform.position;
                AudioSource.PlayClipAtPoint(CheckpointSFX, gameObject.transform.position);
            }
        }
    }
}
