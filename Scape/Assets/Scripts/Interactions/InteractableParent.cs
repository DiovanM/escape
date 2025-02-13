using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InteractableParent : Interactable
{
    private List<Interactable> interactableParts = new ();

    protected new void Awake()
    {
        base.Awake();

        interactableParts = GetComponentsInChildren<Interactable>().Skip(1).ToList();

        interactableParts.ForEach((part) =>
        {
            part.onSelect.AddListener(Select);
            part.onDeselect.AddListener(Deselect);
            part.onInteract.AddListener(Interact);
        });

    }

    public override void SetEnabled(bool value)
    {
        base.SetEnabled(value);

        interactableParts.ForEach((part) => part.SetEnabled(value));
    }

}
