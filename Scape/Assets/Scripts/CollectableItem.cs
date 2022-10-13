using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemKey;

    private void Awake()
    {
        if (string.IsNullOrEmpty(itemKey))
            itemKey = gameObject.name;
    }

    public void Collect()
    {
        gameObject.SetActive(false);
    }

    public void Use()
    {
        Debug.Log("Item Used " + itemKey);
    }

}
