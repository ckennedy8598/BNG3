using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerPOS : MonoBehaviour
    {
        // Start is called before the first frame update

        private GameMaster gm;

        void Start()
        {
            
        }

        private void Awake()
        {
            Debug.Log("Spawning player");
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            transform.position = gm.LastCheckpointPOS;
        }
        // Update is called once per frame
        void Update()
        {
        
        }

          
        
    }
}
