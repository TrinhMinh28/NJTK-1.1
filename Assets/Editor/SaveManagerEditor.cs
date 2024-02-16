using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(SaveManager))]
public class SaveManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (target is SaveManager )
        {
            var itemManagerScript = ( SaveManager )target;

            if (GUILayout.Button("Save Game"))
            {
                itemManagerScript.Save();
            }
            if (GUILayout.Button("Delete Game"))
            {
                itemManagerScript.DeleteGame();
            }

        }
    }
}
