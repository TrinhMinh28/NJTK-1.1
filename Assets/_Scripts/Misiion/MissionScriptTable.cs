using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionScriptTable : ScriptableObject
{
    public List<SaveMission> _MisionList;
  //  public int _MissionSmallIndex; // Đang làm đến nhiệm vụ con thứ ... 
   // public int _MissionIndex; // Đang làm đến nhiệm vụ thứ ...
} 

[Serializable]
public class SaveMission
{
    public CreateMission MissionCreate;
}
