using UnityEngine;
using UnityEngine.InputSystem;
using Input;

public class CharacterInteractor : MonoBehaviour
{

    [SerializeField] private Transform raycastReference;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float maxDistance;

    private InteractableBase selectedInteractable;

    private void Start()
    {
        InputManager.player.Controls.Interaction.performed += Interact;
    }

    private void FixedUpdate()
    {
        var ray = new Ray(raycastReference.position, raycastReference.forward);
        var rayColor = Color.blue;

        if(Physics.Raycast(ray, out var hitInfo, maxDistance, layer.value))
        {
            var interactable = hitInfo.collider.GetComponent<InteractableBase>();
            if (interactable == null)
            {
                return;
            }
            else if (interactable.isAvailable && interactable != selectedInteractable)
            {
                selectedInteractable?.Deselect();
                selectedInteractable = interactable;
                interactable.Select();
            }
            rayColor = Color.red;
        }
        else
        {
            selectedInteractable?.Deselect();
            selectedInteractable = null;
        }

        Debug.DrawRay(raycastReference.position, raycastReference.forward * maxDistance, rayColor);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(selectedInteractable != null && selectedInteractable.isAvailable)
            selectedInteractable?.Interact();
    }

}