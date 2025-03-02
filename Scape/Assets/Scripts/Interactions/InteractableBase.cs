using System;
using Localization;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableBase : SerializedMonoBehaviour
{

    [NonSerialized, OdinSerialize]
    public LocalizedString name;

    public abstract bool Available 
    {
        get;
    }

    public abstract void Select();
    public abstract void Deselect();
    public abstract void Interact();

}