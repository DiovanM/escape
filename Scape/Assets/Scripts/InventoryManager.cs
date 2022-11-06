using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Input;
using static UnityEngine.InputSystem.InputAction;

public class InventoryManager : Singleton<InventoryManager>
{
    public static Event<CollectableItem> onAddItem = new();
    public static Event<CollectableItem> onRemoveItem = new();
    public static Event<CollectableItem> onSelectItem = new();
    
    public static List<CollectableItem> playerItems = new();

    private static int _selectedItemId;
    private static CollectableItem _selectedItem;

    private void Start()
    {
        InputManager.inventory.InGame.PrevItem.performed += OnPreviousItemPerformed;
        InputManager.inventory.InGame.NextItem.performed += OnNextItemPerformed;
        InputManager.inventory.InGame.Item1.performed += (ctx) => OnIndexItemPerformed(0);
        InputManager.inventory.InGame.Item2.performed += (ctx) => OnIndexItemPerformed(1);
        InputManager.inventory.InGame.Item3.performed += (ctx) => OnIndexItemPerformed(2);
        InputManager.inventory.InGame.Item4.performed += (ctx) => OnIndexItemPerformed(3);
        InputManager.inventory.InGame.Item5.performed += (ctx) => OnIndexItemPerformed(4);
        InputManager.inventory.InGame.Item6.performed += (ctx) => OnIndexItemPerformed(5);
        InputManager.inventory.InGame.Item7.performed += (ctx) => OnIndexItemPerformed(6);
        InputManager.inventory.InGame.Item8.performed += (ctx) => OnIndexItemPerformed(7);
        InputManager.inventory.InGame.Item9.performed += (ctx) => OnIndexItemPerformed(8);
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
            SelectItem(_selectedItemId, true);
        }
        else
        {
            _selectedItemId = 0;
            _selectedItem = null;
        }
    }

    public static void SelectItem(int index, bool overflow = false)
    {
        if (playerItems.Count == 0)
        {
            Debug.LogError("O player não possui itens");
            return;
        }

        if(index > -1 && index < playerItems.Count)
        {
            SelectItem(playerItems[index]);
        }
        else if(index == -1 && overflow)
        {
            SelectItem(playerItems.Count - 1);
        }
        else if(index == playerItems.Count && overflow)
        {
            SelectItem(playerItems[0]);
        }
        else
        {
            Debug.LogError("Index de item inválido");
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
            if (value.y < 0)
            {
                SelectItem(_selectedItemId + 1, true);
            }
        }
        else
        {
            SelectItem(_selectedItemId + 1, true);
        }
    }

    private void OnPreviousItemPerformed(CallbackContext context)
    {
        if (playerItems.Count == 0)
            return;

        if (context.valueType == typeof(Vector2))
        {
            var value = context.ReadValue<Vector2>();
            if (value.y > 0)
            {
                SelectItem(_selectedItemId - 1, true);
            }
        }
        else
        {
            SelectItem(_selectedItemId - 1, true);
        }
    }

    private void OnIndexItemPerformed(int itemIndex)
    {
        SelectItem(itemIndex);
    }

}
