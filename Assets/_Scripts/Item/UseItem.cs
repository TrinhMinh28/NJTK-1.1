using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class UseItem : MonoBehaviour
{
    public static UseItem Instance;
    [SerializeField] private GameObject UseBottonPanel;
    [SerializeField] private GameObject _UseItem;
    [SerializeField] private GameObject UseBottonPanelNpc;
    [SerializeField] private GameObject _UseItemNpc;
    [SerializeField] private GameObject UseBottonPanelInfoItems;
    [SerializeField] private GameObject _UseItemInfo;
     public Text UseBottonText;
     public Text UseBottonTextNpc;
     public Text UseBottonTextItemInfo;
    private void Start ()
    {
        Instance = this;
        //UseBottonPanel.SetActive(false);
        //Instance.gameObject.SetActive(false);
    }
    public void BoradatItem()
    {
        ItemManager.Instance.RemoveItem(ItemManager.Instance.ItemSelected, 1, ItemManager.Instance.IdSlotSelected.id); // Bo đi 1 Item đa chọn n
    }
    public void SudungItem()
    {
        // Debug.Log("Goi su dung item");
        ItemManager.Instance.UseItem();
    }
    public void MuaItem()
    {
        if (SaveManager.Instance.BaseData.Yen - NPCManager.Instance.ItemSelectedForNpc.ItemPrice >=0)
        {
            ItemManager.Instance.UpdateMoney("subtract", NPCManager.Instance.ItemSelectedForNpc.ItemPrice);
            ItemManager.Instance.AddItem(NPCManager.Instance.ItemSelectedForNpc); // Them item da mua x
        }
        else
        {
            GameManager.Instance.ShowThongBao("Bạn không đủ tiền mua vật phẩm");
        }
       
    }
    public void MuaItemSLLItem()
    {

    }
    public void ThaoTrangBi()
    {
        Debug.LogWarning("ThaoTbi");
        InfomationManager.Instance. ThaoTrangBi();
    }
    public void CloseUseItem()
    {
        _UseItem.SetActive(true);
        UseBottonPanel.SetActive(false);
    }
    public void CloseUseItemNpc()
    {
        _UseItemNpc.SetActive(true);
        UseBottonPanelNpc.SetActive(false);
    }
    public void CloseUseItemInfo()
    {
        _UseItemInfo.SetActive(true);
        UseBottonPanelInfoItems.SetActive(false);
    }
    public void RefeshInventoryNpc()
    {
        Debug.Log("Gọi làm mới ");
    }
    public void OpenUsePanel()
    {
        if (UseBottonText.text =="Sử dụng")
        {
            UseBottonPanel.SetActive(true);
        }
        else
        {
            RefeshInventoryNpc();
        }
       
    }
    public void OpenUsePanelNpc()
    {
        if (UseBottonTextNpc.text =="Mua")
        {
            UseBottonPanelNpc.SetActive(true);
        }
        else
        {
            RefeshInventoryNpc();
        }
       
    }
    public void OpenUsePanelItemInfo()
    {
        if (UseBottonTextItemInfo.text == "Chọn")
        {
            UseBottonPanelInfoItems.SetActive(true);
        }
        else
        {
            RefeshInventoryNpc();
        }
       
    }

        

}
