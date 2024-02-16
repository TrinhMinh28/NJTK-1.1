using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    [SerializeField] GameObject InfiticationPanel;
    [SerializeField] GameObject InfiticationText;
    [SerializeField] GameObject InfiticationAcept;
    [SerializeField] GameObject InfiticationReject;

    private void Start()
    {
        InfiticationPanel.SetActive(false);
    }
    public void AcepEvents()
    {
        switch (MainManager.WhatchAction)
        {
            case "NewGame":
               // SaveManager.Instance.DeleteGame();
                MainManager.Instance.DeleteGame();
                break;
            case "ContinueGame":

                break;
            default:
                break;
        }
        LeverManager.Instance.LoadScence("Demo");
    }
    public void RejectEvents()
    {

    }
    public void ShowInfiticationWithYesNo(string Mess)
    {
        InfiticationPanel.SetActive(true);
        InfiticationAcept.SetActive(true);
        InfiticationReject.SetActive(true);
        InfiticationText.GetComponent<TextMeshProUGUI>().text = Mess;
    }
    public void ShowInfiticationWithYes(string Mess)
    {
        InfiticationPanel.SetActive(true);
        InfiticationAcept.SetActive(true);
        InfiticationReject.SetActive(false);
        InfiticationText.GetComponent<TextMeshProUGUI>().text = Mess;
    }
    public void Velang()
    {
        PlayerHeal.Instance.StartHeal();
        GameManager.Instance.PauseGame(false);
        SaveManager.Instance.BaseData.leverMapIndex = 0;
        MapManager.ReadyLoadRight = false;
        MapManager.Instance.WhatMap();
        LeverManager.Instance.UpdateTargetValue();
        // PlayerControler.Instance.MoveInstantly(MapManager.TeleLeftTrans);
        StartCoroutine(PlayerControler.Instance. WaitForLoadMap("TeleLeftGroundObject(Clone)")); // Nếu sau  muốn sửa thì thêm teletaget cho Npc
    }
    public void HoiSinhTaiCho()
    {
        GameManager.Instance.ShowThongBao("Trong hành trang không có Sinh mệnh đan");

        if (false)
        {

        }
    }
}
