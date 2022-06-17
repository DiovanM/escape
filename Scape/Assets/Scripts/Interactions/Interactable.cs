using UnityEngine;
using UnityEngine.Events;

public class Interactable : InteractableBase
{

    [SerializeField] private Outline outline;

    public UnityEvent onInteract;
    public UnityEvent onPerform;

    protected void Awake()
    {
        outline.enabled = false;
    }

    public override void Select()
    {
        outline.enabled = true;
    }

    public override void Deselect()
    {
        outline.enabled = false;
    }

    public override void Interact()
    {
        Debug.Log("INTERACTION");
        onInteract?.Invoke();
        Perform();
    }

    public override void SetEnabled(bool value)
    {
        base.SetEnabled(value);

        if (!isEnabled)
            Deselect();
    }

    public override void Perform()
    {
        Debug.Log("PERFORMED");
        onPerform?.Invoke();
    }

}
