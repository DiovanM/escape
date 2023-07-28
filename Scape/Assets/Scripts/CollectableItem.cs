using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;
    public ItemSO itemSO;

    public void Collect()
    {
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        Debug.Log("Collected " + itemSO.itemKey, gameObject);
    }

    public void Use()
    {
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        Debug.Log("Item Used " + itemSO.itemKey);
    }

}
