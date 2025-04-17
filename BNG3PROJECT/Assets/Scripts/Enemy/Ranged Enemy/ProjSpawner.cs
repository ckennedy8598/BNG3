using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ProjSpawner : MonoBehaviour
    {
        public GameObject enemyBullet;
        public void spawnProjectile()
        {
            Instantiate(enemyBullet, transform.Find("SpawnPoint").position, Quaternion.identity);
        }

        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
