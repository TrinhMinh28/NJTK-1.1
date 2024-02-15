
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if UNITY_EDITOR
using static UnityEditor.Progress;
#endif

public class InfomationButton : MonoBehaviour, IPointerDownHandler
{
    public static InfomationButton Instance;
    public Item ThisItem;

    private void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Invoke("ShowItem", 2f);
        UpdateSkill();
    }

    private void ShowItem()
    {
        if (ThisItem == null)
        {
            Invoke("ShowItem", 5f);
            gameObject.GetComponent<Image>().sprite = null;
            UpdateChracter();
            return;
        }
        gameObject.GetComponent<Image>().sprite = ThisItem.ItemSprite;
        // Invoke("ShowItem", 10f);
        UpdateChracter();
    }
    public void UpdateItem(Item item)
    {
        if (ThisItem != null)
        {
            ItemManager.Instance.RemoveItem(ItemManager.Instance.ItemSelected, 1, ItemManager.Instance.IdSlotSelected.id);
            ItemManager.Instance.AddItem(ThisItem);
            GiamChiSoCharacter();
            ThisItem = item;
        }
        else
        {
            ItemManager.Instance.RemoveItem(ItemManager.Instance.ItemSelected, 1, ItemManager.Instance.IdSlotSelected.id);
            ThisItem = item;
        }
        ShowItem();
    }
    public void ThaoItem(Item item)
    {
        ItemManager.Instance.AddItem(ThisItem);
        ThisItem = null;
        ShowItem();
    }
    public void UpdateSkill()
    {
        if (ThisItem != null)
        {
            switch (ThisItem._TypeUsableItem.equipmentType) // Nếu là đối tượng khác vũ khí sẽ cộng MPHP v.v...
            {
                case Equipmenttype.vukhicanchien:
                    // Add dame va add skill
                    CanchienVukhi CCVK = GameObject.FindObjectOfType<CanchienVukhi>();
                    CCVK.ShowSkill(ThisItem._TypeUsableItem.skillWeapons);
                    break;
                case Equipmenttype.vukhixachien:
                    XachienVukhi XCVK = GameObject.FindObjectOfType<XachienVukhi>();
                    XCVK.ShowSkill(ThisItem._TypeUsableItem.skillWeapons);
                    break;
                case Equipmenttype.ao:

                    break;
                case Equipmenttype.gang:

                    break;
                case Equipmenttype.quan:

                    break;
                case Equipmenttype.daychuyen:

                    break;
                case Equipmenttype.nhan:

                    break;
                case Equipmenttype.bua:

                    break;
                default:
                    break;
            }
        }
    }
    public void ThaoSkill()
    {
        SkillWeapons weapons = new SkillWeapons()
        {
            SkillPhong = null,
            SkillBang = null,
            SkillHoa = null,
            SkillBase = null

        };
        switch (ThisItem._TypeUsableItem.equipmentType) // Nếu là đối tượng khác vũ khí sẽ cộng MPHP v.v...
        {
            case Equipmenttype.vukhicanchien:
                // Add dame va add skill
                CanchienVukhi CCVK = GameObject.FindObjectOfType<CanchienVukhi>();
                CCVK.RemoveSkill(weapons);
                break;
            case Equipmenttype.vukhixachien:
                XachienVukhi XCVK = GameObject.FindObjectOfType<XachienVukhi>();
                XCVK.RemoveSkill(weapons);
                break;
            case Equipmenttype.ao:

                break;
            case Equipmenttype.gang:

                break;
            case Equipmenttype.quan:

                break;
            case Equipmenttype.daychuyen:

                break;
            case Equipmenttype.nhan:

                break;
            case Equipmenttype.bua:

                break;
            default:
                break;
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject useItemInfo = GameObject.Find("UseItemInfomation");
        Text text = GameManager.Instance.FindObjectInChildren(useItemInfo.transform, "TextUse").GetComponent<Text>();
        InfomationManager.Instance.InfomationItemSelected(gameObject, ThisItem);
        InfomationManager.Instance.ShowInfomationItems();
        if (InfomationManager.Instance.ItemSelected != null)
        {
            //  UseItem.Instance.gameObject.SetActive(true);
            useItemInfo.SetActive(true);
            text.text = "Chọn";
        }
        else
        {
            text.text = "";
        }

    }

    internal void UpdateChracter()
    {
        if (ThisItem != null)
        {
            switch (ThisItem._TypeUsableItem.equipmentType) // Nếu là đối tượng khác vũ khí sẽ cộng MPHP v.v...
            {
                case Equipmenttype.vukhicanchien:
                    // Add dame va add skill
                    InfomationManager.Instance.LoadData.Hp += (SaveManager.Instance.BaseData.Hp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.HealBonus;
                    InfomationManager.Instance.LoadData.Dame += (SaveManager.Instance.BaseData.Dame * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.DameBonus;
                    InfomationManager.Instance.LoadData.XuyenGiap += (SaveManager.Instance.BaseData.XuyenGiap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.XuyenGiapBonus;
                    InfomationManager.Instance.LoadData.Crit += (SaveManager.Instance.BaseData.Crit * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.CritBonus;
                    break;
                case Equipmenttype.vukhixachien:
                    InfomationManager.Instance.LoadData.Mp += (SaveManager.Instance.BaseData.Mp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.ManaNonus;
                    InfomationManager.Instance.LoadData.Dame += (SaveManager.Instance.BaseData.Dame * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.DameBonus;
                    InfomationManager.Instance.LoadData.XuyenGiap += (SaveManager.Instance.BaseData.XuyenGiap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.XuyenGiapBonus;
                    InfomationManager.Instance.LoadData.Crit += (SaveManager.Instance.BaseData.Crit * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.CritBonus;
                    break;
                case Equipmenttype.ao:
                    InfomationManager.Instance.LoadData.Hp += (SaveManager.Instance.BaseData.Hp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.HealBonus;
                    InfomationManager.Instance.LoadData.Giap += (SaveManager.Instance.BaseData.Giap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.GiapBonus;
                    break;
                case Equipmenttype.gang:
                    InfomationManager.Instance.LoadData.Hp += (SaveManager.Instance.BaseData.Hp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.HealBonus;
                    InfomationManager.Instance.LoadData.Giap += (SaveManager.Instance.BaseData.Giap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.GiapBonus;
                    break;
                case Equipmenttype.quan:
                    InfomationManager.Instance.LoadData.Hp += (SaveManager.Instance.BaseData.Hp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.HealBonus;
                    InfomationManager.Instance.LoadData.Giap += (SaveManager.Instance.BaseData.Giap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.GiapBonus;
                    break;
                case Equipmenttype.daychuyen:
                    InfomationManager.Instance.LoadData.Mp += (SaveManager.Instance.BaseData.Mp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.ManaNonus;
                    InfomationManager.Instance.LoadData.XuyenGiap += (SaveManager.Instance.BaseData.XuyenGiap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.XuyenGiapBonus;
                    break;
                case Equipmenttype.nhan:
                    InfomationManager.Instance.LoadData.Mp += (SaveManager.Instance.BaseData.Mp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.ManaNonus;
                    InfomationManager.Instance.LoadData.XuyenGiap += (SaveManager.Instance.BaseData.XuyenGiap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.XuyenGiapBonus;
                    break;
                case Equipmenttype.bua:
                    InfomationManager.Instance.LoadData.Mp += (SaveManager.Instance.BaseData.Mp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.ManaNonus;
                    InfomationManager.Instance.LoadData.XuyenGiap += (SaveManager.Instance.BaseData.XuyenGiap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.XuyenGiapBonus;
                    break;
                default:
                    break;
            }
            InfomationManager.Instance.ShowInfomationCharacter();
            PlayerHeal.Instance.StartHeal();
        }
    }

    public void GiamChiSoCharacter()
    {
        if (ThisItem != null)
        {
            switch (ThisItem._TypeUsableItem.equipmentType) // Nếu là đối tượng khác vũ khí sẽ cộng MPHP v.v...
            {
                case Equipmenttype.vukhicanchien:
                    // Add dame va add skill
                    InfomationManager.Instance.LoadData.Hp -= (SaveManager.Instance.BaseData.Hp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.HealBonus;
                    InfomationManager.Instance.LoadData.Dame -= (SaveManager.Instance.BaseData.Dame * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.DameBonus;
                    InfomationManager.Instance.LoadData.XuyenGiap -= (SaveManager.Instance.BaseData.XuyenGiap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.XuyenGiapBonus;
                    InfomationManager.Instance.LoadData.Crit -= (SaveManager.Instance.BaseData.Crit * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.CritBonus;
                    break;
                case Equipmenttype.vukhixachien:
                    InfomationManager.Instance.LoadData.Mp -= (SaveManager.Instance.BaseData.Mp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.ManaNonus;
                    InfomationManager.Instance.LoadData.Dame -= (SaveManager.Instance.BaseData.Dame * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.DameBonus;
                    InfomationManager.Instance.LoadData.XuyenGiap -= (SaveManager.Instance.BaseData.XuyenGiap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.XuyenGiapBonus;
                    InfomationManager.Instance.LoadData.Crit -= (SaveManager.Instance.BaseData.Crit * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.CritBonus;
                    break;
                case Equipmenttype.ao:
                    InfomationManager.Instance.LoadData.Hp -= (SaveManager.Instance.BaseData.Hp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.HealBonus;
                    InfomationManager.Instance.LoadData.Giap -= (SaveManager.Instance.BaseData.Giap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.GiapBonus;
                    break;
                case Equipmenttype.gang:
                    InfomationManager.Instance.LoadData.Hp -= (SaveManager.Instance.BaseData.Hp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.HealBonus;
                    InfomationManager.Instance.LoadData.Giap -= (SaveManager.Instance.BaseData.Giap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.GiapBonus;
                    break;
                case Equipmenttype.quan:
                    InfomationManager.Instance.LoadData.Hp -= (SaveManager.Instance.BaseData.Hp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.HealBonus;
                    InfomationManager.Instance.LoadData.Giap -= (SaveManager.Instance.BaseData.Giap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.GiapBonus;
                    break;
                case Equipmenttype.daychuyen:
                    InfomationManager.Instance.LoadData.Mp -= (SaveManager.Instance.BaseData.Mp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.ManaNonus;
                    InfomationManager.Instance.LoadData.XuyenGiap -= (SaveManager.Instance.BaseData.XuyenGiap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.XuyenGiapBonus;
                    break;
                case Equipmenttype.nhan:
                    InfomationManager.Instance.LoadData.Mp -= (SaveManager.Instance.BaseData.Mp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.ManaNonus;
                    InfomationManager.Instance.LoadData.XuyenGiap -= (SaveManager.Instance.BaseData.XuyenGiap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.XuyenGiapBonus;
                    break;
                case Equipmenttype.bua:
                    InfomationManager.Instance.LoadData.Mp -= (SaveManager.Instance.BaseData.Mp * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.ManaNonus;
                    InfomationManager.Instance.LoadData.XuyenGiap -= (SaveManager.Instance.BaseData.XuyenGiap * SaveManager.Instance.BaseData.leve) + ThisItem._TypeUsableItem.UsableItemsItem.XuyenGiapBonus;
                    break;
                default:
                    break;
            }
            InfomationManager.Instance.ShowInfomationCharacter();
            PlayerHeal.Instance.StartHeal();
        }
    }
}
