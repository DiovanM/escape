using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItemUser : MonoBehaviour
{

    private CollectableItem _selectedItem;

    private void Awake()
    {
        InventoryManager.onSelectItem.Insert((item) => _selectedItem = item, this);
    }

    public void OnInteract(InteractableBase interactable)
    {
        if (_selectedItem != null)
        {
            if (interactable.TryGetComponent(out KeyItemReceptacle receptacle))
            {
                if(receptacle.TryUseItem(_selectedItem))
                {
                    InventoryManager.RemoveItem(_selectedItem);
                }
            }
        }
    }

}
