using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTrigger : MonoBehaviour
{
    public static NpcTrigger instance;
    public Dialogue _dialog ;

    private bool playerDetected;
    [SerializeField] public CreateNpc _NpcData;
    [SerializeField] public GameObject _ButtonNpc;
    [SerializeField] public GameObject ButtonMision;

    public GameObject mainObject;
    public Transform mainTransform;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        NPCManager.Instance.RegisterNpcTrigger(this);
        mainObject = this.gameObject;
        mainTransform = mainObject.transform;
        ButtonMision = GameManager.Instance. FindObjectInChildren(mainObject.transform, "Mission");
        _ButtonNpc = GameManager.Instance. FindObjectInChildren(mainObject.transform, "ButtonNpc");
        _dialog = GameManager.Instance.FindObjectInChildren(mainObject.transform, "Dialogue").GetComponent<Dialogue>();

        _dialog.SetNpcName(_NpcData.NpcName);
       // Debug.Log("Npc chạy "+ mainObject.name);
    }

    //private void Start()
    //{
    //    NPCManager.Instance.RegisterNpcTrigger(this);
    //    instance = this;
    //  //  ButtonMision = GameObject.Find("Mission");
    //    // ButtonMision = _ButtonNpcMain.Find("Mission").gameObject;
    //    ButtonMision = mainTransform.Find("Mission").gameObject; 

    //}

    private void OnDestroy() // Sau khi close game thi.
    {
        //NPCManager.Instance.UnregisterNpcTrigger(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            NPCManager.Instance.ConectNpc(_NpcData);
            instance = this; // Tạo nhận diện cho NPC manager.
            playerDetected = true;
            _dialog.SetIndicator(playerDetected);
            // Cần hiển thị cả thông báo mission
            MissionManager.Instance.CheckingMission(_NpcData, out NPCManager.Instance.WhatMission, out NPCManager.Instance.WhatSmallMission);
            if (NPCManager.Instance.WhatMission != null)
            {
                if (NPCManager.Instance.WhatMission.Complete == false)
                {
                    _dialog.SetMissionEffectTransWith(true, "Revice", "Complete");
                }
                else if (NPCManager.Instance.WhatMission.Complete == true)
                {

                }
            }
             else if (NPCManager.Instance.WhatSmallMission != null)
            {
                 if (NPCManager.Instance.WhatSmallMission.TrangThaiMission == false)
                {
                    _dialog.SetMissionEffectTransWith(true, "Complete", "Revice");
                }
            }

            else
            {
                _dialog.SetMissionEffectTrans(false);
            }
          
            
            #region
            //if (MissionManager.Instance.CheckingMissionSmall(_NpcData, out NPCManager.Instance.WhatSmallMission))
            //{
            //    NPCManager.Instance.HaveMissionSmall = true;
            //}
            //else
            //{
            //    NPCManager.Instance.HaveMissionSmall = false;
            //}
            #endregion
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCManager.Instance.ConectNpc(null);
        _ButtonNpc.SetActive(false);
        if (collision.tag == "Player")
        {
            playerDetected = false;
            _dialog.SetIndicator(playerDetected);
            _dialog.SetMissionEffectTrans(false);
            _dialog.EndDialogue();
        }
    }
    public void ConnectNpc()
    {
        _ButtonNpc.SetActive(true);
        ButtonMision.SetActive(false);
    }
    public void ConnectNpcWithMission()
    {
        _ButtonNpc.SetActive(true);
        ButtonMision.SetActive(true);
    }
    public void TalkingNpc()
    {
        _ButtonNpc.SetActive(false);
        if (playerDetected)
        {
            _dialog.StartDialogue(_NpcData._TalkingNpcList);
            _dialog.SetWindows(true);
        }
    }
    public void TalkingNpcWithMission(CreateMissionSmall _MissionSmall)
    {
        _ButtonNpc.SetActive(false);
        if (playerDetected)
        {
            _dialog.StartDialogue(_MissionSmall.missionProperties._MissionTalkForGiverNpc.MissionListTalkingNpc);
            _dialog.SetWindows(true);
        }
    }
}
