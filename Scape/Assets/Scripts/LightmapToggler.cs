using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightmapToggler : MonoBehaviour
{
    private const int noLightmapIndex = -1;

    private new Renderer renderer;
    private int originalIndex = noLightmapIndex;
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
            originalIndex = renderer.lightmapIndex;

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
