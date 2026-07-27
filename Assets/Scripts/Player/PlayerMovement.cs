using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float crouchSpeed = 2.5f;

    [Header("Jump & Gravity")]
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = -9.81f;

    [Header("Crouch")]
    [SerializeField] float standingHeight = 2f;
    [SerializeField] float crouchingHeight = 1f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
{
    if (PlayerInteraction.IsUsingMonitor)
        return;

    HandleMovement();
    HandleJump();
    HandleCrouch();
    ApplyGravity();
}

    private Vector3 moveDirection;

    private void HandleMovement()
{
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    moveDirection = (transform.right * horizontal + transform.forward * vertical).normalized;

    float currentSpeed = walkSpeed;

    if (Input.GetKey(KeyCode.LeftShift))
    {
        currentSpeed = sprintSpeed;
    }

    if (Input.GetKey(KeyCode.LeftControl))
    {
        currentSpeed = crouchSpeed;
    }

    moveDirection *= currentSpeed;
}

    private void HandleJump()
{
    isGrounded = controller.isGrounded;

    if (controller.isGrounded && velocity.y < 0)
    {
    velocity.y = -2f;
    }

    if (Input.GetButtonDown("Jump") && isGrounded)
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}

    private void ApplyGravity()
    {
    velocity.y += gravity * Time.deltaTime;

    Vector3 finalMove = moveDirection;
    finalMove.y = velocity.y;

    controller.Move(finalMove * Time.deltaTime);
    }

    private void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = crouchingHeight;
        }
        else
        {
            controller.height = standingHeight;
        }
    }
}
