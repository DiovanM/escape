using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input;

public class CharacterRotation : MonoBehaviour
{

    [SerializeField] private Transform camera;
    [SerializeField] private float sensibility;
    [SerializeField] private float yMin = -90;
    [SerializeField] private float yMax = 90;

    private Vector3 eulerAngles;
    private float currentVerticalVelocity;
    private float currentHorizontalVelocity;

    private void Update()
    {
        var input = InputManager.player.Controls.Look.ReadValue<Vector2>() * sensibility;

        eulerAngles.x = Mathf.SmoothDampAngle(eulerAngles.x, eulerAngles.x - input.y, ref currentVerticalVelocity, Time.deltaTime);
        eulerAngles.x = Mathf.Clamp(eulerAngles.x, yMin, yMax);
        camera.localEulerAngles = eulerAngles;

        var currentBodyRotation = transform.eulerAngles.y;

        var targetBodyRotation = Mathf.SmoothDampAngle(currentBodyRotation, currentBodyRotation + input.x, ref currentHorizontalVelocity, Time.deltaTime);

        transform.Rotate(0, targetBodyRotation - currentBodyRotation, 0, Space.World);
    }

}
