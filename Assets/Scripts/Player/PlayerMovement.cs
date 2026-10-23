
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float crouchSpeed = 2.5f;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Crouch")]
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchingHeight = 1f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private InputActionReference crouchAction;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool jumpRequested;

    private void OnEnable()
    {
        EnableAction(moveAction);
        EnableAction(jumpAction);
        EnableAction(sprintAction);
        EnableAction(crouchAction);
    }

    private void OnDisable()
    {
        DisableAction(moveAction);
        DisableAction(jumpAction);
        DisableAction(sprintAction);
        DisableAction(crouchAction);
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (PlayerInteraction.IsUsingMonitor)
            return;

        HandleMovement();
        HandleJump();
        HandleCrouch();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        Vector2 input = moveAction != null
            ? moveAction.action.ReadValue<Vector2>()
            : Vector2.zero;

        Vector3 moveDirection =
            transform.right * input.x + transform.forward * input.y;

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        float currentSpeed = walkSpeed;

        if (sprintAction != null && sprintAction.action.IsPressed())
            currentSpeed = sprintSpeed;

        if (crouchAction != null && crouchAction.action.IsPressed())
            currentSpeed = crouchSpeed;

        moveDirection *= currentSpeed;

        controller.Move(
            new Vector3(moveDirection.x, 0f, moveDirection.z) * Time.deltaTime
        );
    }
    
    
private float jumpBufferTime = 0.15f;
private float jumpBufferCounter;

private void HandleJump()
{
    isGrounded = controller.isGrounded;

    if (jumpAction != null && jumpAction.action.WasPressedThisFrame())
    {
        jumpBufferCounter = jumpBufferTime;
    }
    else
    {
        jumpBufferCounter -= Time.deltaTime;
    }

    if (isGrounded && velocity.y < 0f)
    {
        velocity.y = -2f;
    }

    if (jumpBufferCounter > 0f && isGrounded)
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        jumpBufferCounter = 0f;
    }
}


    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleCrouch()
    {
        if (crouchAction != null && crouchAction.action.IsPressed())
            controller.height = crouchingHeight;
        else
            controller.height = standingHeight;
    }

    private void EnableAction(InputActionReference actionReference)
    {
        if (actionReference != null)
            actionReference.action.Enable();
    }

    private void DisableAction(InputActionReference actionReference)
    {
        if (actionReference != null)
            actionReference.action.Disable();
    }
}
