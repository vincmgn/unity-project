using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissingScriptFinder : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MissingScriptFinder));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in Scene"))
        {
            FindMissingScriptsInCurrentScene();
        }

        if (GUILayout.Button("Find Missing Scripts in Prefabs"))
        {
            FindMissingScriptsInAllPrefabs();
        }

        if (GUILayout.Button("Find Missing Scripts in All Scenes"))
        {
            FindMissingScriptsInAllScenes();
        }
    }

    private void FindMissingScriptsInCurrentScene()
    {
        GameObject[] gos = GameObject.FindObjectsOfType<GameObject>(true);
        int go_count = 0, components_count = 0, missing_count = 0;
        foreach (GameObject g in gos)
        {
            go_count++;
            Component[] components = g.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                components_count++;
                if (components[i] == null)
                {
                    missing_count++;
                    string s = g.name;
                    Transform t = g.transform;
                    while (t.parent != null)
                    {
                        s = t.parent.name + "/" + s;
                        t = t.parent;
                    }
                    Debug.Log(s + " has an empty script attached in position: " + i, g);
                }
            }
        }

        Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
    }

    private void FindMissingScriptsInAllPrefabs()
    {
        string[] allPrefabs = AssetDatabase.GetAllAssetPaths();
        int go_count = 0, components_count = 0, missing_count = 0;
        foreach (string prefab in allPrefabs)
        {
            if (prefab.EndsWith(".prefab"))
            {
                GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(prefab);
                if (go != null)
                {
                    go_count++;
                    Component[] components = go.GetComponentsInChildren<Component>(true);
                    for (int i = 0; i < components.Length; i++)
                    {
                        components_count++;
                        if (components[i] == null)
                        {
                            missing_count++;
                            Debug.Log(prefab + " has an empty script attached in position: " + i, go);
                        }
                    }
                }
            }
        }
        Debug.Log(string.Format("Searched {0} Prefabs, {1} components, found {2} missing", go_count, components_count, missing_count));
    }

    private void FindMissingScriptsInAllScenes()
    {
        string[] allScenes = AssetDatabase.FindAssets("t:Scene");
        int go_count = 0, components_count = 0, missing_count = 0;

        foreach (string sceneGUID in allScenes)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(sceneGUID);

            // Skip scenes that are not in the Assets folder
            if (!scenePath.StartsWith("Assets/"))
            {
                continue;
            }

            Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

            GameObject[] gos = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject g in gos)
            {
                go_count++;
                Component[] components = g.GetComponents<Component>();
                for (int i = 0; i < components.Length; i++)
                {
                    components_count++;
                    if (components[i] == null)
                    {
                        missing_count++;
                        string s = g.name;
                        Transform t = g.transform;
                        while (t.parent != null)
                        {
                            s = t.parent.name + "/" + s;
                            t = t.parent;
                        }
                        Debug.Log(s + " has an empty script attached in position: " + i, g);
                    }
                }
            }
        }

        Debug.Log(string.Format("Searched {0} Scenes, {1} GameObjects, {2} components, found {3} missing", allScenes.Length, go_count, components_count, missing_count));
    }
}
