using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "MissionSmall", menuName = "2D / Mission /New Missions Small")]
public class CreateMissionSmall : ScriptableObject
{

    public TypeMission TypeSmallMiss; // Loai nhiem vu
    public int MissionSmallIndex;
    public string MissionSmallName;
    public string MissionSmaillDes;
    public MissionItems _MissionSmallItemGift; // vật phẩm nhiệm vụ
    public MissionItems _MissionSmallItemsReward; // Vật phẩm thưởng nhiệm vụ
    public Sprite MissionSmallSprite;// Tạn thời chưa dùng đế
    public bool TrangThaiMission;

    public MissionProperty missionProperties;
   


}

[Serializable]
public enum TypeMission
{
    TakingNpc = 0,
    CollectItems = 1,
    UseItem = 2,
    Attackenemy = 3
        // tạn thời 3 loại nv 
}
[Serializable]
public struct MissionItems
{
    public int Exp;
    public List<Item> MissionItemList;
}
[Serializable]
public struct MissionTalking
{
    public CreateNpc NpcAssigned;
    public List<string> MissionListTalkingNpc;
}
[Serializable]
public struct MissionCollectionItem
{
    public Item _ColectionItem;
    public int SoluongItem;
}
[Serializable]
public struct MissionUseItem
{
    public Item _UseItem;
    public int SoluongItem;
}
[Serializable]
public struct MissionAttackEnemy
{
    public CreateEnemy _AttackEnemy;
    public int SoluongEnemy;
}

[Serializable]
public struct MissionProperty
{
    public MissionTalking _MissionTalkForGiverNpc;// Câu thoại Nhiệm vụ
    public List<MissionCollectionItem> _MissionColectionItem; // Cần nhặt item gì để hoàn thành nhiệm vụ
    public List<MissionUseItem> _MissionUseItem; // Cần dùng item gì để hoàn thành nhiệm vụ
    public List<MissionAttackEnemy> _MissionAttackEnemy; // Quái vật cần giết của nhiệm vụ
}


//public List<string> MissionTalkForGiverNpc;// Câu thoại Nhiệm vụ
//public List<Item> _MissionColectionItem; // Cần nhặt item gì để hoàn thành nhiệm vụ
//public List<Item> _MissionUseItem; // Cần dùng item gì để hoàn thành nhiệm vụ
//public List<CreateEnemy> _MissionAttackEnemy; // Quái vật cần giết của nhiệm vụ
