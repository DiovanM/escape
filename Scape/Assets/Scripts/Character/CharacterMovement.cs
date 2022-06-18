using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Input;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private CharacterController characterController;
    
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float crouchSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;

    [SerializeField] private float playerHeight = 2;
    [SerializeField] private float crouchHeight = 1;
    [SerializeField] private float crouchingTime = .3f;
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, .5f, 0);

    private Vector3 moveVelocity;
    private bool jump;
    private bool crouching;
    private bool runningCrouch;

    private void Start()
    {
        InputManager.player.Controls.Jump.performed += OnJumpPerformed;
        InputManager.player.Controls.Crouch.performed += OnCrouchPerformed;
    }

    private void Update()
    {
        var movementInput = InputManager.player.Controls.Move.ReadValue<Vector2>();

        var speed = crouching ? crouchSpeed : walkingSpeed;

        var desiredVelocity = Vector3.ClampMagnitude(((transform.forward * movementInput.y) + (transform.right * movementInput.x)) * speed, speed);

        moveVelocity.x = desiredVelocity.x;
        moveVelocity.z = desiredVelocity.z;

        if(characterController.isGrounded)
        {
            moveVelocity.y = 0f;

            if(jump)
            {
                jump = false;
                moveVelocity.y = jumpForce;
            }
        }
        else
        {
            moveVelocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        characterController.Move(moveVelocity * Time.deltaTime);
        
        Debug.DrawRay(transform.position, Vector3.up * (playerHeight * (transform.localScale.y + .2f)), Color.green);
    }

    private IEnumerator ToggleCrouch()
    {
        runningCrouch = true;

        var currentHeight = characterController.height;
        var targetHeight = crouching ? playerHeight : crouchHeight;
        var currentCenter = characterController.center;
        var targetCenter = crouching ? standingCenter : crouchingCenter;

        var elapsedTime = 0f;

        while(elapsedTime < crouchingTime)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, elapsedTime / crouchingTime);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, elapsedTime / crouchingTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        crouching = !crouching;

        runningCrouch = false;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jump = true;
    }

    private void OnCrouchPerformed(InputAction.CallbackContext context)
    {
        var canCrouch = !runningCrouch && !(crouching && Physics.Raycast(transform.position, Vector3.up, playerHeight * transform.localScale.y + .2f, ~LayerMask.GetMask("Player")));
        if(canCrouch)
        {
            StartCoroutine(ToggleCrouch());
        }
    }

}
