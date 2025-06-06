using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Using TMPro for testing purposes only. Remove before release.
using Platformer;

public class Player_Movement : MonoBehaviour
{
    [Header("Lol doesn't matter")]
    public int PlayerScore;
    public ParentPlatform PP_Script;

    [Header("Audio")]
    public AudioSource JumpSource;

    [Header("Movement")]
    private Transform _playerBody;
    private Transform _playerOrientation;
    private float _lastDesiredMS;
    private float _desiredMS;
    public float MoveSpeed;
    public float DashSpeed;
    public float GroundDrag;
    public bool Dashing;
    public bool CanMove;
    // To be used by other scripts
    public bool StopMovement;

    [Header("Jumping")]
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

    public TMP_Text GroundCheckText; // Remove before release
    public TMP_Text ScoreUpdate; // Score Counter

    public bool PauseScript;
    public bool CheckScore;

    public MovementState state;
    public enum MovementState
    {
        paused,
        walking,
        air,
        dashing,
        onPlatform
    }
    void Start()
    {
        // Get ParentPlatform script reference
        PP_Script = FindAnyObjectByType<ParentPlatform>();

        //ScoreUpdate = GameObject.Find("Score_Counter").GetComponent<TextMeshPro>();
        Debug.Log("Score Counter Found!");

        _playerBody = GameObject.Find("PlayerBody").GetComponent<Transform>();
        _playerOrientation = GameObject.Find("PlayerOrientation").GetComponent<Transform>();

        //GroundCheckText.text = "Start! <3";
        //PlayerScore = 0;
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true; // Otherwise player falls over
        _readyToJump = true;
        Time.timeScale = 1f;
        StopMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Score Update
        ScoreUpdate.text = PlayerScore.ToString();
        // Ground Check
        _grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.175f, IsGround);

        _speedControl();
        StateHandler();

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

    private void LateUpdate()
    {
        _checkPaused();
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
        if (!CanMove)
        {
            return;
        }
        // calculate movement direction
        _moveDirection = Orientation.forward * _vertiInput + Orientation.right * _horiInput;

        // on slope
        if (_onSlope() && !_exitSlope)
        {
            _rb.AddForce(_getSlopeMoveDirection() * _desiredMS * 20f, ForceMode.Force);

            if (_rb.velocity.y > 0)
            {
                _rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        if (_grounded)
        {
            _rb.AddForce(_moveDirection.normalized * _desiredMS * 10f, ForceMode.Force);
        }
        // in air
        else if (!_grounded)
        {
            _rb.AddForce(_moveDirection.normalized * _desiredMS * 10f * AirMultiplier, ForceMode.Force);
        }

        
        //_rb.useGravity = !_onSlope(); Old, keeping for better recognition
    }

    private void _speedControl()
    {
        // limit speed on slope
        if (_onSlope() && !_exitSlope)
        {
            if (_rb.velocity.magnitude > _desiredMS)
            {
                _rb.velocity = _rb.velocity.normalized * _desiredMS;
            }
        }
        else
        {
            Vector3 _flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            // limit velocity if needed
            if (_flatVel.magnitude > _desiredMS)
            {
                Vector3 _limitedVel = _flatVel.normalized * _desiredMS;
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

    private void _checkPaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScript = GameObject.Find("User_Interface").GetComponent<Pause_Menu>().GetIsPaused();
            if (PauseScript)
            {
                state = MovementState.paused;
                return;
            }
        }
    }

    private void StateHandler()
    {
        if (PauseScript)
        {
            state = MovementState.paused;
            GroundCheckText.text = "Paused!";
            return;
        }
        
        if (StopMovement)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        // Dashing
        if (Dashing)
        {
            state = MovementState.dashing;
            CanMove = false;
            //_rb.useGravity = false;
            //_rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _desiredMS = DashSpeed;
            _rb.drag = 0f;
            GroundCheckText.text = "Dashing"; // Remove before release
            //Debug.Log("STATE MACHINE WORKING: DASHING");
        }

        // On Moving Platform State
        if (PP_Script.onPlatform)
        {
            state = MovementState.onPlatform;
            _myInput();
            _desiredMS = MoveSpeed;
            _rb.drag = GroundDrag;
            GroundCheckText.text = "On Platform"; // Remvove before release
        }

        // Ground or Air State
        if (_grounded)
        {
            CanMove = true;
            state = MovementState.walking;
            if(!Dashing)
            {
                _myInput();
                _desiredMS = MoveSpeed;
                _rb.drag = GroundDrag;
            }

            // turn off gravity on slope (so we don't slide)
            if (_onSlope())
            {
                _rb.drag = GroundDrag * 2;
                _rb.useGravity = false;
            }
            else
            {
                _rb.drag = GroundDrag;
                _rb.useGravity = true;
            }
            CoyoteTimeCounter = CoyoteTime;
            GroundCheckText.text = "On Ground"; // Remove before release
            //Debug.Log("STATE MACHINE WORKING: GROUNDED");
        }
        else if (!_grounded && !Dashing)
        {
            CanMove = true;
            state = MovementState.air;
            _rb.useGravity = true;
            _myInput();
            _desiredMS = MoveSpeed;
            _rb.drag = 0f;
            CoyoteTimeCounter -= Time.deltaTime;
            GroundCheckText.text = "In Air"; // Remove before release
            //Debug.Log("STATE MACHINE WORKING: IN AIR");
        }
    }
}
