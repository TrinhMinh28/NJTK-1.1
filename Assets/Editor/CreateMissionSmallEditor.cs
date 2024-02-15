#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(CreateMissionSmall))]
public class CreateMissionSmallEditor : Editor
{
    List<string> fieldNames = new List<string> 
    {
    "_MissionTalkForGiverNpc",
    "_MissionColectionItem",
    "_MissionUseItem",
    "_MissionAttackEnemy",
    //Thêm các tên trường khác ở đây
    };
    private SerializedProperty typeSmallMissProperty;
    //
    //private SerializedProperty missionTalkForGiverNpcProperty;
    //private SerializedProperty missionColectionItemProperty;
    //private SerializedProperty missionUseItemProperty;
    //private SerializedProperty missionAttackEnemyProperty;

    // Khai báo các SerializedProperty cho các trường khác trong CreateMissionSmall
    private SerializedProperty missionSmallIndexProperty;
    private SerializedProperty missionSmallNameProperty;
    private SerializedProperty missionSmallDesProperty;
    private SerializedProperty missionSmallItemGiftProperty;
    private SerializedProperty missionSmallItemsRewardProperty;
    private SerializedProperty missionSmallSpriteProperty;
    private SerializedProperty trangThaiMissionProperty;

    private SerializedProperty missionPropertiesEditor;
    private void OnEnable()
    {
        typeSmallMissProperty = serializedObject.FindProperty("TypeSmallMiss");
        #region
        // Type
        //missionTalkForGiverNpcProperty = serializedObject.FindProperty("MissionTalkForGiverNpc");
        //missionColectionItemProperty = serializedObject.FindProperty("_MissionColectionItem");
        //missionUseItemProperty = serializedObject.FindProperty("_MissionUseItem");
        //missionAttackEnemyProperty = serializedObject.FindProperty("_MissionAttackEnemy");

        // Gán các SerializedProperty cho các trường khác trong CreateMissionSmall
        #endregion

        missionSmallIndexProperty = serializedObject.FindProperty("MissionSmallIndex");
        missionSmallNameProperty = serializedObject.FindProperty("MissionSmallName");
        missionSmallDesProperty = serializedObject.FindProperty("MissionSmaillDes");
        missionSmallItemGiftProperty = serializedObject.FindProperty("_MissionSmallItemGift");
        missionSmallItemsRewardProperty = serializedObject.FindProperty("_MissionSmallItemsReward");
        missionSmallSpriteProperty = serializedObject.FindProperty("MissionSmallSprite");
        trangThaiMissionProperty = serializedObject.FindProperty("TrangThaiMission");
        //
        missionPropertiesEditor = serializedObject.FindProperty("missionProperties");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        // Hiển thị các trường khác trong CreateMissionSmall
        EditorGUILayout.PropertyField(missionSmallIndexProperty, true);
        EditorGUILayout.PropertyField(missionSmallNameProperty, true);
        EditorGUILayout.PropertyField(missionSmallDesProperty, true);
        EditorGUILayout.PropertyField(missionSmallItemGiftProperty, true);
        EditorGUILayout.PropertyField(missionSmallItemsRewardProperty, true);
        EditorGUILayout.PropertyField(missionSmallSpriteProperty, true);
        EditorGUILayout.PropertyField(trangThaiMissionProperty, true);

        EditorGUILayout.PropertyField(typeSmallMissProperty, true);
        // Hiển thị các trường khác trong CreateMissionSmall
        CreateMissionSmall missionSmall = target as CreateMissionSmall;
        // EditorGUILayout.PropertyField(missionProperties, true);
        if (missionSmall.TypeSmallMiss == TypeMission.TakingNpc)
        {
            #region
            // EditorGUI.BeginChangeCheck();
            ////EditorGUILayout.PropertyField(missionTalkForGiverNpcProperty, true);
            ////EditorGUILayout.PropertyField(missionColectionItemProperty, true);
            ////EditorGUILayout.PropertyField(missionUseItemProperty, true);
            ////EditorGUILayout.PropertyField(missionAttackEnemyProperty, true);
            //if (EditorGUI.EndChangeCheck())
            //{
            //    serializedObject.ApplyModifiedProperties();
            //}


            //EditorGUI.indentLevel++;
            //EditorGUILayout.PropertyField(missionPropertiesEditor.FindPropertyRelative("_MissionTalkForGiverNpc"), false);
            //EditorGUI.indentLevel--;
            //EditorGUI.indentLevel++;
            //EditorGUILayout.PropertyField(missionPropertiesEditor.FindPropertyRelative("_MissionColectionItem"), false);
            //EditorGUI.indentLevel--;
            //EditorGUI.indentLevel++;
            //EditorGUILayout.PropertyField(missionPropertiesEditor.FindPropertyRelative("_MissionUseItem"), true);
            //EditorGUI.indentLevel--;
            //EditorGUI.indentLevel++;
            //EditorGUILayout.PropertyField(missionPropertiesEditor.FindPropertyRelative("_MissionAttackEnemy"), false);
            //EditorGUI.indentLevel--;
            #endregion


            SetMissionProperties(fieldNames, "_MissionTalkForGiverNpc", true);

        }
        else if (missionSmall.TypeSmallMiss == TypeMission.UseItem)
        {
            SetMissionProperties(fieldNames, "_MissionUseItem", true);
        }
        else if (missionSmall.TypeSmallMiss == TypeMission.CollectItems)
        {
            SetMissionProperties(fieldNames, "_MissionColectionItem", true);
        }
        else
        {
            EditorGUILayout.HelpBox("Thành phần này chưa được khởi tạo trong script Editor  CreateMissionSmallEditor", MessageType.Info);
        }
        serializedObject.ApplyModifiedProperties();
    }
    private void SetMissionProperties(List<string> fieldNames, string _Taget, bool value)
    {
        EditorGUI.indentLevel++;

        foreach (string fieldName in fieldNames)
        {
            if (fieldName == _Taget)
            {
                EditorGUILayout.PropertyField(missionPropertiesEditor.FindPropertyRelative(fieldName), value);
                //SerializedProperty fieldProperty = missionProperties.FindPropertyRelative(fieldName);
                //EditorGUILayout.PropertyField(fieldProperty, value);
            }
            else
            {
                EditorGUILayout.PropertyField(missionPropertiesEditor.FindPropertyRelative(fieldName), !value);
                //SerializedProperty fieldProperty = missionProperties.FindPropertyRelative(fieldName);
                //EditorGUILayout.PropertyField(fieldProperty, !value);
            }
        }

        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }
}