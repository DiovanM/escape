using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollector : MonoBehaviour
{

    public void OnInteract(InteractableBase interactable)
    {
        if(interactable.TryGetComponent(out CollectableItem collectable))
        {
            collectable.Collect();
            InventoryManager.AddItem(collectable);

        }
    }

}
