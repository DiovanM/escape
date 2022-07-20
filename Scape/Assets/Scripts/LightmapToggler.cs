using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightmapToggler : MonoBehaviour
{
    private new Renderer renderer;
    private int originalIndex = -1;

    private void OnEnable()
    {
        renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalIndex = renderer.lightmapIndex;
        }

    }

    public void Toggle(bool value)
    {
        if (renderer == null) 
            return;

        if (renderer != null)
        {
            if (value)
            {
                renderer.lightmapIndex = originalIndex;
            }
            else
            {
                renderer.lightmapIndex = -1;
            }
        }
    }
}
