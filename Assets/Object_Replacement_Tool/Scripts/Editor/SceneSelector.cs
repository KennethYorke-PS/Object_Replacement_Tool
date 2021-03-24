using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneSelector : EditorWindow
{
    [MenuItem("Pineapple Studios/Scenes/Load Sample Scene")]
    public static void LoadDesertScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");
    }

    [MenuItem("Pineapple Studios/Utility/Clear All PlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        if (EditorUtility.DisplayDialogComplex("Clear All Player Prefs", "Are you sure you want to clear all Player Prefs?", "Yes please", "Not a chance", "Cancel") == 0)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
