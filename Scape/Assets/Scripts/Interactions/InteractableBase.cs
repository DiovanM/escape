using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{

    public bool isEnabled = true;
    public bool isBusy;
    public bool isAvailable => isEnabled && !isBusy;

    public abstract void Select();
    public abstract void Deselect();
    public abstract void Interact();
    public abstract void Perform();
    public virtual void SetEnabled(bool value)
    {
        isEnabled = value;
    }

}