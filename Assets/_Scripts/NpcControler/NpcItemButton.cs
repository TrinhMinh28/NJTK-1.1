using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public class NpcItemButton : MonoBehaviour , IPointerEnterHandler, IPointerDownHandler
{
    public static NpcItemButton Instance;
    [SerializeField] private Item thisItemNpc;
    public int IDofslotNpc;
    [SerializeField] private ToolTip toolTipImageNpc;

    private void Start()
    {
        NPCManager.Instance.SlotNpcid.id += 1; // Mỗi lần start thì sẽ cộng lên vs 1 
        IDofslotNpc = NPCManager.Instance.SlotNpcid.id - 1;
        getthisItem();
        toolTipImageNpc = ToolTip.Instance;

    }
    private Item getthisItem() // lấy vị trí items cần mua
    {
        for (int i = 0; i < NPCManager.Instance.thisNpcData.ItemForNpc.ItemsforNpc.Count; i++)
        {
            if (IDofslotNpc == i)
            {
                thisItemNpc = NPCManager.Instance.thisNpcData.ItemForNpc.ItemsforNpc[i];
                break;
            }
        }
        return thisItemNpc;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        if (thisItemNpc != null)
        {
            //Debug.Log("Mouse entered: " + gameObject.name);
            toolTipImageNpc.showTooltips();
            // toolTipImage.UpdateTooltip(thisItem.ItemDes);
            toolTipImageNpc.UpdateTooltip(getItemNpcText(thisItemNpc));
            #region // Nếu muốn ô info đi theo chuột thì dùng code dưới.
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").transform as RectTransform, Input.mousePosition, null, out poSition);
            //toolTipImage.Position(poSition);
            #endregion
        }
        else
        {
            // toolTipImage.hideTooltips();
            //toolTipImage.UpdateTooltip(getItemText(thisItem));
            toolTipImageNpc.UpdateTooltip(getItemNpcText(NPCManager.Instance.ItemSelectedForNpc));
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject useItemNpc = GameObject.Find("UseItemNpc");
        Text text = GameManager.Instance.FindObjectInChildren(useItemNpc.transform, "TextUse").GetComponent<Text>();
        NPCManager.Instance.Selected(IDofslotNpc, thisItemNpc);
        toolTipImageNpc.UpdateTooltip(getItemNpcText(thisItemNpc));
        if (NPCManager.Instance.ItemSelectedForNpc != null)
        {
          //  UseItem.Instance.gameObject.SetActive(true);
            useItemNpc.SetActive(true);
            text.text = "Mua";
        }
        else
        {
            text.text = "Làm mới";
        }

    }
    private string getItemNpcText(Item _item)
    {
        if (_item == null)
        {
            return (" <size=50>Chọn vật phẩm để xem thông tin </size> ");
        }
        else
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("<color=black><size=40>Item: </size></color> <color=orange><size=45>{0}</size></color> \n", _item.ItemName);
            stringBuilder.AppendFormat("<color=black><size=40>ItemBuy: </size></color> <color=yellow><size=45>{0}</size></color> \n"
                + "<color=black><size=40>ItemDes: </size></color> <color=red><size=45>{1}</size></color>", _item.ItemBuy, _item.ItemDes);
            return stringBuilder.ToString();
        }

    }
}
