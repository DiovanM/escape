using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;
    public ItemSO itemSO;

    private void Awake()
    {
        if (renderer == null) GetComponent<Renderer>();
    }

    public void Collect()
    {
        renderer.shadowCastingMode = ShadowCastingMode.Off;
        renderer.receiveShadows = false;
        Debug.Log("Collected " + itemSO.itemKey, gameObject);
    }

    public void Use()
    {
        renderer.shadowCastingMode = ShadowCastingMode.On;
        renderer.receiveShadows = true;
        Debug.Log("Item Used " + itemSO.itemKey);
    }

}
