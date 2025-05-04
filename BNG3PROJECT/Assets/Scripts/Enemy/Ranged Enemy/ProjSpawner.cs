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

        public AudioClip AU;

        public Player_Health playerHealth;

        public void spawnProjectile()
        {
            if(playerHealth.PlayerHealth >= 0)
            {
                Instantiate(enemyBullet, transform.Find("SpawnPoint").position, Quaternion.identity);
            }
            
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
            //AU = GetComponent<AudioClip>();
            playerHealth = FindAnyObjectByType<Player_Health>();
        }

        public void ShootSound()
        {
            AudioSource.PlayClipAtPoint(AU, transform.position, 0.2f);
        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
