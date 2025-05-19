using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ResetEnemyButton : MonoBehaviour
    {
        [Header("Enemy Settings")]
        public GameObject enemyPrefab;    
        public Transform spawnPoint;       // spawn location (Empty GameObject)

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Fireball>() != null)
            {
                Debug.Log("Reset Enemy Button hit.");
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
