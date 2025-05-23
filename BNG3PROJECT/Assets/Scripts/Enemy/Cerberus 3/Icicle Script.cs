using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

namespace Platformer
{
    public class NewBehaviourScript : MonoBehaviour
    {
        public Player_Attacking PA_Script;
        public EnemyHealth EH_Script;

        public float despawnTimer = 5f;
        public float force = 40f;
        private float timer;
        public float bulletDamage = 10f;

        public Player_Health playerHealth;
        Rigidbody rb;

        public Camera MainCamera;
        public GameObject enemyBullet;
        public GameObject player;

        public PlayerTickDamage ptd;
        public Transform playerT;

        public AudioClip AU;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            player = GameObject.FindGameObjectWithTag("Player");
            MainCamera = FindAnyObjectByType<Camera>();
            PA_Script = FindAnyObjectByType<Player_Attacking>();
            EH_Script = FindAnyObjectByType<EnemyHealth>();

            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector3(direction.x, direction.y, direction.z).normalized * force;
            playerHealth = FindAnyObjectByType<Player_Health>();
            playerT = GameObject.Find("Player").transform;
            ptd = GetComponent<PlayerTickDamage>();

            Vector3 targetPostition = new Vector3(playerT.position.x, this.transform.position.y, playerT.position.z);
            this.transform.LookAt(targetPostition);
        }
        void Update()
        {
            
            timer += Time.deltaTime;
            if (timer > despawnTimer)
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.CompareTag("Wall"))
            {
                AudioSource.PlayClipAtPoint(AU, transform.position, 1f);
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Ground"))
            {
                //put the spawning of the 'fire' object here
                AudioSource.PlayClipAtPoint(AU, transform.position, 1f);
                Debug.Log("hit ground");
            }

            if (other.gameObject.CompareTag("Player"))
            {
                if (PA_Script.CanParry) // - B
                {
                    rb.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y - .25f, MainCamera.transform.position.z);
                    rb.velocity = MainCamera.transform.forward * force;
                    gameObject.tag = "Reflected";
                    PA_Script.ParrySound.Play();
                }
                else
                {
                    playerHealth.TakeDamage(bulletDamage);
                    AudioSource.PlayClipAtPoint(AU, transform.position, 1f);
                    //ptd is causing a crash that I will fix later maybe - Chris
                    //ptd.isFrosted = true;
                    Debug.Log("Player has been shot D:");
                    Destroy(gameObject);
                }
            }
            else if (other.gameObject.CompareTag("Enemy") && gameObject.tag == "Reflected") // - B
            {
                other.gameObject.GetComponent<EnemyHealth>().Hurt(bulletDamage);
                Debug.Log("Enemy got rekt by reflected projectile!");
                Destroy(gameObject);
            }
        }
    }
}


