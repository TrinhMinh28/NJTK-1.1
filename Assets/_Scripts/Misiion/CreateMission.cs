using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "2D / Mission /New Missions", order =4 )]
public class CreateMission : ScriptableObject
{
    public int MissionIndex;
    public string MissionName;
    public string MissionDes;
    public int LeverMission;
    public CreateNpc NpcAssigned; // Npc giao nhiêm vụ
    public List<string> MissionTalkForGiverNpc;// Câu thoại cho người giao nhiệm vụ.
    public List<CreateMissionSmall> MissionSmall; // danh sách nv con
    public bool ActiveMission; // đã nhận nhiệm vụ hay chưa.
    public bool Complete; // đã hoàn thành chưa.
}

