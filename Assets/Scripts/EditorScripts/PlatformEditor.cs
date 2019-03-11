using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Platform))]
public class PlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        
        DrawDefaultInspector();

        Platform myScript = (Platform)target;

        if (GUILayout.Button("Destroy Platform"))
        {
            myScript.DestroyExistingTiles();
        }

        if (GUILayout.Button("Build Platform"))
        {
            myScript.GeneratePlatform();
        }

        //if(GUI.changed)
        //{
        //    EditorSceneManager.D;

        //}
    }
}
