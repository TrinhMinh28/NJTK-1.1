using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using static UnityEditor.Progress;
#endif

public class MissionManager : MonoBehaviour
{
    private static MissionManager instance;

    [SerializeField] private List<CreateMission> Missions = new List<CreateMission>();
    [SerializeField] private List<CreateMissionSmall> MissionSmall = new List<CreateMissionSmall>(); // đưa dữ liệu vào ddeerdebug, k dùng tới
    // [SerializeField] private int MissionIndex; // Index nhiệm vụ đang làm, dùng để debug hạn chế dùng
    //public int MissionSmallIndex;
    public TextMeshProUGUI missionText;

    [SerializeField] public CreateMission tempMission; // TempMission hiện tại nhân vật đang làm. Hạn chế xử lý vào dữ liệu gốc.
    [SerializeField] public TypeMission tempTypeMission; // TempMission hiện tại nhân vật đang làm. Hạn chế xử lý vào dữ liệu gốc.
    [SerializeField] private CreateMission tempMissionAgain; // tempMissionAgain tiếp theo nhân vật sẽ làm. Hạn chế xử lý vào dữ liệu gốc.[SerializeField] public CreateMission tempMission; 
    [SerializeField] private CreateMissionSmall tempMissionSmall; // Nhiem vu con dnag lam
    [SerializeField] private CreateMissionSmall tempMissionSmallAgain; // nhiem vu con tiep theo ==> Cho phép làm cùng lúc 2 nhiệm vụ con liền kề,
                                                                       // [SerializeField] private List<CreateMissionSmall> tempMissionSmalls; // Nếu muốn cơ chế làm nhiều nhiệm vụ con cùng lúc thì add nv chua làm vào đây ! 

    public static MissionManager Instance
    {
        get
        {
            // Nếu instance đã bị hủy bỏ, tìm lại và gán lại
            if (instance == null)
            {
                instance = FindObjectOfType<MissionManager>();

                // Nếu không tìm thấy, tạo mới instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<MissionManager>();
                    singletonObject.name = "MissionManager (Generated)";
                }

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        // Đảm bảo chỉ có một instance duy nhất
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        LoadMission();
       
    }
    private void FixedUpdate()
    {
        IdentifyMision(); // tu dong nhay nhiem vu
    }
    private void DisplayMissionDebug()
    {
        #region
        // missionText
        //for (int i = 0; i < Missions.Count; i++)
        //{
        //    if (MissionIndex == Missions[i].MissionIndex)
        //    {
        //        tempMission = Missions[i]; // Nhiệm vụ lớn đang làm.
        //        break;
        //    }
        //    else
        //    {
        //        tempMission = null;
        //    }
        //}

        //if (CheckTempMission())
        //{
        //    tempSmaillMission.Clear();
        //    for (int i = 0; i < tempMission.MissionSmall.Count; i++)
        //    {
        //        if (tempMission.MissionSmall[i].TrangThaiMission == false)
        //        {
        //            tempSmaillMission.Add(tempMission.MissionSmall[i]); // Nhệm vụ con chưa làm.
        //        }
        //    }

        //}
        //else
        //{
        //    tempSmaillMission.Clear();
        //}
        #endregion
        if (CheckTempMission())
        {
            MissionSmall.Clear();
            foreach (CreateMissionSmall mission in tempMission.MissionSmall) // Nhiee vu con cua nv lon
            {
                MissionSmall.Add(mission);
            }
        }
    }
    public void SaveMission()
    {
        if (Missions == null)
            return;
        MissionScriptTable newMissionData = ScriptableObject.CreateInstance<MissionScriptTable>();

        List<SaveMission> savedMissions = new List<SaveMission>();
        foreach (CreateMission mission in Missions)
        {
            SaveMission savedMission = new SaveMission { MissionCreate = mission };
            savedMissions.Add(savedMission);
        }

        newMissionData._MisionList = savedMissions;
        // newMissionData._MissionIndex = MissionIndex;

#if UNITY_EDITOR
        // Mã chỉ dùng trong chế độ Editor
        ScripttablbeMissionUtility.SaveMissionFile(newMissionData);
#endif
        // Lưu trữ đối tượng newItemData
    }
    public void LoadMission()
    {

        // Đọc dữ liệu từ file và gán vào Items và ItemNumbers
        MissionScriptTable loadedMissionData = Resources.Load<MissionScriptTable>("MissionSave/MissionDataSave");
        if (loadedMissionData != null)
        {
            Missions.Clear();
            foreach (SaveMission savedMission in loadedMissionData._MisionList)
            {
                Missions.Add(savedMission.MissionCreate);
            }
            // MissionIndex = loadedMissionData._MissionIndex;
        }
        else
        {
            Debug.LogError("Failed to load item data.");
        }
        IdentifyMision();
    }

    private void IdentifyMision()
    {
        for (int i = 0; i < Missions.Count; i++)
        {
            if (Missions[i].Complete == false)
            {
                if (i < Missions.Count - 1)
                {
                    tempMission = Missions[i];
                    tempMissionAgain = Missions[i + 1];
                    break;
                }
                else
                {
                    tempMission = Missions[i];
                    tempMissionAgain = null;
                    break;
                }
            }
        }

        if (tempMission.ActiveMission == true)
        {
            for (int i = 0; i < tempMission.MissionSmall.Count; i++)
            {
                if (tempMission.MissionSmall[i].TrangThaiMission == false)
                {
                    if (i < tempMission.MissionSmall.Count - 1)
                    {
                        tempMissionSmall = tempMission.MissionSmall[i]; // nhiem vu hien tai
                        tempMissionSmallAgain = tempMission.MissionSmall[i + 1]; // nhiem vu kế
                        tempTypeMission = tempMission.MissionSmall[i].TypeSmallMiss;
                        break;
                    }
                    else
                    {
                        tempMissionSmall = tempMission.MissionSmall[i];
                        tempTypeMission = tempMission.MissionSmall[i].TypeSmallMiss;
                        tempMissionSmallAgain = null; // nhiem vu kế
                        break;
                    }
                }
                else
                {
                    tempMission.Complete = true; // hoan thanh het nv con
                }
            }
        }
        else
        {
            tempMissionSmall = null; tempMissionSmallAgain = null;
            tempMission.Complete = false;
        }
        for (int i = 0; i < tempMission.MissionSmall.Count; i++) // neu co mot nhiem vu con chua hoan thanh thi nv lon sex chua danh dau la hoan thanh
        {
            if (tempMission.MissionSmall[i].TrangThaiMission == false)
            {
                tempMission.Complete = false;
                break;
            }

        }
        DisplayMissionDebug();
    }

    public void UpdateMission(CreateMission _Mission, string _taget)
    {
        if (_taget == "Accept")
        {
            //  MissionIndex = _Mission.MissionIndex;
            _Mission.ActiveMission = true;
        }
        else if (_taget == "Reject")
        {
            //  MissionIndex = _Mission.MissionIndex;
            _Mission.ActiveMission = false;
        }

        // Goi hieu ung nhan va huy nhiem vu
        Debug.LogWarning("Vui long cap nhat hieu ung nhan va huy Mission");

    }
    public void UpdateMissionSmall(CreateMissionSmall _MissionSmall, bool Trangthai)
    {
        if (Trangthai == true)
        {
            _MissionSmall.TrangThaiMission = Trangthai;
            Debug.Log("Bạn vừa hoàn thành nhiệm vụ" + _MissionSmall.MissionSmallName);
        }
        else
        {
            _MissionSmall.TrangThaiMission = Trangthai;
        }

    }
    void xacDinhloaiMission()
    {
        switch (tempTypeMission)
        {
            case TypeMission.Attackenemy:
                // Xử lý khi tempTypeMission là TypeMission.Attackenemy
                break;
            case TypeMission.TakingNpc:
                // Xử lý khi tempTypeMission là TypeMission.TakingNpc
                break;
            case TypeMission.UseItem:
                // Xử lý khi tempTypeMission là TypeMission.UseItem
                break;
            case TypeMission.CollectItems:
                // Xử lý khi tempTypeMission là TypeMission.CollectItems
                break;
            default:
                // Xử lý mặc định nếu không khớp với bất kỳ trường hợp nào
                break;
        }
    }
    public void CheckingMission(CreateNpc _Npc, out CreateMission MissionOut , out CreateMissionSmall MisionSmallOut)
    {
        MissionOut = null;
        MisionSmallOut = null;
        if (_Npc != null)
        {
            if (CheckTempMission())
            {
                if (_Npc == tempMission.NpcAssigned)  // hoac  if (mission.MissionNpc == tempMission)
                {
                     MissionOut = tempMission;
                    NPCManager.Instance.HaveMission = true;
                }
                else
                {
                    MissionOut = null;
                    NPCManager.Instance.HaveMission = false;
                }
                if (CheckTempSmallMission())
                {
                    if (_Npc == tempMissionSmall.missionProperties._MissionTalkForGiverNpc.NpcAssigned)
                    {
                        MisionSmallOut = tempMissionSmall;
                        NPCManager.Instance.HaveMissionSmall = true;
                    }
                    else
                    {
                        MisionSmallOut = null;
                        NPCManager.Instance.HaveMissionSmall = false;
                    }
                }
            }
        }

    }
    #region
    //public bool CheckingMissionSmall(CreateNpc npc, out MissionContain _MissionSmallOut)
    //{
    //    bool Return = false;
    //    _MissionSmallOut.MissionSmallNpc = null;
    //    _MissionSmallOut.MissionTalkingListNpc = null;

    //    if (_Npc != null)
    //    {
    //        if (CheckTempMission())
    //        {
    //            if (_Npc == tempMission.NpcAssigned)  // hoac  if (mission.MissionNpc == tempMission)
    //            {
    //                MissionOut = tempMission;
    //                Return = true;
    //            }

    //        }
    //    }

    //    return Return;
    //}
    #endregion
    public bool CheckTempMission()
    {
        bool Return = false;
        if (tempMission != null)
        {
            Return = true;
        }
        return Return;
    }
    public bool CheckTempSmallMission()
    {
        bool Return = false;
        if (tempMissionSmall != null)
        {
            Return = true;
        }
        return Return;
    }

    public void ResetMission()
    {
        LoadMission();
        if (Missions != null)
        {
            foreach (CreateMission missions in Missions)
            {
                foreach (CreateMissionSmall missmall in missions.MissionSmall)
                {
                    missmall.TrangThaiMission = false;
                }
                missions.ActiveMission = false;
                missions.Complete = false;
            }
        }
        else 
        {
            Debug.LogError("Reset Mission that bai");
        }
      
    }
    public void ShowMission()
    {
        missionText = GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>();
        missionText.text = getMissionText(tempMission, tempMission.MissionSmall);
    }

    private string getMissionText(CreateMission _MissionInfo, List<CreateMissionSmall> _SmallMissionsList)
    {
        if (_MissionInfo == null)
        {

            return (" <size=50>Bạn chưa nhận nhiệm vụ nào </size> ");
        }
        else
        {
            if (_MissionInfo.ActiveMission == false)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("<color=black><size=40>Nhiêm vụ: </size></color> <color=orange><size=45>{0}</size></color> \n", _MissionInfo.MissionName);
                stringBuilder.AppendFormat("<color=black><size=40>Goi y: </size></color> <color=yellow><size=45>{0}</size></color> \n", _MissionInfo.MissionDes);
                return stringBuilder.ToString();
            }
            else
            {
                if (_SmallMissionsList == null)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendFormat("<color=black><size=40>Nhiêm vụ: </size></color> <color=orange><size=45>{0}</size></color> \n", _MissionInfo.MissionName);
                    stringBuilder.AppendFormat("<color=black><size=40>Goi y: </size></color> <color=yellow><size=45>{0}</size></color> \n", _MissionInfo.MissionDes);
                    return stringBuilder.ToString();
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendFormat("<color=black><size=40>Nhiêm vụ: </size></color> <color=orange><size=45>{0}</size></color> \n", _MissionInfo.MissionName);
                    for (int i = 0; i < _SmallMissionsList.Count; i++)
                    {
                        if (_SmallMissionsList[i].TrangThaiMission == true)
                        {
                            stringBuilder.AppendFormat("<color=black><size=40>Nhiêm vụ: </size></color> <color=white><size=45>{0}</size></color> \n", _SmallMissionsList[i].MissionSmallName);
                        }
                        else if (_SmallMissionsList[i] == tempMissionSmall)
                        {
                            stringBuilder.AppendFormat("<color=black><size=40>Nhiêm vụ: </size></color> <color=orange><size=45>{0}</size></color> \n", _SmallMissionsList[i].MissionSmallName);
                            stringBuilder.AppendFormat("<color=black><size=40>Goi y: </size></color> <color=yellow><size=45>{0}</size></color> \n", _SmallMissionsList[i].MissionSmaillDes);
                        }
                        else if (_SmallMissionsList[i].TrangThaiMission == false)
                        {
                            stringBuilder.AppendFormat("<color=black><size=40>Nhiêm vụ: </size></color> <color=#575757><size=45>{0}</size></color> \n", _SmallMissionsList[i].MissionSmallName);
                        }
                    }
                    return stringBuilder.ToString();

                }
            }
        }
    }
}


#if UNITY_EDITOR
// Mã chỉ dùng trong chế độ Editor
public static class ScripttablbeMissionUtility
{
    public static void SaveMissionFile(MissionScriptTable newItemData)
    {
        AssetDatabase.CreateAsset(newItemData, $"Assets/Resources/MissionSave/MissionDataSave.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
#endif


