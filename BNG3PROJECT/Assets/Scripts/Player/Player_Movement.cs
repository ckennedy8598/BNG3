using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed;

    public float GroundDrag;

    public float JumpForce;
    public float JumpCooldown;
    public float AirMultiplier;
    bool _readyToJump;

    [Header("Keybinds")]
    public KeyCode JumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public LayerMask IsGround;
    public float PlayerHeight;
    bool _grounded;

    public Transform Orientation;

    float _horiInput;
    float _vertiInput;

    Vector3 _moveDirection;
    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true; // Otherwise player falls over
        _readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Ground Check
        _grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, IsGround);

        _myInput();
        _speedControl();

        // Handle drag
        if (_grounded)
        {
            _rb.drag = GroundDrag;
        }
        else
        {
            _rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        _movePlayer();
    }

    private void _myInput()
    {
        _horiInput = Input.GetAxisRaw("Horizontal");
        _vertiInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(JumpKey) && _readyToJump && _grounded)
        {
            _readyToJump = false;

            _jump();

            Invoke(nameof(_resetJump), JumpCooldown);
        }
    }

    private void _movePlayer()
    {
        // calculate movement direction
        _moveDirection = Orientation.forward * _vertiInput + Orientation.right * _horiInput;

        if (_grounded)
        {
            _rb.AddForce(_moveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);
        }
        // in ar
        else if (!_grounded)
        {
            _rb.AddForce(_moveDirection.normalized * MoveSpeed * 10f * AirMultiplier, ForceMode.Force);
        }
    }

    private void _speedControl()
    {
        Vector3 _flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        // limit velocity if needed
        if (_flatVel.magnitude > MoveSpeed)
        {
            Vector3 _limitedVel = _flatVel.normalized * MoveSpeed;
            _rb.velocity = new Vector3(_limitedVel.x, _rb.velocity.y, _limitedVel.z);
        }
    }

    private void _jump()
    {
        // reset y velocity
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        _rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    private void _resetJump()
    {
        _readyToJump = true;
    }
}
