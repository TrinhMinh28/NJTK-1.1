using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private static ToolTip instance;
    // Getter cho instance
    public static ToolTip Instance
    {
        get
        {
            // Nếu instance đã bị hủy bỏ, tìm lại và gán lại
            if (instance == null)
            {
                instance = FindObjectOfType<ToolTip >();

                // Nếu không tìm thấy, tạo mới instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<ToolTip>();
                    singletonObject.name = "ItemManager (Generated)";
                }

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }
    public TextMeshProUGUI DetailText;

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
            //DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
      //  gameObject.SetActive(false);
    }
    
    public void showTooltips () 
    {
        gameObject.SetActive (true);
    }
   public void hideTooltips ()
    {
        gameObject.SetActive (false);
    }
    public void UpdateTooltip(string _detailTetx)
    {
        DetailText.text = _detailTetx;
    }
    public void Position(Vector2 _Pos)
    {
        transform.localPosition = _Pos;// dùng position cha
      //  transform.position = _Pos;// Dùng position transform làm gốc khoảng cách từ vị trí của đối tượng đến gốc của không gian toàn cục (gốc của scene)
    }
}
