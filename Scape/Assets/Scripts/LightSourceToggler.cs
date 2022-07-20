using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightSourceToggler : MonoBehaviour
{

    public bool startOn;
    public Renderer emitterRenderer;
    public new Light light;
    public List<LightmapToggler> lightmaps = new ();

    private bool isEnabled;

    private const string emission_keyword = "_EMISSION";

    private void OnValidate()
    {
        Awake();
    }

    private void Awake()
    {
        Toggle(startOn);
    }

    public void Toggle()
    {
        Toggle(!isEnabled);
    }

    public void Toggle(bool value)
    {
        isEnabled = value;

        if(emitterRenderer != null)
        {
            if (isEnabled)
            {
                emitterRenderer.sharedMaterial.EnableKeyword(emission_keyword);
            }
            else
            {
                emitterRenderer.sharedMaterial.DisableKeyword(emission_keyword);
            }
        }

        if(light != null)
            light.enabled = isEnabled;

        if(lightmaps.Count > 0)
            lightmaps.ForEach((lm) =>
            {
                if (lm != null)
                    lm.Toggle(isEnabled);
            });

    }    

}
