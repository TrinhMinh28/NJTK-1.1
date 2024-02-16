using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemScriptTable : ScriptableObject
{
    [SerializeField] public List<SavedItem> _Items;
    [SerializeField] public List<int> _ItemNumbers;

}
[Serializable]
public class SavedItem
{
    public Item Item;
}
