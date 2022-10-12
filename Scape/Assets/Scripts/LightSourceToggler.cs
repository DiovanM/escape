using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class LightSourceToggler : MonoBehaviour
{

    public bool startOn;
    public Renderer emitterRenderer;
    [ColorUsage(true, true)] 
    public Color emissionColor;
    public new Light light;
    public List<LightmapToggler> lightmaps = new();

    private bool isEnabled;
    private MaterialPropertyBlock _mpb;

    private void OnValidate()
    {
        Awake();
    }

    private void Awake()
    {
        if (_mpb == null)
            _mpb = new MaterialPropertyBlock();

        Toggle(startOn);
    }

    public void Toggle()
    {
        Toggle(!isEnabled);
    }

    public void Toggle(bool value)
    {
        isEnabled = value;

        if (emitterRenderer != null)
        {
            if (isEnabled)
            {
                _mpb.SetColor("_EmissionColor", emissionColor);
            }
            else
            {
                _mpb.SetColor("_EmissionColor", Color.black);
            }
            emitterRenderer.SetPropertyBlock(_mpb);
        }

        if (light != null)
            light.enabled = isEnabled;

        if (lightmaps.Count > 0)
        {
            lightmaps.ForEach((lm) =>
            {
                if (lm != null)
                    lm.Toggle(isEnabled);
            });
        }
    }

}
