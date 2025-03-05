using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class sightline : MonoBehaviour
    {
        public SlimeScriptfornow slime;
        

        // Start is called before the first frame update
        void Start()
        {
            slime = FindAnyObjectByType<SlimeScriptfornow>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                slime.playerSeen = true;
            }
        }
    }
}
