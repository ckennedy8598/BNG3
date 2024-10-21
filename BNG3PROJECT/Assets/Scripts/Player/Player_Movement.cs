using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Using TMPro for testing purposes only. Remove before release.

public class Player_Movement : MonoBehaviour
{
    [Header("Lol doesn't matter")]
    public int PlayerScore;

    [Header("Audio")]
    public AudioSource JumpSource;

    [Header("Movement")]
    public float MoveSpeed;
    public float GroundDrag;

    public float CoyoteTime;
    public float CoyoteTimeCounter;
    public float JumpForce;
    public float JumpCooldown;
    public float JumpBufferTime;
    public float JumpBufferCounter;
    public float AirMultiplier;
    bool _hasJumped = false;
    bool _readyToJump;
    bool _exitSlope;

    [Header("Keybinds")]
    public KeyCode JumpKey = KeyCode.Space;

    [Header("Slope Handling")]
    public float MaxSlopeAngle;
    private RaycastHit _slopeHit;

    [Header("Ground Check")]
    public LayerMask IsGround;
    public float PlayerHeight;
    bool _grounded;

    public Transform Orientation;

    float _horiInput;
    float _vertiInput;

    Vector3 _moveDirection;
    Rigidbody _rb;

    public TMP_Text GroundCheck; // Remove before release
    public TMP_Text ScoreUpdate; // Score Counter

    public bool PauseScript;
    void Start()
    {
        //PauseScript = GameObject.Find("User_Interface").GetComponent<Pause_Menu>().Paused;
        PlayerScore = 0;
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true; // Otherwise player falls over
        _readyToJump = true;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScript = GameObject.Find("User_Interface").GetComponent<Pause_Menu>().GetIsPaused();
            if(PauseScript)
            {
                return;
            }
        }
        // Score Update
        ScoreUpdate.text = "Score: " + PlayerScore.ToString();
        // Ground Check
        _grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, IsGround);

        _myInput();
        _speedControl();

        // Handle drag
        if (_grounded)
        {
            GroundCheck.text = "On Ground"; // Remove before release
            _rb.drag = GroundDrag;
            CoyoteTimeCounter = CoyoteTime;
        }
        else
        {
            GroundCheck.text = "In the Air"; // Remove before release
            _rb.drag = 0;
             CoyoteTimeCounter -= Time.deltaTime;
        }

        // Jump Buffer
        if (Input.GetKeyDown(JumpKey))
        {
            JumpBufferCounter = JumpBufferTime;
        }
        else
        {
            JumpBufferCounter -= Time.deltaTime;
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

        // when to jump - Checks Coyote Time instead of _grounded and checks JumpBufferCounter instead of Input.Key
        if ((JumpBufferCounter > 0f) && _readyToJump && (CoyoteTimeCounter > 0f))
        {
            _readyToJump = false;
            _jump();
            JumpSource.Play();
            JumpBufferCounter = 0f;

            Invoke(nameof(_resetJump), JumpCooldown);
        }

        if (Input.GetKeyUp(JumpKey))
        {
            CoyoteTimeCounter = 0f;
        }
    }

    private void _movePlayer()
    {
        // calculate movement direction
        _moveDirection = Orientation.forward * _vertiInput + Orientation.right * _horiInput;

        // on slope
        if (_onSlope() && !_exitSlope)
        {
            _rb.AddForce(_getSlopeMoveDirection() * MoveSpeed * 20f, ForceMode.Force);

            if (_rb.velocity.y > 0)
            {
                _rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        if (_grounded)
        {
            _rb.AddForce(_moveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);
        }
        // in air
        else if (!_grounded)
        {
            _rb.AddForce(_moveDirection.normalized * MoveSpeed * 10f * AirMultiplier, ForceMode.Force);
        }

        // turn off gravity on slope (so we don't slide)
        _rb.useGravity = !_onSlope();
    }

    private void _speedControl()
    {
        // limit speed on slope
        if (_onSlope() && !_exitSlope)
        {
            if (_rb.velocity.magnitude > MoveSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * MoveSpeed;
            }
        }
        else
        {
            Vector3 _flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            // limit velocity if needed
            if (_flatVel.magnitude > MoveSpeed)
            {
                Vector3 _limitedVel = _flatVel.normalized * MoveSpeed;
                _rb.velocity = new Vector3(_limitedVel.x, _rb.velocity.y, _limitedVel.z);
            }
        }
    }

    private void _jump()
    {
        _exitSlope = true;

        // reset y velocity
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        _rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    private void _resetJump()
    {
        _readyToJump = true;

        _exitSlope = false;
    }

    private bool _onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, PlayerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < MaxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 _getSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal).normalized;
    }
}
