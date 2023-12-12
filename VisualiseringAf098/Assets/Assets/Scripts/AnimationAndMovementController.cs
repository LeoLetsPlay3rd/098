using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInput playerInputs;
    CharacterController characterController;
    Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float movementSpeed = 3.0f;
    float rotationVelocity;
    float rotationSmoothTime = 0.1f;

    void Awake()
    {
        playerInputs = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInputs.CharacterControls.Move.started += onMovementInput;
        playerInputs.CharacterControls.Move.canceled += onMovementInput;
        playerInputs.CharacterControls.Move.performed += onMovementInput;
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }


    void handleRotation()
    {
        Vector3 moveDirection = new Vector3(currentMovement.x, 0.0f, currentMovement.z).normalized;

        if (moveDirection != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle + Camera.main.transform.eulerAngles.y, ref rotationVelocity, rotationSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    void Update()
    {
        handleRotation();

        // Convert input to world space based on camera orientation
        Vector3 worldSpaceInput = Camera.main.transform.TransformDirection(new Vector3(currentMovement.x, 0.0f, currentMovement.z));

        // Set the y component of worldSpaceInput to 0 for strafing
        worldSpaceInput.y = 0.0f;

        characterController.Move(worldSpaceInput * movementSpeed * Time.deltaTime);

        handleAnimation();
    }


    void handleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }

        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
    }

    void OnEnable()
    {
        playerInputs.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInputs.CharacterControls.Disable();
    }
}