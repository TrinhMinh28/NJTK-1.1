using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
#if UNITY_EDITOR
using static UnityEditor.Progress;
#endif

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    // Getter cho instance
    public static ItemManager Instance
    {
        get
        {
            // Nếu instance đã bị hủy bỏ, tìm lại và gán lại
            if (instance == null)
            {
                instance = FindObjectOfType<ItemManager>();

                // Nếu không tìm thấy, tạo mới instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<ItemManager>();
                    singletonObject.name = "ItemManager (Generated)";
                }

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    //[SerializeField] private List<Item> Items = new List<Item>(); 

    //[SerializeField] private List<int> ItemNumbers = new List<int>();
    public List<Item> Items = new List<Item>();

    public List<int> ItemNumbers = new List<int>();
    [SerializeField] private ItemScriptTable loadedItemData;

    // public GameObject[] Slots;
    public SlotStruc[] Slots;

    public Number Slotid;
    [SerializeField] private GameObject _parent;
    public GameObject _titleFrefab;
    public int SpawnBaoNhieuO;
    public GameObject ViewPortThongBao;

    public Item ItemSelected;
    public Number IdSlotSelected; // id của ô đang chọn
    public Item Itemsadd;
    public Item ItemsRemove;
    private string Itemsname;
    private InfomationButton infomationButton;

    // public Dictionary<Item,int> ItemDict = new Dictionary<Item,int>();
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
        //Slotid.id = 0;Inventory Panel
        // _parent = GameObject.FindGameObjectWithTag("InventoryPanel");
        _parent = GameObject.Find("InventoryPanel");
        InstantiateSlot();
        LoadItems();
         DisplayItem();
    }
    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    AddItem(Itemsadd);
        //}
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    RemoveItem(ItemsRemove, 1);
        //}
    }
    void DisplayItem()
    {
        #region
        //for (int i = 0; i < Items.Count; i++)
        //{
        //    Slots[i].Slot.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //    Slots[i].Slot.transform.GetChild(0).GetComponent<Image>().sprite = Items[i].ItemSprite;
        //    //updte text
        //    Slots[i].Slot.transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 1);
        //    Slots[i].Slot.transform.GetChild(1).GetComponent<Text>().text = ItemNumbers[i].ToString();

        //    //Update throw utton
        //    Slots[i].Slot.transform.GetChild(2).gameObject.SetActive(true);
        //}
        #endregion
        for (int i = 0; i < Slots.Length; i++)
        {
            if (i < Items.Count)
            {
                Slots[i].Slot.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Slots[i].Slot.transform.GetChild(1).GetComponent<Image>().sprite = Items[i].ItemSprite;
                //updte text
                Slots[i].Slot.transform.GetChild(2).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                Slots[i].Slot.transform.GetChild(2).GetComponent<Text>().text = ItemNumbers[i].ToString();

                //Update throw utton
                Slots[i].Slot.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                Slots[i].Slot.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                Slots[i].Slot.transform.GetChild(1).GetComponent<Image>().sprite = null;
                //updte text
                Slots[i].Slot.transform.GetChild(2).GetComponent<Text>().color = new Color(1, 1, 1, 0);
                Slots[i].Slot.transform.GetChild(2).GetComponent<Text>().text = null;

                //Update throw utton
                Slots[i].Slot.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        ShowMoney();
    } 
    public void Selected(int _id, Item _items)
    {

        for (int i = 0; i < Slots.Length; i++)
        {
            if (i == _id)
            {
                Slots[i].Slot.transform.GetChild(4).gameObject.SetActive(true);
                ItemSelected = _items;
                IdSlotSelected.id = _id;
            }
            else
            {
                Slots[i].Slot.transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }
    public void AddItem(Item _item)
    {
        // Nếu vật phẩm chưa có
        if (!Items.Contains(_item))
        {
            Items.Add(_item);
            ItemNumbers.Add(1);// thêm 1 cái cho lần nhặt.
        }
        else // nhặt vp đã có
        {
            if (_item.TypeQuantityItem == ItemQuantity.NoQuantity)
            {
                Items.Add(_item);
                ItemNumbers.Add(1);// thêm 1 cái cho lần nhặt.
            }
            else
            {
                Debug.Log("Cong So luong");
                for (int i = 0; i < Items.Count; i++)
                {
                    if (_item == Items[i])
                    {
                        ItemNumbers[i]++;
                    }
                }
            }

        }
        ThongBaoItems(_item);
        RefreshInventory();
        DisplayItem();
    }
    public void UseItem()
    {
        if (ItemSelected.UseItem != UseItemType.NoUse)
        {
            switch (ItemSelected._TypeUsableItem.equipmentType)
            {
                case Equipmenttype.vukhicanchien:
                    Debug.Log("da dung vu khi can chien" + ItemSelected.ItemName);
                    infomationButton = InfomationManager.Instance.CanChienObj.GetComponent<InfomationButton>();
                    infomationButton.UpdateItem(ItemSelected);
                    infomationButton.UpdateSkill();
                    //infomationButton.UpdateChracter();
                    break;
                case Equipmenttype.vukhixachien:

                    infomationButton = InfomationManager.Instance.XaChienObj.GetComponent<InfomationButton>();
                    infomationButton.UpdateItem(ItemSelected);
                    infomationButton.UpdateSkill();
                    break;
                case Equipmenttype.ao:

                    infomationButton = InfomationManager.Instance.AoObj.GetComponent<InfomationButton>();
                    infomationButton.UpdateItem(ItemSelected);
                    infomationButton.UpdateSkill();
                    break;
                case Equipmenttype.gang:
                    infomationButton = InfomationManager.Instance.GangObj.GetComponent<InfomationButton>();
                    infomationButton.UpdateItem(ItemSelected);
                    infomationButton.UpdateSkill();

                    break;
                case Equipmenttype.quan:
                    infomationButton = InfomationManager.Instance.QuanObj.GetComponent<InfomationButton>();
                    infomationButton.UpdateItem(ItemSelected);
                    infomationButton.UpdateSkill();

                    break;
                case Equipmenttype.daychuyen:

                    infomationButton = InfomationManager.Instance.DayChuyenObj.GetComponent<InfomationButton>();
                    infomationButton.UpdateItem(ItemSelected);
                    infomationButton.UpdateSkill();
                    break;
                case Equipmenttype.nhan:

                    infomationButton = InfomationManager.Instance.NhanObj.GetComponent<InfomationButton>();
                    infomationButton.UpdateItem(ItemSelected);
                    infomationButton.UpdateSkill();
                    break;
                case Equipmenttype.bua:

                    infomationButton = InfomationManager.Instance.BuaObj.GetComponent<InfomationButton>();
                    infomationButton.UpdateItem(ItemSelected);
                    infomationButton.UpdateSkill();
                    break;
                case Equipmenttype.tiente:

                    UpdateMoney("add", ItemSelected._TypeUsableItem.UsableItemsItem.YenBonus);

                    break;
                case Equipmenttype.vatphamhoiphuc:

                    StartHealing();

                    break;

                default:
                    break;
            }
            InfomationManager.Instance.UpdateItemsOfInfomationList(ItemSelected);

        }
        else
        {
            GameManager.Instance.ShowThongBao("Không thể sử dụng vật phẩm này.");
        }
    }

    private void StartHealing()
    {
        StartCoroutine(HealingCoroutine());
    }
    private IEnumerator HealingCoroutine()
    {
        // isHealing = true;
        Debug.LogWarning("Máu đang hồi");
        GameManager.Instance.ShowThongBao("Máu đang hồi");
        float currentTime = 0f;

        while (currentTime < ItemSelected._TypeUsableItem.UsableItemsItem.EffectiveTime)
        {
            yield return new WaitForSeconds(0.5f);
            PlayerHeal.Instance.currentHeal += ItemSelected._TypeUsableItem.UsableItemsItem.HealReturn;
            currentTime += 0.5f;
        }

      //  isHealing = false;
    }
    public void RemoveItem(Item _item, int Numdel, int idslot)
    {
        if (Items.Contains(_item))
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (idslot == i)
                {
                    if (_item == Items[i])
                    {

                        ItemNumbers[i] -= Numdel;
                        if (ItemNumbers[i] == 0)
                        {
                            // Remove 
                            Items.Remove(Items[i]);
                            ItemNumbers.Remove(ItemNumbers[i]);
                            RefreshInventory();
                        }

                        Debug.LogWarning("Co the can chinh sua co che tu dong lam moi hanh trang thay vao do gan null cho o slot do");
                    }
                }

            }
        }
        else
        {
            Debug.Log("Item " + _item + " này khong co trong list");
        }
        DisplayItem();
    }

    private void RefreshInventory()
    {
        DestroySlots();
        InstantiateSlot();
        Slotid.id = 0;
    }

    void InstantiateSlot()
    {
        
            SpawnBaoNhieuO = SaveManager.Instance.BaseData.SlotInventory;
        Slots = new SlotStruc[SpawnBaoNhieuO]; // Khởi tạo mảng Slots với độ dài 20
        for (int i = 0; i < SpawnBaoNhieuO; i++)
        {
            GameObject spawnedTitle = Instantiate(_titleFrefab, _parent.transform);
            Slots[i].Slot = spawnedTitle; // Lưu đối tượng clon vào mảng Slots
            Slots[i].index = i; // 
                                //ItemButton.Instance.getButon(spawnedTitle);

        }
    } // Tao ra 20 slot
    // 

    void DestroySlots()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Destroy(Slots[i].Slot); // Hủy đối tượng clone
        }

        Slots = new SlotStruc[0]; // Gán mảng Slots thành một mảng rỗng
    }
    public void LoadItems() // Có thể thêm các điều kiện để gọi tới từ levelNamaer hoặc nhiều hơn thế
    {
        //// Đọc dữ liệu từ file và gán vào Items và ItemNumbers
        this.loadedItemData = Resources.Load<ItemScriptTable>("ItemsSave/ItemDataSave");
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
        #region
        //ItemScriptTable itemscr = new ItemScriptTable();
        //itemscr = ScripttablbeItemUtilitys.LoadItemScriptTable();
        //if (itemscr != null)
        //{
        //    Items.Clear();
        //    foreach (SavedItem savedItem in itemscr._Items)
        //    {
        //        Items.Add(savedItem.Item);
        //    }
        //    ItemNumbers = new List<int>(itemscr._ItemNumbers);
        //}
        #endregion
        // DisplayItem();
    }
    public void SaveItems() // tham số truyền vào để sau dùng
    {
        #region
        // Kiểm tra xem file đã tồn tại hay chưa
        //if (AssetDatabase.LoadAssetAtPath<ItemScriptTable>("Assets/Resources/Items/ItemData.asset") != null)
        //{
        //    Debug.LogWarning("File ItemData.asset đã tồn tại. Không thể ghi đè dữ liệu.");
        //    return;
        //}

        // Tạo một đối tượng ItemScriptTable mới
        #endregion

        SaveItemInventory();
        // ***********************
        // Save luôn cả phần ItemsOfInformation.

        InfomationManager.Instance.SaveItemOfInfo();

        #region
        // ScripttablbeItemUtilitys.SaveItemScriptTable(newItemData);
        //#if UNITY_EDITOR
        //        // Mã chỉ dùng trong chế độ Editor
        //        ScripttablbeItemUtility.SaveLevelFile(newItemData);
        //#else
        //         ScripttablbeItemUtilitys.SaveItemScriptTable(newItemData);

        //#endif
        // Lưu trữ đối tượng newItemData
        #endregion
    }

    public void ResetItems()
    {
        Items.Clear();
        ItemNumbers.Clear();
        SaveItemInventory();
      //  InfomationManager.Instance.ResetItemOfInfo();
    }
    public void ThongBaoItems(Item Item)
    {
        // GameObject obj = GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ViewportThongBao");

        if (GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ViewportThongBao") != null || GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ViewportThongBao(Clone)") != null)
        {
            TextMeshProUGUI Text = GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ThongBaoText").GetComponent<TextMeshProUGUI>();
            Text.text += " , " + Item.ItemName;
        }
        else
        {
            Instantiate(ViewPortThongBao, GameObject.Find("ThongBao").transform);
            TextMeshProUGUI Text = GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ThongBaoText").GetComponent<TextMeshProUGUI>();
            Text.text = "Bạn nhận được " + Item.ItemName;
        }
    }
    private void SaveItemInventory()
    {
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

    }

    public void ShowMoney()
    {
        if (GameObject.Find("TienYen") !=null)
        {
            Text yen;
            yen = GameObject.Find("TienYen").GetComponent<Text>();
            yen.text = SaveManager.Instance.BaseData.Yen.ToString();
        }
        
    }
    public void UpdateMoney(string type, float num)
    {
        switch (type)
        {
            case "add":
                SaveManager.Instance.BaseData.Yen += num;
                break;
            case "subtract":
                SaveManager.Instance.BaseData.Yen -= num;
                break;
            default:
                Debug.LogError("Lỗi type  tiền");
                break;
        }
        ShowMoney();
    }
    private void GetcloneInobject()
    {
        //// Lấy tất cả các đối tượng clone trong cloneContainer
        //ItemButton[] clones = _parent.GetComponentsInChildren<ItemButton>();

        //// Gắn listener cho sự kiện OnButtonClicked của mỗi đối tượng clone
        //foreach (var clone in clones)
        //{
        //   // clone.OnButtonClicked += HandleButtonClicked;
        //}
    }
    public string getItemText(Item _item)
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
    private void HandleButtonClicked(GameObject clone)
    {
        Debug.Log("Button clicked on clone: " + clone.name);
        // Xử lý sự kiện tương ứng với đối tượng clone
    }
}
#if UNITY_EDITOR
public static class ScripttablbeItemUtility
{
    public static void SaveLevelFile(ItemScriptTable newItemData)
    {
        AssetDatabase.CreateAsset(newItemData, $"Assets/Resources/ItemsSave/ItemDataSave.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
#endif

public struct SlotStruc
{
    public GameObject Slot;
    public int index;
}
public struct Number
{
    public int id;
}

