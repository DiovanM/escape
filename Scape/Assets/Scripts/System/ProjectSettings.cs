using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEditor;

[CreateAssetMenu(fileName = "ProjectSettings", menuName = "ScriptableObjects/Project Settings")]
public class ProjectSettings : ScriptableObject
{

    public bool autoLoadSettingsScene;
    public AssetReferenceT<SceneAsset> settingsScene;


}
