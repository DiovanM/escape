using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Input;
using static UnityEngine.InputSystem.InputAction;

public class InventoryManager : MonoBehaviour
{
    public static Event<CollectableItem> onAddItem = new();
    public static Event<CollectableItem> onRemoveItem = new();
    public static Event<CollectableItem> onSelectItem = new();
    
    public static List<CollectableItem> playerItems = new();

    private static int _selectedItemId;
    private static CollectableItem _selectedItem;

    private void Start()
    {
        InputManager.player.Controls.PrevItem.performed += OnPreviousItemPerformed;
        InputManager.player.Controls.NextItem.performed += OnNextItemPerformed;
    }

    public static void AddItem(CollectableItem item)
    {
        playerItems.Add(item);
        onAddItem.Trigger(item);
        if(_selectedItem == null)
        {
            SelectItem(0);
        }
    }

    public static void RemoveItem(CollectableItem item)
    {
        playerItems.Remove(item);
        onRemoveItem.Trigger(item);
        if (playerItems.Count > 0)
        {
            SelectItem(_selectedItemId);
        }
        else
        {
            _selectedItemId = 0;
            _selectedItem = null;
        }
    }

    public static void SelectItem(int index)
    {
        if (playerItems.Count == 0)
        {
            Debug.LogError("O player não possui itens");
            return;
        }

        if (index < -1 || index > playerItems.Count)
        {
            Debug.LogError("Index de item inválido");
            return;
        }
        else if (index == -1)
        {
            SelectItem(playerItems.Count - 1);
        }
        else if (index == playerItems.Count)
        {
            SelectItem(playerItems[0]);
        }
        else
        {
            SelectItem(playerItems[index]);
        }
    }

    public static void SelectItem(CollectableItem item)
    {
        if(item == null)
        {
            _selectedItem = null;
            _selectedItemId = 0;
            onSelectItem.Trigger(null);
        }
        else if (!playerItems.Contains(item))
            Debug.LogError("Player não possui o item que está tentando selecionar");
        else if(item != _selectedItem)
        {
            Debug.Log("Selected Item:" + item.gameObject.name);
            _selectedItemId = playerItems.IndexOf(item);
            _selectedItem = item;
            onSelectItem.Trigger(playerItems[_selectedItemId]);
        }
    }

    private void OnNextItemPerformed(CallbackContext context)
    {
        if (playerItems.Count == 0)
            return;

        if (context.valueType == typeof(Vector2))
        {
            var value = context.ReadValue<Vector2>();
            if (value.y > 0)
            {
                SelectItem(_selectedItemId + 1);
            }
        }
        else
        {
            SelectItem(_selectedItemId + 1);
        }
    }

    private void OnPreviousItemPerformed(CallbackContext context)
    {
        if (playerItems.Count == 0)
            return;

        if (context.valueType == typeof(Vector2))
        {
            var value = context.ReadValue<Vector2>();
            if (value.y < 0)
            {
                SelectItem(_selectedItemId - 1);
            }
        }
        else
        {
            SelectItem(_selectedItemId - 1);
        }
    }

}
