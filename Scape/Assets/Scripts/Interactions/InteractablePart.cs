using System;

public class InteractablePart : InteractableBase
{

    public Action onSelect;
    public Action onDeselect;
    public Action onInteract;
    public Action onPerform;

    public override void Select()
    {
        onSelect?.Invoke();
    }

    public override void Deselect()
    {
        onDeselect?.Invoke();
    }

    public override void Interact()
    {
        onInteract?.Invoke();
    }

    public override void Perform()
    {
        onPerform?.Invoke();
    }

}
