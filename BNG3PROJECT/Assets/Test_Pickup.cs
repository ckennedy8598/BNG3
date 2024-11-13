using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Platformer;

public class Test_Pickup : MonoBehaviour
{
    [Header("Player Script Reference")]
    public Player_Health PHScript;

    private MeshRenderer PickupMesh;
    private SphereCollider SphereCollider;
    public TMP_Text Counter_Text;
    public AudioSource SCREAM;
    void Start()
    {
        PHScript = FindAnyObjectByType<Player_Health>();
        SCREAM = GetComponent<AudioSource>();
        PickupMesh = GetComponent<MeshRenderer>();
        SphereCollider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SCREAM.Play();
            GameObject.Find("Player").GetComponent<Player_Movement>().PlayerScore++;
            SetDead();
            Destroy(gameObject, 5);

            // TESTING DAMAGE ONLY
            PHScript.TakeDamage(5);
        }
    }

    public void SetDead()
    {
        PickupMesh.enabled = false;
        SphereCollider.enabled = false;
    }
}
