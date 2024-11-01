using KBCore.Refs;
using Platformer;
using UnityEngine;


public class PlayerController : ValidatedMonoBehaviour
{
    [Header("Refrences")]
    [SerializeField, Self] CharacterController controller;
    [SerializeField, Self] Animator animator;
    [SerializeField, Anywhere] Camera cam;
    [SerializeField, Anywhere] InputReader input;
    Transform mainCam;

    const float Zerof = 0f;

    [SerializeField] float smoothTime = 0.2f;
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotationspeed = 15f;

    float currentSpeed;
    float velocity;

    private void Awake()
    {
        mainCam = Camera.main.transform;

    }

    private void Start()
    {
        input.EnablePlayerActions();
    }

    void HandleMovement() 
    {
        var movementDirection = new Vector3(input.Direction.x, y: 0f, z: input.Direction.y).normalized;
        var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movementDirection;
        if (adjustedDirection.magnitude > 0f)
        {
            // adjust rotation to match movement direction? This most likely won't be in the final build
            //var targetRotation:Quaternion = Quaternion.LookRotation(adjustedDirection);
            //transform.rotation = Quaternion.RotateTowards(from: transform.rotation, to: targetRotation, maxDegreesDelta: rotationspeed * Time.deltaTime);
            //transform.LookAt(worldPosition: transform.position + adjustedDirection);

            //Player Movement, the point of the script really
            // it is 1 AM and I hate myself


            HandleCharacterController(adjustedDirection);

            SmoothSpeed(adjustedDirection.magnitude);

        } else
        {
            SmoothSpeed(0f);
        }
        void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(current: currentSpeed, target: value, ref velocity, smoothTime);
        }

        void HandleCharacterController(Vector3 adjustedDirection)
        {
            var adjustedMovement = adjustedDirection * (moveSpeed * Time.deltaTime);
            controller.Move(adjustedMovement);
        }
        
    }
    private void Update()
    {
        HandleMovement();
    }

}
