using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Cerb1BossTrigger : MonoBehaviour
    {
        // Start is called before the first frame update

        public GameObject Cerb;

        public GameObject EGate;
        void Start()
        {
           
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Cerb.SetActive(true);
                EGate.SetActive(false);
                Destroy(gameObject);
            }
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
