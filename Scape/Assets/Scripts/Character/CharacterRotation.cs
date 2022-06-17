using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input;

public class CharacterRotation : MonoBehaviour
{

    [SerializeField] private new Transform camera;
    [SerializeField] private float sensibility;
    [SerializeField] private float yMin = -90;
    [SerializeField] private float yMax = 90;

    private Vector3 _eulerAngles;
    private float _currentVerticalVelocity;
    private float _currentHorizontalVelocity;

    private void Update()
    {
        var input = InputManager.player.Controls.Look.ReadValue<Vector2>() * sensibility;

        _eulerAngles.x = Mathf.SmoothDampAngle(_eulerAngles.x, _eulerAngles.x - input.y, ref _currentVerticalVelocity, Time.deltaTime);
        _eulerAngles.x = Mathf.Clamp(_eulerAngles.x, yMin, yMax);
        camera.localEulerAngles = _eulerAngles;

        var currentBodyRotation = transform.eulerAngles.y;

        var targetBodyRotation = Mathf.SmoothDampAngle(currentBodyRotation, currentBodyRotation + input.x, ref _currentHorizontalVelocity, Time.deltaTime);

        transform.Rotate(0, targetBodyRotation - currentBodyRotation, 0, Space.World);
    }

}
