using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TileMapManager))]
public class TileMapEditorManager : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (target is TileMapManager)
        {
            var tileMapScript = (TileMapManager)target;

            if (GUILayout.Button("Save Map"))
            {
                tileMapScript.SaveMap();
            }

            if (GUILayout.Button("Clear Map"))
            {
                tileMapScript.ClearMap();
            }

            if (GUILayout.Button("Load Map"))
            {
                tileMapScript.LoadMap();
            }
        }
        //else if (target is ItemManager)
        //{
           
        //}


    }
}
