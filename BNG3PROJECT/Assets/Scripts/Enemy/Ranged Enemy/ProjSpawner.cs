using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ProjSpawner : MonoBehaviour
    {
        public GameObject enemyBullet;

        public Animator animator;

        public RangedEnemyFinal RF;
        public void spawnProjectile()
        {
            Instantiate(enemyBullet, transform.Find("SpawnPoint").position, Quaternion.identity);
        }

        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }
        public void ResetShot()
        {
            animator.SetTrigger("endAttack");
        }
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
