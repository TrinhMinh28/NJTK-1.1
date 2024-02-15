using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using static UnityEditor.Progress;
#endif

public class InfomationManager : MonoBehaviour
{

    private static InfomationManager instance;
    public static InfomationManager Instance
    {
        get
        {
            // Nếu instance đã bị hủy bỏ, tìm lại và gán lại
            if (instance == null)
            {
                instance = FindObjectOfType<InfomationManager>();

                // Nếu không tìm thấy, tạo mới instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<InfomationManager>();
                    singletonObject.name = "InfomationManager (Generated)";
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

    public GameObject InformationText;
    [SerializeField] private InfomationItemScript loadedItemDataOfInformation;
    // public List<int> ItemNumbers = new List<int>();
    public Item ItemSelected;
    [Header("Các ô trang bị")]
    public GameObject AoObj, CanChienObj, DayChuyenObj, NhanObj, BuaObj, XaChienObj, QuanObj, GangObj, ChaRaracterObj;
    List<GameObject> gameObjects;

    public SaveData LoadData;
    private InfomationButton infomationButton;
    [SerializeField] Text TextLevel;

    // Start is called before the first frame update


    [SerializeField] public Item CanChien;
    [SerializeField] public Item XaChien;
    [SerializeField] public Item Ao;
    [SerializeField] public Item Quan;
    [SerializeField] public Item Gang;
    [SerializeField] public Item DayChuyen;
    [SerializeField] public Item Nhan;
    [SerializeField] public Item Bua;
    void Start()
    {
        LoadData = (SaveData)SaveManager.Instance.BaseData.Clone();
        SetListObject();
        InformationText = GameObject.Find("InformationText");
        LoadItemOfInfoamtion();
        ShowInfomationCharacter();
        Invoke("UpdateCharLV",1f);
        //  LoadData = SaveManager.Instance.BaseData;  
    }
    private void OnDestroy()
    {
        LoadData = null;
    }
    void UpdateCharLV()
    {
        TextLevel.text = "lv: "+SaveManager.Instance.BaseData.leve.ToString();
        Invoke("UpdateCharLV", 1f);
    }
    //public void UpdateItemsOfInfomationList(Item items)
    //{
    //    bool Add = false;
    //    if (items != null)
    //    {
    //        if (!ItemsOfInfomation.Contains(items))
    //        {
    //            for (int i = 0; i < ItemsOfInfomation.Count; i++)
    //            {
    //                if (ItemsOfInfomation[i]==null)
    //                {
    //                    ItemsOfInfomation[i] = items;
    //                }
    //                if (ItemsOfInfomation[i]._TypeUsableItem.equipmentType == items._TypeUsableItem.equipmentType)
    //                {
    //                    ItemsOfInfomation[i] = items;
    //                    Add = false;
    //                    break;
    //                }
    //                else
    //                {
    //                    Add = true;
    //                }

    //            }
    //            if (Add == true)
    //            {
    //                ItemsOfInfomation.Add(items);
    //            }
    //        }
    //        else
    //        {
    //            for (int i = 0; i < ItemsOfInfomation.Count; i++)
    //            {
    //                if (ItemsOfInfomation[i] == items)
    //                {
    //                    ItemsOfInfomation[i] = items;
    //                    break;
    //                }
    //            }
    //        }

    //    }
    //}
    public void UpdateItemsOfInfomationList(Item items)
    {
        if (items != null)
        {
            for (int i = 0; i < 9; i++)
            {
                switch (items._TypeUsableItem.equipmentType)
                {
                    case Equipmenttype.vukhicanchien:
                        CanChien = items;
                        continue;
                    case Equipmenttype.vukhixachien:
                        XaChien = items;
                        continue;
                    case Equipmenttype.ao:

                        Ao = items;
                        continue;
                    case Equipmenttype.gang:
                        Gang = items;

                        continue;
                    case Equipmenttype.quan:
                        Quan = items;

                        continue;
                    case Equipmenttype.daychuyen:
                        DayChuyen = items;
                        continue;
                    case Equipmenttype.nhan:

                        Nhan = items;
                        continue;
                    case Equipmenttype.bua:

                        Bua = items;
                        continue;
                    default:
                        continue;
                }
            }

        }
    }
    public void RemoveItemsOfInfomationList(Item items)
    {

        if (items != null)
        {
            for (int i = 0; i < 9; i++)
            {
                switch (items._TypeUsableItem.equipmentType)
                {
                    case Equipmenttype.vukhicanchien:
                        CanChien = null;
                        continue;
                    case Equipmenttype.vukhixachien:
                        XaChien = null;
                        continue;
                    case Equipmenttype.ao:

                        Ao = null;
                        continue;
                    case Equipmenttype.gang:
                        Gang = null;

                        continue;
                    case Equipmenttype.quan:
                        Quan = null;

                        continue;
                    case Equipmenttype.daychuyen:
                        DayChuyen = null;
                        continue;
                    case Equipmenttype.nhan:

                        Nhan = null;
                        continue;
                    case Equipmenttype.bua:

                        Bua = null;
                        continue;
                    default:
                        continue;
                }
            }

        }
    }
    private void SetListObject()
    {
        gameObjects = new List<GameObject>()
    {
        AoObj,
        CanChienObj,
        DayChuyenObj,
        NhanObj,
        BuaObj,
        XaChienObj,
        QuanObj,
        GangObj,
        ChaRaracterObj
    };
    }
    public void ShowInfomationItems()
    {

        TextMeshProUGUI text = InformationText.GetComponent<TextMeshProUGUI>();
        text.text = getInfomationItemText(ItemSelected);
    }
    public void ShowInfomationCharacter()
    {

        TextMeshProUGUI text = InformationText.GetComponent<TextMeshProUGUI>();
        text.text = getInfomationCharacterText(LoadData);
    }
    void DisplayItemOfInformation(InfomationItemScript items)
    {
        if (items != null)
        {

            CanChienObj.GetComponent<InfomationButton>().ThisItem = items._Items != null ? items._Items.CanChien : null;
            XaChienObj.GetComponent<InfomationButton>().ThisItem = items._Items != null ? items._Items.XaChien : null;
            AoObj.GetComponent<InfomationButton>().ThisItem = items._Items != null ? items._Items.Ao : null;
            GangObj.GetComponent<InfomationButton>().ThisItem = items._Items != null ? items._Items.Gang : null;
            QuanObj.GetComponent<InfomationButton>().ThisItem = items._Items != null ? items._Items.Quan : null;
            DayChuyenObj.GetComponent<InfomationButton>().ThisItem = items._Items != null ? items._Items.DayChuyen : null;
            NhanObj.GetComponent<InfomationButton>().ThisItem = items._Items != null ? items._Items.Nhan : null;
            BuaObj.GetComponent<InfomationButton>().ThisItem = items._Items != null ? items._Items.Bua : null;
        }
    }


    public void InfomationItemSelected(GameObject obj, Item item)
    {
        if (obj)
        {
            if (item == null)
            {
                ItemSelected = null;
            }
            else
            {
                ItemSelected = item;
            }

            foreach (GameObject Check in gameObjects)
            {
                if (Check.name == obj.name)
                {
                    Check.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    Check.transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            #region
            //switch (obj.name)
            //{

            //    case var name when name == NhanObj.name:

            //        NhanObj.GetComponent<InfomationButton>().ThisItem = item;
            //        break;
            //    case var name when name == BuaObj.name:

            //        BuaObj.GetComponent<InfomationButton>().ThisItem = item;
            //        break;
            //    default:
            //        break;
            #endregion
        }
    }
    public void LoadItemOfInfoamtion()
    {
        //// Đọc dữ liệu từ file và gán vào Items và ItemNumbers
        loadedItemDataOfInformation = Resources.Load<InfomationItemScript>("ItemsSave/InfomationItemData");
        if (loadedItemDataOfInformation != null)
        {

            CanChien = loadedItemDataOfInformation._Items != null ? loadedItemDataOfInformation._Items.CanChien : null;
            XaChien = loadedItemDataOfInformation._Items != null ? loadedItemDataOfInformation._Items.XaChien : null;
            Ao = loadedItemDataOfInformation._Items != null ? loadedItemDataOfInformation._Items.Ao : null;
            Gang = loadedItemDataOfInformation._Items != null ? loadedItemDataOfInformation._Items.Gang : null;
            Quan = loadedItemDataOfInformation._Items != null ? loadedItemDataOfInformation._Items.Quan : null;
            DayChuyen = loadedItemDataOfInformation._Items != null ? loadedItemDataOfInformation._Items.DayChuyen : null;
            Nhan = loadedItemDataOfInformation._Items != null ? loadedItemDataOfInformation._Items.Nhan : null;
            Bua = loadedItemDataOfInformation._Items != null ? loadedItemDataOfInformation._Items.Bua : null;
            // ItemNumbers = new List<int>(loadedItemDataOfInformation._ItemNumbers);
            DisplayItemOfInformation(loadedItemDataOfInformation);
        }
        else
        {

            Debug.LogError("Failed to load item data.");
        }
    }

    public void SaveItemOfInfo()
    {
        //InfomationItemScript newItemDataOfIn = InfomationItemScript.CreateInstance<InfomationItemScript>();
        //InfomationItemScript newItemDataOfIn = null;


        //newItemDataOfIn._Items.CanChien = CanChien != null ? CanChien : null;
        //newItemDataOfIn._Items.XaChien = XaChien != null ? XaChien : null;
        //newItemDataOfIn._Items.Ao = Ao != null ? Ao : null;
        //newItemDataOfIn._Items.Gang = Gang != null ? Gang : null;
        //newItemDataOfIn._Items.Quan = Quan != null ? Quan : null;
        //newItemDataOfIn._Items.DayChuyen = DayChuyen != null ? DayChuyen : null;
        //newItemDataOfIn._Items.Nhan = Nhan != null ? Nhan : null;
        //newItemDataOfIn._Items.Bua = Bua != null ? Bua : null;


        SavedItemInfo newItemDataOfInf = new SavedItemInfo();


        newItemDataOfInf.CanChien = CanChien != null ? CanChien : null;
        newItemDataOfInf.XaChien = XaChien != null ? XaChien : null;
        newItemDataOfInf.Ao = Ao != null ? Ao : null;
        newItemDataOfInf.Gang = Gang != null ? Gang : null;
        newItemDataOfInf.Quan = Quan != null ? Quan : null;
        newItemDataOfInf.DayChuyen = DayChuyen != null ? DayChuyen : null;
        newItemDataOfInf.Nhan = Nhan != null ? Nhan : null;
        newItemDataOfInf.Bua = Bua != null ? Bua : null;


        if (newItemDataOfInf == null)
        {
            loadedItemDataOfInformation._Items = null;
        }
        else
        {
            Debug.Log("Save Info");
            loadedItemDataOfInformation._Items = newItemDataOfInf;
        }


    }
    public string getInfomationItemText(Item _item)
    {
        if (_item == null)
        {
            return (" <size=50>Chọn vật phẩm để xem thông tin </size> ");
        }
        else
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("<color=black><size=40>Item: </size></color> <color=orange><size=45>{0}</size></color> \n", _item.ItemName);
            stringBuilder.AppendFormat("<color=black><size=40>ItemPrice: </size></color> <color=yellow><size=45>{0}</size></color> \n"
                + "<color=black><size=40>ItemDes: </size></color> <color=red><size=45>{1}</size></color>", _item.ItemPrice, _item.ItemDes);
            return stringBuilder.ToString();
        }

    }
    public string getInfomationCharacterText(SaveData _data)
    {
        if (_data == null)
        {
            return (" <size=50>Chọn vật phẩm để xem thông tin </size> ");
        }
        else
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("<color=white><size=40>Tên NV: </size></color> <color=orange><size=60>{0}</size></color> \n", _data.name);
            stringBuilder.AppendFormat("<color=white><size=40>Cấp Độ: </size></color> <color=yellow><size=45>{0}</size></color> \n"
                + "<color=white><size=40>Thuộc Tính: </size></color> <color=white><size=45>{1}</size></color> \n"
                + "<color=white><size=40>Máu: </size></color> <color=white><size=45>{2}</size></color> \n"
                + "<color=white><size=40>Năng Lượng: </size></color> <color=white><size=45>{3}</size></color> \n"
                + "<color=white><size=40>Tấn Công: </size></color> <color=white><size=45>{4}</size></color> \n"
                + "<color=white><size=40>Giáp: </size></color> <color=white><size=45>{5}</size></color> \n"
                + "<color=white><size=40>Xuyên Giáp: </size></color> <color=white><size=45>{6}</size></color> \n"
                + "<color=white><size=40>Tỉ lệ chí mạng: </size></color> <color=white><size=45>{7}</size></color> \n"



                , _data.leve, _data.CurenCharactor.ToString(), _data.Hp, _data.Mp, _data.Dame, _data.Giap, _data.XuyenGiap, _data.Crit);
            return stringBuilder.ToString();
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateExp(float exp)
    {
        //double requiredExp = 100 * (Math.Pow(SaveManager.Instance.BaseData.leve + 1, 4)) - 100 * (Math.Pow(SaveManager.Instance.BaseData.leve, 4));
        double requiredExp = CalculateRequiredExp(SaveManager.Instance.BaseData.leve, 100, 4);

        if (exp > 0)
        {
            if (exp + SaveManager.Instance.BaseData.exp > requiredExp)
            {
                float expdu = exp - ((float)requiredExp - SaveManager.Instance.BaseData.exp);
                SaveManager.Instance.BaseData.leve += 1;
                SaveManager.Instance.BaseData.exp = 0;
                if (expdu > CalculateRequiredExp(SaveManager.Instance.BaseData.leve, 100, 4))
                {
                    UpdateExp(expdu);
                }
                else
                {
                    SaveManager.Instance.BaseData.exp += expdu;
                }
            }
            else if (exp + SaveManager.Instance.BaseData.exp == requiredExp)
            {
                SaveManager.Instance.BaseData.leve += 1;
                SaveManager.Instance.BaseData.exp = 0;
            }
            else if (exp + SaveManager.Instance.BaseData.exp < requiredExp)
            {
                SaveManager.Instance.BaseData.exp += exp;
            }

        }
        PlayerControler.Instance.ShowExp(exp.ToString());

    }
    double CalculateRequiredExp(float level, float baseExp, float exponent)
    {
        double requiredExp = baseExp * (Math.Pow(level + 1, exponent)) - baseExp * (Math.Pow(level, exponent));
        return requiredExp;
    }
    //public void ResetItemOfInfo()
    //{
    //    loadedItemDataOfInformation._Items = null;
    //    SaveItemOfInfo();
    //}
    public void ThaoTrangBi()
    {
        if (ItemManager.Instance.Items.Count >= ItemManager.Instance.Slots.Length)
        {
            GameManager.Instance.ShowThongBao("Hành trang không đủ chỗ trống");
            return;
        }
        if (ItemSelected.UseItem != UseItemType.NoUse)
        {
            switch (ItemSelected._TypeUsableItem.equipmentType)
            {
                case Equipmenttype.vukhicanchien:
                    Debug.Log("da dung vu khi can chien" + ItemSelected.ItemName);
                    infomationButton = InfomationManager.Instance.CanChienObj.GetComponent<InfomationButton>();
                    infomationButton.ThaoSkill();
                    infomationButton.GiamChiSoCharacter();
                    infomationButton.ThaoItem(ItemSelected);
                    break;
                case Equipmenttype.vukhixachien:

                    infomationButton = InfomationManager.Instance.XaChienObj.GetComponent<InfomationButton>();
                    infomationButton.ThaoSkill();
                    infomationButton.GiamChiSoCharacter();
                    infomationButton.ThaoItem(ItemSelected);
                    break;
                case Equipmenttype.ao:

                    infomationButton = InfomationManager.Instance.AoObj.GetComponent<InfomationButton>();
                    infomationButton.ThaoSkill();
                    infomationButton.GiamChiSoCharacter();
                    infomationButton.ThaoItem(ItemSelected);
                    break;
                case Equipmenttype.gang:
                    infomationButton = InfomationManager.Instance.GangObj.GetComponent<InfomationButton>();
                    infomationButton.ThaoSkill();
                    infomationButton.ThaoItem(ItemSelected);

                    break;
                case Equipmenttype.quan:
                    infomationButton = InfomationManager.Instance.QuanObj.GetComponent<InfomationButton>();
                    infomationButton.ThaoSkill();
                    infomationButton.GiamChiSoCharacter();
                    infomationButton.ThaoItem(ItemSelected);

                    break;
                case Equipmenttype.daychuyen:

                    infomationButton = InfomationManager.Instance.DayChuyenObj.GetComponent<InfomationButton>();
                    infomationButton.ThaoSkill();
                    infomationButton.GiamChiSoCharacter();
                    infomationButton.ThaoItem(ItemSelected);
                    break;
                case Equipmenttype.nhan:

                    infomationButton = InfomationManager.Instance.NhanObj.GetComponent<InfomationButton>();
                    infomationButton.ThaoSkill();
                    infomationButton.GiamChiSoCharacter();
                    infomationButton.ThaoItem(ItemSelected);
                    break;
                case Equipmenttype.bua:

                    infomationButton = InfomationManager.Instance.BuaObj.GetComponent<InfomationButton>();
                    infomationButton.ThaoSkill();
                    infomationButton.GiamChiSoCharacter();
                    infomationButton.ThaoItem(ItemSelected);
                    break;
                default:
                    break;
            }
            InfomationManager.Instance.RemoveItemsOfInfomationList(ItemSelected);

        }
    }
}
