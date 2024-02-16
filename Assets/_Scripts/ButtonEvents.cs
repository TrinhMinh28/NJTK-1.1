using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
#if UNITY_EDITOR
using static UnityEditor.Progress;
#endif

public class ButtonEvents : MonoBehaviour
{
    public static ButtonEvents instance;
    [SerializeField] private Animator _MenubuttonAnimtor;
    [SerializeField] private Animator _InventoryAnim;
    [SerializeField] private GameObject _InventoryObject;
    [SerializeField] private GameObject _MissionObject;
    [SerializeField] private GameObject _InventoryNpcObject;
    [SerializeField] private GameObject _MapObject;
    [SerializeField] private GameObject _Information;
    [SerializeField] private GameObject _Items;
    [SerializeField] private Text textTitle;
    // Nếu muốn bắt sự kiện cho nút thì

    public static ButtonEvents Instance
    {
        get
        {
            // Nếu instance đã bị hủy bỏ, tìm lại và gán lại
            if (instance == null)
            {
                instance = FindObjectOfType<ButtonEvents>();

                // Nếu không tìm thấy, tạo mới instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<ButtonEvents>();
                    singletonObject.name = "ButtonEvents (Generated)";
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

    // Các phương thức và thuộc tính của GameManager
    // ...
    private void Start()
    {
        instance = this;
        _InventoryObject = GameObject.Find("Inventory");
        _MissionObject = GameObject.Find("MissionIventory");
        _InventoryNpcObject = GameObject.Find("BuyItemForNpc");
        _MapObject = GameObject.Find("MapIventory");
        _Information = GameObject.Find("InformationIventory");
        _Items = GameObject.Find("Items");
        _MenubuttonAnimtor = GameObject.Find("MenuBUtton").GetComponent<Animator>();
        _InventoryAnim = GameObject.Find("Inventory Background Image").GetComponent<Animator>();
        textTitle = GameObject.Find("Text Title Inventory").GetComponent<Text>();


    }

    #region
    //void FindAnimator(Animator _Taget,string _name) // 1 pần tử có tên 
    //{

    //     _Taget = GameObject.FindObjectsOfType<Animator>()
    //        .FirstOrDefault(animator => animator != null && animator.name == _name);
    //}
    //void FindListAnimator(string _name) // danh sách các animator cùng tên
    //{
    //    Animator[] animators = GameObject.FindObjectsOfType<Animator>()
    // .Where(animator => animator != null && animator.name == "AnimatorName")
    // .ToArray();

    //    foreach (Animator animator in animators)
    //    {
    //        // Tìm thấy Animator với tên "AnimatorName"
    //    }
    //}
    #endregion
    private IEnumerator OpenInventoryCoroutine()
    {
        SetTringerAnimTor();
        // Chờ đợi cho animation hoàn thành
        yield return new WaitForSeconds(1f);
        // Đặt Time.timeScale thành 0.0f
        // Time.timeScale = 0.0f;// đóng băng thời gian
        GameManager.Instance.PauseGame(true);// pause game.
    }
    private IEnumerator OpenInventoryCoroutineNoMenu()
    {
        _InventoryAnim.SetTrigger("PlayModeCheck");
        // Chờ đợi cho animation hoàn thành
        yield return new WaitForSeconds(1f);
        // Đặt Time.timeScale thành 0.0f
        // Time.timeScale = 0.0f;// đóng băng thời gian
        GameManager.Instance.PauseGame(true);// pause game.
    }
    private IEnumerator OpenInventoryNpcCoroutine()
    {
        _InventoryAnim.SetTrigger("PlayModeCheck");
        // Chờ đợi cho animation hoàn thành
        yield return new WaitForSeconds(1f);
        // Đặt Time.timeScale thành 0.0f
        GameManager.Instance.PauseGame(true);// pause game.
    }
    public void OpenInventory()
    {
        _Items.SetActive(true);
        _InventoryObject.SetActive(true);
        _MissionObject.SetActive(false);
        _InventoryNpcObject.SetActive(false);
        _MapObject.SetActive(false);
        _Information.SetActive(false);
        textTitle.text = "Hành Trang";
        StartCoroutine(OpenInventoryCoroutine());
    }
    public void OpenMission()
    {
        textTitle.text = "Nhiệm Vụ";
        _Items.SetActive(false);
       // _InventoryObject.SetActive(false);
        _MissionObject.SetActive(true);
      //  _InventoryNpcObject.SetActive(false);
        _MapObject.SetActive(false);
        _Information.SetActive(false);
        StartCoroutine(OpenInventoryCoroutine());
        MissionManager.Instance.ShowMission();
    }
    public void OpenMap()
    {
        _Items.SetActive(false);
        textTitle.text = "Bản đồ";
      //  _InventoryObject.SetActive(false);
        _MissionObject.SetActive(false);
      //  _InventoryNpcObject.SetActive(false);
        _MapObject.SetActive(true);
        _Information.SetActive(false);
        StartCoroutine(OpenInventoryCoroutine());
        MissionManager.Instance.ShowMission();
    }
    public void OpenNpcInventory()
    {
        _Items.SetActive(true);
        _InventoryObject.SetActive(false);
        _MissionObject.SetActive(false);
        _InventoryNpcObject.SetActive(true);
        _MapObject.SetActive(false);
        _Information.SetActive(false);
        StartCoroutine(OpenInventoryNpcCoroutine());
        textTitle.text = "Gian Hàng";
        //  NPCManager.Instance. ();
    }

    public void OpenInformation()
    {
        _Items.SetActive(false);
        textTitle.text = "Thông tin";
        //_InventoryObject.SetActive(false);
        _MissionObject.SetActive(false);
      //  _InventoryNpcObject.SetActive(false);
        _MapObject.SetActive(false);
        _Information.SetActive(true);
        StartCoroutine(OpenInventoryCoroutine());
     //   MissionManager.Instance.ShowMission();
    }
    public void OpenInformationWimg()
    {
        _Items.SetActive(false);
        textTitle.text = "Thông tin";
     //   _InventoryObject.SetActive(false);
        _MissionObject.SetActive(false);
      //  _InventoryNpcObject.SetActive(false);
        _MapObject.SetActive(false);
        _Information.SetActive(true);
        StartCoroutine(OpenInventoryCoroutineNoMenu());
     //   MissionManager.Instance.ShowMission();
    }
    public void TalkingNpc()
    {
        NPCManager.Instance.NpcTakling();
    }
    public void NpcBuying()
    {
        NPCManager.Instance.NpcBuying();
    }
    public void AccepMission()
    {
        MissionManager.Instance.UpdateMission(MissionManager.Instance.tempMission, "Accept"); 
    }
    public void RejectMission()
    {
        MissionManager.Instance.UpdateMission(MissionManager.Instance.tempMission, "Reject"); 
    }
    public void GoPractice()
    {
        MissionManager.Instance.UpdateMission(MissionManager.Instance.tempMission, "Reject"); 
    }
  
    public void CloseInventory()
    {
        _InventoryAnim.SetTrigger("PlayModeCheck");
        //  Time.timeScale = 1.0f;
        GameManager.Instance.PauseGame(false); // ko pause game 
    }

    public void ClickMap()
    {
        SetTringerAnimTor();
        Debug.Log("Đã nhấn vào Map ");
    }
    public void NewGame(string SeaneName)
    {
        Debug.Log("Đã nhấn vào New game");
        //LoadSceneButton.Instance.ChangeScene("GridItem");
        LeverManager.Instance.LoadScence(SeaneName);
    }
    void SetTringerAnimTor()
    {
        _MenubuttonAnimtor.SetTrigger("PlayModeCheck");
        _InventoryAnim.SetTrigger("PlayModeCheck");
    }
}
