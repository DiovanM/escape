using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyItemReceptacle : MonoBehaviour
{

    public UnityEvent<CollectableItem> onItemUsed;

    public string expectedItemKey;

    private void Awake()
    {
        if(string.IsNullOrEmpty(expectedItemKey))
        {
            Debug.LogWarning("Expected Item Key is empty", gameObject);
        }
    }

    public bool TryUseItem(CollectableItem item)
    {
        if(item.itemSO.itemKey == expectedItemKey)
        {
            item.Use();
            onItemUsed?.Invoke(item);
            return true;
        }
        Debug.Log($"Item key does not match: {item.itemSO.itemKey} : {expectedItemKey}", gameObject);
        return false;
    }

}
