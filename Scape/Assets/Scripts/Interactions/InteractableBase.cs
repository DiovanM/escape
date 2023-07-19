using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableBase : MonoBehaviour
{

    public abstract bool Available 
    {
        get;
    }

    public abstract void Select();
    public abstract void Deselect();
    public abstract void Interact();

}