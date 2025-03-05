// Created by Bobby Lapadula
// Last Change 9/26/24 XX:XX

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject PlayerCamera;

    private Vector3 _movement;
    public float MoveSpeed = 10f;
    public float VelocityMax = 5f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement: Get input and normalize it (force magnitude of 1)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _movement = new Vector3(horizontal, 0f, vertical);
        _movement.Normalize();
    }

    private void FixedUpdate()
    {
        // Movement: Add force and limit velocity
        _rb.AddForce(_movement * MoveSpeed);
        _rb.velocity = new Vector3 (Mathf.Clamp(_rb.velocity.x, -VelocityMax, VelocityMax),
                                   _rb.velocity.y,
                                   Mathf.Clamp(_rb.velocity.z, -VelocityMax, VelocityMax));
        Debug.Log("This is player velocity: " + _rb.velocity);
    }
}
