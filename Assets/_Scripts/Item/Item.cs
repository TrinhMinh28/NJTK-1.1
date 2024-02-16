
using JetBrains.Annotations;
using System;
using UnityEngine;
[CreateAssetMenu (fileName = "New Item", menuName = "2D / Items /New Item", order = 2)]
[System.Serializable]
public class Item :ScriptableObject
{
    public int ItemIndex;
    public string ItemName;
    public string ItemDes;
    public Sprite ItemSprite;
    public int ItemPrice;
    public int ItemBuy;
    public ItemQuantity TypeQuantityItem; // VP không có số lượng;
    public UseItemType UseItem; // Có thể sử dụng không Items;
    public ItemType _TypeUsableItem; // Loại Items;

}

[Serializable]
public enum  ItemQuantity
{
    HaveQuantity = 1,
    NoQuantity = 0

}
[Serializable]
public enum UseItemType
{
    NoUse = 0,
    HaveUse = 1,

}
[Serializable]
public struct UsableItems
{
    public float ItemLevel;
    public float EffectiveTime; // thời gian tồn tại
    public float HealReturn; // thời gian tồn tại
    public float ManaReturn; // thời gian tồn tại
    public float LeveBonus; // thời gian tồn tại
    public float ExpBonus; // 
    public float YenBonus; // 
    public float XuBonus; // 
    public float HealBonus;
    public float ManaNonus;
    public float DameBonus;
    public float GiapBonus;
    public float XuyenGiapBonus;
    public float CritBonus;
    
}
[Serializable]
public enum Equipmenttype
{
    vukhicanchien =0,
    vukhixachien = 1,
    ao =2,
    gang =3, 
    quan = 4,
    daychuyen =5,
    nhan = 6,
    bua = 7,
    vatphamhoiphuc =8,
    tiente =9

}
[Serializable]
public enum ClassItem
{
    hoathuoctinh = 0 ,
    bangthuoctinh = 1 ,
    phongthuoctinh = 2,
}
[Serializable]
public struct SkillWeapons
{
    public CreateSkillBang SkillBang;
    public CreateSkillHoa SkillHoa;
    public CreateSkillPhong SkillPhong;
    public CreateSkillBase SkillBase;
}
[Serializable]
public struct ItemType
{
    public UsableItems UsableItemsItem; // Thuộc tính của Item
    public Equipmenttype equipmentType; // loại Item
    public ClassItem classItem; // loại tộc hệ item
    public SkillWeapons skillWeapons; // thuộc tính vũ khí
    public GameObject ObjectItem;
    // Sau này có thể bổ sung
}
