using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightmapToggler))]
public class LightmapTogglerEditor : Editor
{

    public override void OnInspectorGUI()
    {

        var lt = target as LightmapToggler;

        serializedObject.Update();

        using (new GUILayout.HorizontalScope())
        {
            if(GUILayout.Button("Turn On"))
            {
                Undo.RecordObject(lt, "Turn On lightmap");
                lt.Toggle(true);
            }

            if (GUILayout.Button("Turn Off"))
            {
                Undo.RecordObject(lt, "Turn Off lightmap");
                lt.Toggle(false);
            }
        }

        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();

    }

}
