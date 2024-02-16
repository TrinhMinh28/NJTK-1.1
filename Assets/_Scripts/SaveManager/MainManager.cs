using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    // Start is called before the first frame update
    public GameObject ChoiTiep;
    public static string WhatchAction;
    void Start()
    {
        Instance = this;
        CheckData();
    }
    private static string savePaths => Application.persistentDataPath + "/save.game";
    private void CheckData()
    {
        if (!File.Exists(savePaths)) // Nếu như chưa có dữ liệu thì tạo mới dữ liệu người dùng.
        {
            Debug.LogError("Chưa có dữ liệu ng dùng");

            ChoiTiep.SetActive(false);
        }
        else
        {
            Debug.LogError("có dữ liệu ng dùng");
            ChoiTiep.SetActive(true);
        }

    }
    public void NewGame()
    {
        NotificationUI Notifi = GameObject.Find("ThongBao").GetComponent<NotificationUI>();
        Notifi.ShowInfiticationWithYesNo("Chơi mới sẽ xóa toàn bộ dữ liệu đã lưu trước đó ! bạn có muốn tiếp tục ?");
        WhatchAction = "NewGame";
    }
    public void ContinueGame()
    {
        NotificationUI Notifi = GameObject.Find("ThongBao").GetComponent<NotificationUI>();
        Notifi.ShowInfiticationWithYesNo("Bạn có muốn chơi tiếp từ màn chơi trước đó ?");
        WhatchAction = "ContinueGame";
    }

    public void DeleteGame()
    {
        if (File.Exists(savePaths))
        {
            File.Delete(savePaths);
            File.Delete(savePaths + ".meta");
        }
        //  MissionManager.Instance.ResetMission();
        ResetMission();
      //  ItemManager.Instance.ResetItems();
        ResetItems();
    }

    private void ResetMission()
    {
        List<CreateMission> Missions = new List<CreateMission>();
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

    private void ResetItems()
    {
        // Load Items
        List<Item> Items = new List<Item>();

        List<int> ItemNumbers = new List<int>();
        ItemScriptTable loadedItemData;
        loadedItemData = Resources.Load<ItemScriptTable>("ItemsSave/ItemDataSave");
        if (loadedItemData != null)
        {
            Items.Clear();
            foreach (SavedItem savedItem in loadedItemData._Items)
            {
                Items.Add(savedItem.Item);
            }
            ItemNumbers = new List<int>(loadedItemData._ItemNumbers);
        }
        else
        {
            Debug.LogError("Failed to load item data.");
        }


        ///***************
        ///ResetItems
        Items.Clear();
        ItemNumbers.Clear();

        ItemScriptTable newItemData = ScriptableObject.CreateInstance<ItemScriptTable>();

        List<SavedItem> savedItems = new List<SavedItem>();
        foreach (Item item in Items)
        {
            SavedItem savedItem = new SavedItem { Item = item };
            savedItems.Add(savedItem);
        }

        newItemData._Items = savedItems;
        newItemData._ItemNumbers = ItemNumbers;

        if (newItemData._Items == null)
        {
            loadedItemData._ItemNumbers = null;
            loadedItemData._Items = null;
        }
        else
        {
            loadedItemData._ItemNumbers = newItemData._ItemNumbers;
            loadedItemData._Items = newItemData._Items;
        }

        //************************************************
        // Load IntemsIfomation

        InfomationItemScript loadedItemDataOfInformation;
        //// Đọc dữ liệu từ file và gán vào Items và ItemNumbers
        loadedItemDataOfInformation = Resources.Load<InfomationItemScript>("ItemsSave/InfomationItemData");
        //// Đọc dữ liệu từ file và gán vào Items và ItemNumbers
        if (loadedItemDataOfInformation != null)
        {
          
        }
        else
        {
            Debug.LogError("Failed to load item data.");
        }

        ///******** RemoveItems Ìnomaion 
        ///
        SavedItemInfo newItemDataOfInf = new SavedItemInfo();
        newItemDataOfInf = null;
        loadedItemDataOfInformation._Items = null;
    }
}
