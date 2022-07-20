using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightmapToggler : MonoBehaviour
{
    private const int noLightmapIndex = -1;
    private const int originalIndex = 0;

    private new Renderer renderer;
    private int currentIndex;

    private void OnValidate()
    {
        Awake();
    }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.lightmapIndex = currentIndex;
        }
    }

    public void Toggle(bool value)
    {
        if (renderer == null) 
            return;

        currentIndex = value ? originalIndex : noLightmapIndex;

        renderer.lightmapIndex = currentIndex;
    }
}
