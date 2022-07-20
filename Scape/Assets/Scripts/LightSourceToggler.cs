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

    private MaterialPropertyBlock mpb;

    private void OnValidate()
    {
        Awake();
    }

    private void Awake()
    {
        if (mpb == null)
            mpb = new MaterialPropertyBlock();
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
                //mpb.SetColor("_EmissionColor", new Color(191,191,191));
                emitterRenderer.sharedMaterial.EnableKeyword(emission_keyword);
            }
            else
            {
                //mpb.SetColor("_EmissionColor", new Color(0,0,0));
                emitterRenderer.sharedMaterial.DisableKeyword(emission_keyword);
            }

            //emitterRenderer.SetPropertyBlock(mpb);
        }

        if(light != null)
            light.enabled = isEnabled;

        lightmaps.ForEach((lm) => lm.Toggle(isEnabled));

    }    

}
