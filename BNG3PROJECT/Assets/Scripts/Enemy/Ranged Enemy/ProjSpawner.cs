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
            //AU = GetComponent<AudioClip>();
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
