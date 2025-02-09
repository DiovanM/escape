using DG.Tweening;
using UnityEngine;

public class CameraReference : MonoBehaviour
{
    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Vector3 crouchingPosition;
    [SerializeField] private float transitionDuration;

    public void SetPosition(bool crouching)
    {
        var targetPosition = crouching ? crouchingPosition : defaultPosition;

        DOTween.To(() => transform.localPosition, x => transform.localPosition = x, targetPosition, transitionDuration);
    }
    
}
