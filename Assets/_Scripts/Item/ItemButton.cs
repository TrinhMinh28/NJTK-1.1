using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using System.Text;

public class ItemButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler//,IPointerClickHandler,IPointerUpHandler, IPointerExitHandler,
{
    public static ItemButton Instance;
    // chưa dùng đến.
    [SerializeField] private Item thisItem;
    public int IDofslot;

   // public Button button;
   [SerializeField] private ToolTip toolTipImage;

    public delegate void ButtonClickedDelegate(GameObject clone);
    public static event ButtonClickedDelegate OnButtonClicked;

    private Vector2 poSition;
    private void Start()
    {
        ItemManager.Instance.Slotid.id += 1; // Mỗi lần start thì sẽ cộng lên vs 1 
        IDofslot = ItemManager.Instance.Slotid.id - 1;
        getthisItem();
        toolTipImage = ToolTip.Instance;

    }
    private Item getthisItem() // lấy vị trí items cần xóa
    {
        Item item = null ;
        for (int i = 0; i < ItemManager.Instance.Items.Count; i++)
        {
            if (IDofslot == i)
            {
                thisItem = ItemManager.Instance.Items[i];
                item = ItemManager.Instance.Items[i];
                break;
            }
        }
        return item;
    }
    //public void getButon()
    //{
    //    Button buttons = button.GetComponent<Button>();
    //    button.onClick.AddListener(TriggerButtonClickedEvent);
    //}

    //private void TriggerButtonClickedEvent()
    //{
    //    if (OnButtonClicked != null)
    //    {
    //        OnButtonClicked(gameObject);
    //    }
    //}
    public void closeButton()
    {
        ItemManager.Instance.RemoveItem(getthisItem(), 1,IDofslot); // Bo đi 1 Item, 
    }
   
    public void OnPointerEnter(PointerEventData eventData)
    {

        if (thisItem != null)
        {
            //Debug.Log("Mouse entered: " + gameObject.name);
            toolTipImage.showTooltips();
            // toolTipImage.UpdateTooltip(thisItem.ItemDes);
            toolTipImage.UpdateTooltip(ItemManager.Instance.getItemText(thisItem));
            #region // Nếu muốn ô info đi theo chuột thì dùng code dưới.
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").transform as RectTransform, Input.mousePosition, null, out poSition);
            //toolTipImage.Position(poSition);
            #endregion
        }
        else
        {
            // toolTipImage.hideTooltips();
            //toolTipImage.UpdateTooltip(getItemText(thisItem));
            toolTipImage.UpdateTooltip(ItemManager.Instance.getItemText(ItemManager.Instance.ItemSelected));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject useItem = GameObject.Find("UseItem");
        Text text = GameManager.Instance.FindObjectInChildren(useItem.transform, "TextUse").GetComponent<Text>();
        ItemManager.Instance.Selected(IDofslot, thisItem);
        toolTipImage.UpdateTooltip(ItemManager.Instance. getItemText(thisItem));
        if (ItemManager.Instance.ItemSelected != null)
        {
            //  UseItem.Instance.gameObject.SetActive(true);
            useItem.SetActive(true);
            text.text = "Sử dụng";
        }
        else
        {
            text.text = "Xắp xếp";
        }

    }
    #region
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    throw new System.NotImplementedException();
    //}


    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    throw new System.NotImplementedException();
    //} 
    #endregion
   

}
