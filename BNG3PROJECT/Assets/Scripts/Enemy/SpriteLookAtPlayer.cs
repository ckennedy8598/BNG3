using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class SpriteLookAtPlayer : MonoBehaviour
    {
        // Start is called before the first frame update
        public Transform player;

        public void Awake()
        {
            player = GameObject.Find("Player").transform;
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(player);
        }
    }
}
