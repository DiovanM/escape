using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InteractableParent : Interactable
{

    [SerializeField] private Animator animator;

    private List<InteractablePart> interactableParts = new List<InteractablePart>();

    protected new void Awake()
    {
        base.Awake();

        if (animator == null)
            animator = GetComponent<Animator>();

        interactableParts = GetComponentsInChildren<InteractablePart>().ToList();

        interactableParts.ForEach((part) =>
        {
            part.onSelect += Select;
            part.onDeselect += Deselect;
            part.onInteract += Interact;
            part.onPerform += Perform;
        });

    }

    public override void Select()
    {
        base.Select();

    }

    public override void Deselect()
    {
        base.Deselect();
    }

    public override void Interact()
    {
        if (!isAvailable)
            return;

        SetIsBusy(true);
        Debug.Log("INTERACTED", gameObject);
        animator.SetTrigger("Activate");
    }

    public override void SetEnabled(bool value)
    {
        base.SetEnabled(value);

        interactableParts.ForEach((part) => part.SetEnabled(value));
    }

    public override void Perform()
    {
        if (!isEnabled)
            return;

        SetIsBusy(false);
        base.Perform();
    }

    private void SetIsBusy(bool value)
    {
        isBusy = value;
        interactableParts.ForEach((part) => part.SetEnabled(value));
    }

}
