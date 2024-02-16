using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemManager))]
public class ItemManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (target is ItemManager)
        {
            var itemManagerScript = (ItemManager)target;

            if (GUILayout.Button("Save Items"))
            {
                itemManagerScript.SaveItems();
            }
            if (GUILayout.Button("Load Items"))
            {
                itemManagerScript.LoadItems();
            }

        }
        // Các nút khác cho ItemManager có thể được thêm tại đây
        // Ví dụ: GUILayout.Button("Load Items"), GUILayout.Button("Clear Items"), vv.
    }
}
