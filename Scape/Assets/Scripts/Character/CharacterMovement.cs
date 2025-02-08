using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;
using Input;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator characterAnimator;
    
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
        InputManager.character.Controls.Jump.performed += OnJumpPerformed;
        InputManager.character.Controls.Crouch.performed += OnCrouchPerformed;
    }

    private void FixedUpdate()
    {
        var movementInput = InputManager.character.Controls.Move.ReadValue<Vector2>();

        var speed = crouching ? crouchSpeed : walkingSpeed;

        var desiredVelocity = Vector3.ClampMagnitude(((transform.forward * movementInput.y) + (transform.right * movementInput.x)) * speed, speed);

        moveVelocity.x = desiredVelocity.x;
        moveVelocity.z = desiredVelocity.z;
        
        characterAnimator.SetFloat("x", movementInput.x);
        characterAnimator.SetFloat("y", movementInput.y);

        if (characterController.isGrounded)
        {
            moveVelocity.y = Physics.gravity.y * gravityScale * Time.deltaTime;

            if (jump)
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

        if (crouching)
        {
            var position = transform.position;
            position.y += crouchHeight;
            Debug.DrawRay(position, Vector3.up * ((playerHeight +.1f) * transform.localScale.y), Color.green);
        }
    }

    private void ToggleCrouch()
    {
        runningCrouch = true;

        var currentHeight = characterController.height;
        var targetHeight = crouching ? playerHeight : crouchHeight;
        var currentCenter = characterController.center;
        var targetCenter = crouching ? standingCenter : crouchingCenter;

        var runningHeight = true;
        var runningcenter = true;

        DOTween.To(() => characterController.height, x => characterController.height = x, targetHeight, crouchingTime).onComplete += () =>
        {
            runningHeight = false;
            
            if(!runningHeight && !runningcenter && runningCrouch)
            {
                crouching = !crouching;
                runningCrouch = false;
            }
        };

        DOTween.To(() => characterController.center, y => characterController.center = y, targetCenter, crouchingTime).onComplete += () =>
        {
            runningcenter = false;

            if (!runningHeight && !runningcenter && runningCrouch)
            {

                crouching = !crouching;
                runningCrouch = false;
            }
        };

    }

    private void OnJumpPerformed(CallbackContext context)
    {
        jump = true;
    }

    private void OnCrouchPerformed(CallbackContext context)
    {
        var position = transform.position;
        position.y += crouchHeight + .1f;
        var ray = new Ray(position, Vector3.up);
        var raycast = Physics.Raycast(ray, playerHeight * transform.localScale.y, ~LayerMask.GetMask("Player"));

        var canCrouch = !runningCrouch && !(crouching && raycast) && characterController.isGrounded;

        if(canCrouch)
        {
            ToggleCrouch();
        }
    }

}
