using UnityEngine;
using UnityEditor;

// Some code has been referenced and adapted from https://unity3d.college/2017/09/07/replace-gameobjects-or-prefabs-with-another-prefab/

public class ReplaceSelectedObject : EditorWindow
{
    private bool isUsingTiled;
    [SerializeField] GameObject replacementObject = null;
    [MenuItem("Pineapple Studios/Object Replacer")]
    static void ReplaceSelectedWithObject()
    {
        GetWindow(typeof(ReplaceSelectedObject), true, "Object Replacement Tool");
    }

    private void OnGUI()
    {
        isUsingTiled = EditorGUILayout.Toggle("Using TILED", isUsingTiled);

        replacementObject = EditorGUILayout.ObjectField("", replacementObject, typeof(GameObject), true) as GameObject;

        EditorGUILayout.LabelField("Select at least one object to replace in scene, choose the prefab to replace the selection with then click Replace");

        if (GUILayout.Button("Replace Selected With Object"))
        {
            var selection = Selection.gameObjects;

            if (selection.Length == 0)
            {
                Debug.LogError("Please ensure you have selected at least one object in the scene and try again");
            }

            for (var i = selection.Length - 1; i >= 0; --i)
            {
                var selected = selection[i];
                var objectToDelete = selection[i].gameObject;
                GameObject newObject;

                if (replacementObject != null)
                {
                    newObject = (GameObject)PrefabUtility.InstantiatePrefab(replacementObject);
                }
                else
                {
                    newObject = null;
                    Debug.LogError("Please ensure you have assigned a replacement prefab object and try again.");
                    break;
                }

                Undo.RegisterCreatedObjectUndo(newObject, "Replace Objects with Prefab");
                newObject.transform.parent = selected.transform.parent;
                newObject.transform.localPosition = selected.transform.localPosition;
                newObject.transform.localRotation = selected.transform.localRotation;
                newObject.transform.localScale = selected.transform.localScale;
                newObject.transform.SetSiblingIndex(selected.transform.GetSiblingIndex());
                Undo.DestroyObjectImmediate(objectToDelete);
            }
        }

        GUI.enabled = false;
        EditorGUILayout.LabelField("Selected Object Count: " + Selection.objects.Length);
    }
}