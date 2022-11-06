using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class OnEditorPlayStatus
{

    private static ProjectSettings projectSettings;

    static OnEditorPlayStatus()
    {
        Addressables.LoadAssetAsync<ProjectSettings>("ProjectSettings").Completed += (a) =>
        {
            projectSettings = a.Result;
        };

        EditorApplication.playModeStateChanged -= OnPlayModeStateChange;
        EditorApplication.playModeStateChanged += OnPlayModeStateChange;
    }

    private static void OnPlayModeStateChange(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.ExitingEditMode:
                OnPlay();
                break;
            case PlayModeStateChange.EnteredEditMode:
                OnStop();
                break;
        }
    }

    private static void OnPlay()
    {

        if (projectSettings.autoLoadSettingsScene)
        {

            var settingsAsset = projectSettings.settingsScene.editorAsset;
            var loadedScenes = new Scene[SceneManager.sceneCount];
            bool isSettingsLoaded = false;

            for (int i = 0; i < loadedScenes.Length; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
                if (loadedScenes[i].name == settingsAsset.name)
                {
                    isSettingsLoaded = true;
                    break;
                }
            }

            if (!isSettingsLoaded)
            {
                var scenePath = AssetDatabase.GetAssetOrScenePath(settingsAsset);

                var loadedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
                SceneManager.SetActiveScene(loadedScene);

            }
        }

    }

    private static void OnStop()
    {
        if (projectSettings.autoLoadSettingsScene)
        {
            var loadedSettingsScene = SceneManager.GetSceneByName(projectSettings.settingsScene.editorAsset.name);
            if (loadedSettingsScene.isLoaded)
            {
                EditorSceneManager.CloseScene(loadedSettingsScene, true);
            }
        }
    }
}
