
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    // Getter cho instance
    public static GameManager Instance
    {
        get
        {
            // Nếu instance đã bị hủy bỏ, tìm lại và gán lại
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                // Nếu không tìm thấy, tạo mới instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GameManager>();
                    singletonObject.name = "GameManager (Generated)";
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
        //loadManager();
        //checkManager();
    }
    void loadManager()
    {
        LoadSaveManager(0);
        loadMapManager(0);
        loadNpcManager(0);
        loadMissionManager(0);
        LoadItemManager(0);
        LoadButtonEven (0);
    }
    void checkManager()
    {
        LoadSaveManager(1);
        loadMapManager(1);
        loadNpcManager(1);
        loadMissionManager(1);
        LoadItemManager(1);
        LoadButtonEven(1);
    }

    private void LoadButtonEven(int indexFocus)
    {
        if (indexFocus == 0)
        {
            ButtonEvents _taget = FindObjectOfType<ButtonEvents>();
            if (_taget == null)
            {
                ButtonEvents.Instance.GetInstanceID();
               
            }
        }
        else if (indexFocus == 1)
        {
            ButtonEvents _taget = FindObjectOfType<ButtonEvents>();
            if (_taget == null)
            {
                Debug.LogError("Chưa có load được " + _taget + "Game có thể lỗi !");
            }
        }
       
    }

    private void loadMissionManager(int indexFocus)
    {
        if (indexFocus == 0)
        {
            MissionManager _taget = FindObjectOfType<MissionManager>();
            if (_taget == null)
            {
                MissionManager.Instance.GetInstanceID();
            }
        }
        else if (indexFocus == 1)
        {
            MissionManager _taget = FindObjectOfType<MissionManager>();
            if (_taget == null)
            {
                Debug.LogError("Chưa có load được " + _taget + "Game có thể lỗi !");
            }
        }
    }

    private void LoadSaveManager(int indexFocus)
    {
        if (indexFocus == 0)
        {
            SaveManager _taget = FindObjectOfType<SaveManager>();
            if (_taget == null)
            {
                SaveManager.Instance.GetInstanceID();
            }
        }
        else if (indexFocus == 1)
        {
            SaveManager _taget = FindObjectOfType<SaveManager>();
            if (_taget == null)
            {
                Debug.LogError("Chưa có load được " + _taget + "Game có thể lỗi !");
            }
        }
    }

    private void LoadItemManager(int indexFocus)
    {
        if (indexFocus == 0)
        {
            ItemManager _taget = FindObjectOfType<ItemManager>();
            if (_taget == null)
            {
                ItemManager.Instance.GetInstanceID();
            }
        }
        else if (indexFocus == 1)
        {
            ItemManager _taget = FindObjectOfType<ItemManager>();
            if (_taget == null)
            {
                Debug.LogError("Chưa có load được " + _taget + "Game có thể lỗi !");
            }
        }
    }

    private void loadNpcManager(int indexFocus)
    {
        if (indexFocus == 0)
        {
            NPCManager _taget = FindObjectOfType<NPCManager>();
            if (_taget == null)
            {
                NPCManager.Instance.GetInstanceID();
            }
        }
        else if (indexFocus == 1)
        {
            NPCManager _taget = FindObjectOfType<NPCManager>();
            if (_taget == null)
            {
                Debug.LogError("Chưa có load được " + _taget + "Game có thể lỗi !");
            }
        }
    }
    private void loadMapManager(int indexFocus)
    {
        if (indexFocus == 0)
        {
            MapManager _taget = FindObjectOfType<MapManager>();
            if (_taget == null)
            {
                MapManager.Instance.GetInstanceID();
            }
        }
        else if (indexFocus == 1)
        {
            MapManager _taget = FindObjectOfType<MapManager>();
            if (_taget == null)
            {
                Debug.LogError("Chưa có load được " + _taget + "Game có thể lỗi !");
            }
        }
    }

    public void PauseGame(bool Tactive)
    {
        if (Tactive)
        {
            Time.timeScale = 0.0f;// đóng băng thời gian

        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void ShowThongBao(string String)
    {
        if (GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ViewportThongBao") != null || GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ViewportThongBao(Clone)") != null)
        {
            TextMeshProUGUI Text = GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ThongBaoText").GetComponent<TextMeshProUGUI>();
            Text.text = String;
        }
        else
        {
            Instantiate(ItemManager.Instance.ViewPortThongBao, GameObject.Find("ThongBao").transform);
            TextMeshProUGUI Text = GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ThongBaoText").GetComponent<TextMeshProUGUI>();
            Text.text = String;
        }
    }

    //public void CheckMission(GameObject _tagetObject)
    //{
    //    GameObject[] children = _tagetObject.GetComponentsInChildren<Transform>(true)
    //   .Where(t => t.name == targetObjectName)
    //   .Select(t => t.gameObject)
    //   .ToArray();

    //    if (children.Length > 0)
    //    {
    //        GameObject buttonMission = children[0];
    //        // Sử dụng buttonMission
    //    }
    //}
    //public void GetInchildrent(GameObject _tagetObject,Type _TagetType)
    //{
    //    GameObject[] children = _tagetObject.GetComponentsInChildren<_TagetType>(true);
    //    List<GameObject> matchingObjects = new List<GameObject>();

    //    foreach (GameObject child in children)
    //    {
    //        if (child.name == targetObjectName)
    //        {
    //            matchingObjects.Add(child);
    //        }
    //    }

    //    if (matchingObjects.Count > 0)
    //    {
    //        GameObject buttonMission = matchingObjects[0];
    //        // Sử dụng buttonMission
    //    }
    //}
    public GameObject FindObjectInChildren(Transform parentTransform, string objectName)
    {
        foreach (Transform childTransform in parentTransform)
        {
            if (childTransform.name == objectName)
            {
                return childTransform.gameObject;
            }

            GameObject foundObject = FindObjectInChildren(childTransform, objectName);
            if (foundObject != null)
            {
                return foundObject;
            }
        }

        return null;
    }
}
