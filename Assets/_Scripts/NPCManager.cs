using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
// Mã chỉ dùng trong chế độ Editor
using static UnityEditor.Progress;
#endif
using UnityEditor;
using System;

public class NPCManager : MonoBehaviour
{
    private static NPCManager instance;
    private List<NpcTrigger> _npcTriggers;
    public CreateNpc thisNpcData;

    public SlotStruc[] SlotsNpc;

    public NumberNpc SlotNpcid;
    [SerializeField]private GameObject _parentNpc;
    [SerializeField] private GameObject _SlotFrefabNpc;
    public Item ItemSelectedForNpc;

    public bool HaveMission;
    public CreateMission WhatMission;
   // public MissionAssigned MissionOfNpc;
    public bool HaveMissionSmall;
    public CreateMissionSmall WhatSmallMission;
    // public MissionContain MissionSmallOfNpc;
    // kiẻm tra xem npc đang có nhiệm vụ nào.
    // Getter cho instance
    public static NPCManager Instance
    {
        get
        {
            // Nếu instance đã bị hủy bỏ, tìm lại và gán lại
            if (instance == null)
            {
                instance = FindObjectOfType<NPCManager>();

                // Nếu không tìm thấy, tạo mới instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<NPCManager>();
                    singletonObject.name = "NPCManager (Generated)";
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

    private void Start()
    {
        _parentNpc = GameObject.Find("Inventory Panel Npc");
    }
    // Các phương thức và thuộc tính của NPCManager
    // ...

    // Noi chuyen 
    public void NpcTakling()
    {
        if (WhatSmallMission != null && WhatSmallMission.TypeSmallMiss == TypeMission.TakingNpc)
        {
            Debug.Log("Xác nhận đã làm nhiệm vụ, tiếp theo là update nhiệm vụ ");
            NpcTrigger.instance.TalkingNpcWithMission(WhatSmallMission);
            MissionManager.Instance.UpdateMissionSmall(WhatSmallMission, true);
        }
        else
        {
            NpcTrigger.instance.TalkingNpc();
        }
       
    }
    public void NpcBuying()
    {
        NpcTrigger.instance._ButtonNpc.SetActive(false);
        InstantiateSlot();
        DisplayItem();
        ButtonEvents.Instance.OpenNpcInventory();
    }

    void DisplayItem()
    {
        #region
        //for (int i = 0; i < Items.Count; i++)
        //{
        //    SlotsNpc[i].Slot.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //    SlotsNpc[i].Slot.transform.GetChild(0).GetComponent<Image>().sprite = Items[i].ItemSprite;
        //    //updte text
        //    SlotsNpc[i].Slot.transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 1);
        //    SlotsNpc[i].Slot.transform.GetChild(1).GetComponent<Text>().text = ItemNumbers[i].ToString();

        //    //Update throw utton
        //    SlotsNpc[i].Slot.transform.GetChild(2).gameObject.SetActive(true);
        //}
        #endregion
        for (int i = 0; i < SlotsNpc.Length; i++)
        {
            if (i < thisNpcData.ItemForNpc.ItemsforNpc.Count)
            {
                SlotsNpc[i].Slot.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                SlotsNpc[i].Slot.transform.GetChild(1).GetComponent<Image>().sprite = thisNpcData.ItemForNpc.ItemsforNpc[i].ItemSprite;
                //updte text
                SlotsNpc[i].Slot.transform.GetChild(2).GetComponent<Text>().color = new Color(1, 1, 1, 0);
                SlotsNpc[i].Slot.transform.GetChild(2).GetComponent<Text>().text = null;

                //Update throw utton
                SlotsNpc[i].Slot.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                SlotsNpc[i].Slot.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                SlotsNpc[i].Slot.transform.GetChild(1).GetComponent<Image>().sprite = null;
                //updte text
                SlotsNpc[i].Slot.transform.GetChild(2).GetComponent<Text>().color = new Color(1, 1, 1, 0);
                SlotsNpc[i].Slot.transform.GetChild(2).GetComponent<Text>().text = null;

                //Update throw utton
                SlotsNpc[i].Slot.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }

    public void Selected(int _id, Item _items)
    {

        for (int i = 0; i < SlotsNpc.Length; i++)
        {
            if (i == _id)
            {
                SlotsNpc[i].Slot.transform.GetChild(3).gameObject.SetActive(true);
                ItemSelectedForNpc = _items;
            }
            else
            {
                SlotsNpc[i].Slot.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }
    void InstantiateSlot()
    {
        if (thisNpcData.ItemForNpc.ItemsforNpc.Count == 0)
        {
            Debug.Log("Không có vật phẩm để bán");
        }
        if (SlotsNpc != null)
        {
            DestroySlots();
            SlotNpcid.id = 0;
        }
       // Array.Resize(ref SlotsNpc, thisNpcData.ItemForNpc.ItemsforNpc.Count + 2);
        SlotsNpc = new SlotStruc[thisNpcData.ItemForNpc.ItemsforNpc.Count +2 ]; // Khởi tạo mảng SlotsNpc với độ dài số lượng vật phẩm bán  2
        for (int i = 0; i < SlotsNpc.Length; i++)
        {
            GameObject spawnedTitle = Instantiate(_SlotFrefabNpc, _parentNpc.transform);
            SlotsNpc[i].Slot = spawnedTitle; // Lưu đối tượng clon vào mảng SlotsNpc
            SlotsNpc[i].index = i; // 

        }
    }

    void DestroySlots()
    {
        for (int i = 0; i < SlotsNpc.Length; i++) 
        {
            Destroy(SlotsNpc[i].Slot); // Hủy đối tượng clone
        }

        SlotsNpc = new SlotStruc[0]; // Gán mảng Slots thành một mảng rỗng
    }
    public void RegisterNpcTrigger(NpcTrigger npcTrigger)
    {

    }

    public void UnregisterNpcTrigger(NpcTrigger npcTrigger)
    {
      
    }
    public void ConectNpc(CreateNpc _data)
    {
        if (_data != null)
        {
            thisNpcData = _data;
        }
        else
        {
            thisNpcData = null;
        }
        
    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.E))
        {
            if (thisNpcData !=null)
            {
                if (HaveMission) // nhiem vu lon chưa active.
                {
                    // Debug.Log("Goi HaveMission");
                    NpcTrigger.instance.ConnectNpcWithMission();
                }
                //else if (HaveMissionSmall) // Nhiem vu can tuong tac voi npc. Hiejn tại chi co noi chuyen tuong lai co le can pt them. 
                //{ // cơ chế gắn nv vào npc và thêm điều kiện cho nv. Vd: Nv nói truyejn thì ngoài type nv thì thêm cả data của npc vào để check.
                //    Debug.Log("Goi HaveMissionSmall 2");
                //    NpcTrigger.instance.ConnectNpcWithMission();
                //     // cơ chế tự nhảy đoạn hội thoại không cần nhấn space
                //}
                else
                {
                    Debug.Log("Goi else");
                    NpcTrigger.instance.ConnectNpc();
                }
            }
        }
    }
}
public struct NumberNpc
{
    public int id;
}
