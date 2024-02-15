using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Enemy", menuName = "2D / Enemy /New Boss Enemy ",order =1)]
public class CreateBossEnemy : ScriptableObject
{
    public int EnemyIndex;
    public string BossEnemyName;
    public float BossLevel;
    public string EnemyDes; // mo ta ve quai vat
    public BossItemsDrop ListItemDrop; // VatPhamroi ra
    public int HpEnemy;// mau quai
    public int DameEnemy;// Dame quai
    public int ArmorEnemy;// Giáp quai
    public ResistanceProperties KhangThuocTinh;
    public SkillProperties KynangTancong;
    public SkillProperties KynangPhongthu;
    public SkillProperties KynangPhuchoi;
    public Sprite EnemySprite;// Tạn thời chưa dùng đến

    public ItemBossnemy BossItemType; // Quai roi item khong
    public List<string> BossTalking;
}
[Serializable]
public enum ItemBossnemy
{
    DropItem = 0,
    NoDropItem = 1

}
[Serializable]
public struct BossItemsDrop
{
    public List<Item> BossEnemyItemList;
}
[Serializable]
public struct ResistanceProperties
{
    public float KhangBang;
    public float KhangHoa;
    public float KhangPhong;
}
[Serializable]
public struct SkillProperties
{
    public string SkillName;
    public float DameSkill;
    public float HealHpSkill;
    public float ArmorSkill;
}
