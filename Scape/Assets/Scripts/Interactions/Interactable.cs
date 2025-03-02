using UnityEngine;
using UnityEngine.Events;
using QuickOutline;
using System;

public class Interactable : InteractableBase
{

    public UnityEvent onInteract;
    public UnityEvent onSelect;
    public UnityEvent onDeselect;

    public bool isEnabled = true;
    public override bool Available => isEnabled;

    [SerializeField] private Outline outline;

    protected void Awake()
    {
        outline ??= GetComponent<Outline>();
        SetOutline(false);
    }

    private void SetOutline(bool value)
    {
        if (outline != null)
            outline.enabled = value;
    }

    public override void Select()
    {
        SetOutline(true);
        
        if(name != null)
            DialogManager.ShowTip(name.GetText());
        
        onSelect?.Invoke();
    }

    public override void Deselect()
    {
        SetOutline(false);
        DialogManager.HideTip();
        onDeselect?.Invoke();
    }

    public override void Interact()
    {
        if(Available)
            onInteract?.Invoke();
    }

    public virtual void SetEnabled(bool value)
    {
        isEnabled = value;

        if (!isEnabled)
            Deselect();
    }

}
