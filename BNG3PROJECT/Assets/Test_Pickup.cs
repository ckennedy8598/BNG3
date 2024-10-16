using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test_Pickup : MonoBehaviour
{
    public AudioSource SCREAM;
    void Start()
    {
        SCREAM = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        SCREAM.Play();
        Debug.Log("Sound Played~~~~~~~~~~~~~~~~~~~~~");
        gameObject.SetActive(false);
        Destroy(gameObject, 5);
    }
}
