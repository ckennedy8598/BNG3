using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;

    public float Speed = 12f;
    public float Gravity = -9.81f * 2;
    public float JumpHeight = 3f;

    public Transform GroundCheck;
    public float GroundDistance = .4f;
    public LayerMask GroundMask;

    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isMoving;
    private Vector3 _lastPosition = new Vector3 (0f,0f,0f);
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        _isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
        
        // Reset the default velocity. Velocity builds up on each jump so we have to remove it.
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        // Getting inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; // (right = red axis, forward = blue axis)

        _characterController.Move(move * Speed * Time.deltaTime);

        // Check if can jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }

        // Falling down
        _velocity.y += Gravity * Time.deltaTime;

        // Execute jump
        _characterController.Move(_velocity * Time.deltaTime);

        if (_lastPosition != gameObject.transform.position && _isGrounded == true)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }

        _lastPosition = gameObject.transform.position;
    }
}
