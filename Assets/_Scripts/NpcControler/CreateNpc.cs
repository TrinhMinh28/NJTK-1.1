using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
[CreateAssetMenu(fileName = "New Npc", menuName = "2D / Npc /New Npcs", order = 3)]
public class CreateNpc : ScriptableObject
{
    public NameNpc npc;
    public int NpcIndex;
    public string NpcName;
    public string NpcDescription;
    public List<string> _TalkingNpcList;//
    public ItemForNpc ItemForNpc;

   // public List<MissionAssigned> ListMissionsAssigned; // nhệm vụ Giao cho Player
   // public List<MissionContain> ListMissionsContain; // Nhiệm vụ tương tác với Player

    
    public TypeNpc TypeNpc;

    private void OnValidate()
    {
        // Kiểm tra giá trị của npc và ẩn/hiển thị các trường tương ứng 
        if (npc == NameNpc.Taijma)
        {
            NpcIndex = Convert.ToInt32( NameNpc.Taijma);
            NpcName = NameNpc.Taijma.ToString();
        }
        else if (npc == NameNpc.Tabemono)
        {
            NpcIndex = Convert.ToInt32(NameNpc.Tabemono);
            NpcName = NameNpc.Tabemono.ToString();
        }
        else if (npc == NameNpc.Kirito)
        {
            NpcIndex = Convert.ToInt32(NameNpc.Kirito);
            NpcName = NameNpc.Kirito.ToString();
        }
        else if (npc == NameNpc.Katana)
        {
            NpcIndex = Convert.ToInt32(NameNpc.Katana);
            NpcName = NameNpc.Katana.ToString();
        }
        else if (npc == NameNpc.Kisibongdem)
        {
            NpcIndex = Convert.ToInt32(NameNpc.Kisibongdem);
            NpcName = NameNpc.Kisibongdem.ToString();
        }
    }
}
[Serializable]
public struct ItemForNpc
{
    public List<Item> ItemsforNpc;
}

//[Serializable]
//public struct MissionAssigned
//{
//    public CreateMission MissionNpc;
//    public List<string> MissionTalkingListNpc;
//}
//[Serializable]
//public struct MissionContain
//{
//    public CreateMissionSmall MissionSmallNpc;
//    public List<string> MissionTalkingListNpc;
//}
[Serializable]
public enum TypeNpc
{
    HaveMission = 1,
    NoMission = 0
}
[Serializable]
public enum NameNpc
{
    Taijma = 0,
    Tabemono = 1,
    Kirito = 2,
    Katana = 3,
    Kisibongdem =4
}


