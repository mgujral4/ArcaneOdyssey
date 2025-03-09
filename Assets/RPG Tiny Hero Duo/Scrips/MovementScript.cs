using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterMove2 : MonoBehaviour
{
    PlayerInput playerInput;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    CharacterController characterController;
    Animator animator;
    private float rotationFactorPerFrame = 10f;
    private bool isRunPressed;
    Vector3 currentRunMovement;

    private int isWalkingHash;
    
    private int isRunningHash;

    public float runMultiplier = 3.0f;
    
    
    

    void Awake()
    {
        
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); 

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
        Debug.Log(isRunPressed);
    }


    void handleRotation()
    {
        Vector3 postionToLookAt;
        postionToLookAt.x = currentMovement.x;
        postionToLookAt.y = 0.0f;
        postionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(postionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }


    }

    void handleAnimation()
    {
        if (animator == null) return; // Prevent errors

        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }


        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void hanldeGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -0.5f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity * Time.deltaTime;
            currentRunMovement.y += gravity * Time.deltaTime;
        }
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>(); // Read input values correctly
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.magnitude > 0;
    }

    void Update()
    {
        
        bool isAttacking = false;
        bool isDancing = false;
        bool isVaulting = false;
        bool isJumping1 = false;
        hanldeGravity();    
        
        
        isAttacking = Input.GetKey(KeyCode.LeftControl);
        isDancing = Input.GetKey(KeyCode.X);
        isVaulting = Input.GetKey(KeyCode.Z);
        isJumping1 = Input.GetKey(KeyCode.C);
        
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isDancing", isDancing);
        animator.SetBool("isVaulting", isVaulting);
        animator.SetBool("isJumping1", isJumping1);

        
        Vector3 moveDirection;
        handleAnimation();
        handleRotation();
        if (isRunPressed)
        {
            Vector3 runDirection = new Vector3(currentRunMovement.x, currentRunMovement.y, currentRunMovement.z);
            characterController.Move(runDirection * Time.deltaTime);
        }
        else
        {   

            moveDirection = new Vector3(currentMovement.x, currentMovement.y, currentMovement.z);
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
