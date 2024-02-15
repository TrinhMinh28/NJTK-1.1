#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
[CustomEditor(typeof(Item))]

public class CreateItemEditor : Editor
{
    List<string> fieldNames = new List<string>
    {
    "UsableItemsItem"
    //Thêm các tên trường khác ở đây
    };
    private SerializedProperty ItemIndex;
    private SerializedProperty ItemName;
    private SerializedProperty ItemDes;
    private SerializedProperty ItemSprite;
    private SerializedProperty ItemPrice;
    private SerializedProperty ItemBuy;
    private SerializedProperty TypeQuantityItem;
    private SerializedProperty UseItem;
    private SerializedProperty _TypeUsableItem;
    private void OnEnable()
    {
        ItemIndex = serializedObject.FindProperty("ItemIndex");

        ItemName = serializedObject.FindProperty("ItemName");
        ItemDes = serializedObject.FindProperty("ItemDes");
        ItemSprite = serializedObject.FindProperty("ItemSprite");
        ItemPrice = serializedObject.FindProperty("ItemPrice");
        ItemBuy = serializedObject.FindProperty("ItemBuy");
        TypeQuantityItem = serializedObject.FindProperty("TypeQuantityItem");
        UseItem = serializedObject.FindProperty("UseItem");
        //
        _TypeUsableItem = serializedObject.FindProperty("_TypeUsableItem");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        // Hiển thị các trường khác trong CreateMissionSmall
        EditorGUILayout.PropertyField(ItemIndex, true);
        EditorGUILayout.PropertyField(ItemName, true);
        EditorGUILayout.PropertyField(ItemDes, true);
        EditorGUILayout.PropertyField(ItemSprite, true);
        EditorGUILayout.PropertyField(ItemPrice, true); 
        EditorGUILayout.PropertyField(ItemBuy, true);
        EditorGUILayout.PropertyField(TypeQuantityItem, true);

        EditorGUILayout.PropertyField(UseItem, true);

        // Hiển thị các trường khác trong CreateMissionSmall
        Item Item = target as Item;
        // EditorGUILayout.PropertyField(missionProperties, true);
        if (Item.UseItem == UseItemType.HaveUse)
        {

            EditorGUILayout.PropertyField(_TypeUsableItem, true);

        }
        else
        {
            EditorGUILayout.PropertyField(_TypeUsableItem, false);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
