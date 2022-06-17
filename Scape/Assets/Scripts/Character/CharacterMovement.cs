using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Input;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private CharacterController characterController;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private float gravityScale;

    private Vector3 _velocity;
    private bool _jump;

    private void Start()
    {
        InputManager.player.Controls.Jump.performed += OnJumpPerformed;
    }

    private void Update()
    {
        var movementInput = InputManager.player.Controls.Movement.ReadValue<Vector2>();

        var desiredVelocity = Vector3.ClampMagnitude(((transform.forward * movementInput.y) + (transform.right * movementInput.x)) * speed, speed);

        _velocity.x = desiredVelocity.x;
        _velocity.z = desiredVelocity.z;

        if(characterController.isGrounded)
        {
            _velocity.y = 0f;

            if(_jump)
            {
                _jump = false;
                _velocity.y = jumpForce;
            }
        }
        else
        {
            _velocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        characterController.Move(_velocity * Time.deltaTime);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _jump = true;
    }


}
