using UnityEngine;
using UnityEngine.Events;
using QuickOutline;

public class Interactable : InteractableBase
{

    [SerializeField] private Outline outline;

    protected void Awake()
    {
        outline ??= GetComponent<Outline>();
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
        onPerform?.Invoke();
    }

}
