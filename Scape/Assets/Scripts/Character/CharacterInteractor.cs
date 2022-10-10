using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;
using Input;
using UnityEngine.Events;

public class CharacterInteractor : MonoBehaviour
{
    public UnityEvent<InteractableBase> onInteract = new (); 

    [SerializeField] private Transform raycastReference;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float maxDistance;

    private InteractableBase selectedInteractable;

    private void Start()
    {
        InputManager.character.Controls.Interaction.performed += Interact;
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

    private void Interact(CallbackContext context)
    {
        if (selectedInteractable != null && selectedInteractable.isAvailable)
        {
            onInteract?.Invoke(selectedInteractable);
            selectedInteractable?.Interact();

        }
    }

}
