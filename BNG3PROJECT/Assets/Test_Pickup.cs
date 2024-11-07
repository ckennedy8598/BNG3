using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Test_Pickup : MonoBehaviour
{
    private MeshRenderer PickupMesh;
    private SphereCollider SphereCollider;
    public TMP_Text Counter_Text;
    public AudioSource SCREAM;
    void Start()
    {
        SCREAM = GetComponent<AudioSource>();
        PickupMesh = GetComponent<MeshRenderer>();
        SphereCollider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        SCREAM.Play();
        GameObject.Find("Player").GetComponent<Player_Movement>().PlayerScore++;
        SetDead();
        Destroy(gameObject, 5);
    }

    public void SetDead()
    {
        PickupMesh.enabled = false;
        SphereCollider.enabled = false;
    }
}
