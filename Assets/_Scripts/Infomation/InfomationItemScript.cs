using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New InfomationItems ", menuName = "2D / Items /New InfomationItems ", order = 0)]
public class InfomationItemScript : ScriptableObject
{
    [SerializeField] public SavedItemInfo _Items;

}
[Serializable]
public class SavedItemInfo
{

    [SerializeField] public Item CanChien;
    [SerializeField] public Item XaChien;
    [SerializeField] public Item Ao;
    [SerializeField] public Item Quan;
    [SerializeField] public Item Gang;
    [SerializeField] public Item DayChuyen;
    [SerializeField] public Item Nhan;
    [SerializeField] public Item Bua;

}
