using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerPOS : MonoBehaviour
    {
        // Start is called before the first frame update

        private GameMaster gm;
        private bool _needRespawn;

        void Start()
        {
            _needRespawn = true;
        }

        private void Awake()
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        // Update is called once per frame
        void Update()
        {
        
        }

        private void LateUpdate()
        {
            if (_needRespawn)
            {
                transform.position = gm.LastCheckpointPOS;
                Debug.Log("Spawning player at coords: " + gm.LastCheckpointPOS);
                Debug.Log("Actual Player Coords: " + this.gameObject.transform.position);
                _needRespawn = false;
            }
        }



    }
}
