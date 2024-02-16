using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MissionManager))]
public class MissionMangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (target is MissionManager)
        {
            var MissionManagerScript = (MissionManager)target;

            if (GUILayout.Button("Save Mission"))
            {
                MissionManagerScript.SaveMission();
            }
            if (GUILayout.Button("Load Mission"))
            {
                MissionManagerScript.LoadMission();
            }

        }
        // Các nút khác cho MissionManager có thể được thêm tại đây
        // Ví dụ: GUILayout.Button("Load Items"), GUILayout.Button("Clear Items"), vv.
    }
}
