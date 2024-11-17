using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Platformer;
using UnityEngine;

public class EnemyBulletTest : MonoBehaviour
{

    public float despawnTimer = 5f;
    public float force = 10f;
    private float timer;
    public float bulletDamage = 5f;

    public Player_Health playerHealth;
    Rigidbody rb;

    public GameObject enemyBullet;
    public GameObject player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector3(direction.x, direction.y, direction.z).normalized * force;
        playerHealth = FindAnyObjectByType<Player_Health>();
    }
    void Update()
    {
        
        timer += Time.deltaTime;
        if(timer > despawnTimer)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(bulletDamage);
            Debug.Log("Player has been shot D:");
            Destroy(gameObject);
        }
    }
}
