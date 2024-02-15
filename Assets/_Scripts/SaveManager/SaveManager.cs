using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Playables;
using System.IO;
using Spine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Text;

public class SaveManager : MonoBehaviour
{
    private static SaveData saveData;
    //private static SaveData baseData; // Dữ liệu dung để temp để chơi và update.
    public  SaveData BaseData = new SaveData();


    [SerializeField] private SaveData ShowLoadData;// Dữ liệu hien thi
    public bool LoadComplete = false;

    private static SaveManager instance;

    // Getter cho instance
    public static SaveManager Instance
    {
        get
        {
            // Nếu instance đã bị hủy bỏ, tìm lại và gán lại
            if (instance == null)
            {
                instance = FindObjectOfType<SaveManager>();

                // Nếu không tìm thấy, tạo mới instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<SaveManager>();
                    singletonObject.name = "SaveManager (Generated)";
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
        Load();
    }
    // private static string savePath => $"{Application.persistentDataPath}/save.game"; // luu vao user tren may tính
#if UNITY_EDITOR
    //private static string savePath => $"Assets/Game/save.game"; // luu vao foder game 
#endif
    private static string savePaths => Application.persistentDataPath + "/save.game";


    private BinaryFormatter _formatter = new BinaryFormatter();
    private void Start()
    {
        // Lưu game
        CreateFloder();

    }
    private void OnEnable()
    {
        Application.quitting += HandleGameQuit;
    }

    private void OnDisable()
    {
        Application.quitting -= HandleGameQuit;
    }

    private void HandleGameQuit()
    {
        // Xử lý khi người dùng đóng trò chơi
        // Debug.Log("Trò chơi đã được đóng");
        SaveGame();
    }
    private void CreateFloder()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "Game"); // Đường dẫn tới thư mục mới trong ổ C 
        if (!Directory.Exists(folderPath)) // Kiểm tra xem thư mục đã tồn tại chưa
        {
            Directory.CreateDirectory(folderPath); // Tạo thư mục mới nếu chưa tồn tại
        }
    }

    public void Save()
    {
        //if (!File.Exists(savePath)) // Nếu như chưa có dữ liệu thì tạo mới dữ liệu người dùng.
        if (!File.Exists(savePaths)) // Nếu như chưa có dữ liệu thì tạo mới dữ liệu người dùng.
        {
            CreateData();
        }
        saveData = BaseData; // Lưu lại dư liệu khi chơi game
        var json = JsonUtility.ToJson(saveData);
        using (var stream = new FileStream(savePaths, FileMode.Create))
        {
            _formatter.Serialize(stream, MahoaJson(json));
        }
    }

    public void SaveGame()
    {
        Save();
        ItemManager.Instance.SaveItems();
        InfomationManager.Instance.SaveItemOfInfo();


    }
    void CreateData()
    {
        saveData = new SaveData()
        {
            name = "OnGame",
            leve = 1,
            exp = 0,
            Yen = 1000000,
            Xu = 0,
            Hp = 100,
            Mp = 100,
            Dame = 10,
            Giap = 1,
            XuyenGiap = 1,
            Crit = 1,

            leverMapIndex = 0,
            SlotInventory = 45,
            music = true,
            sound = true,
            Vibrate = true,
            CurenCharactor = CharacterPype.ClassHoa,
            UnlockCharactors = new List<CharacterPype>() { CharacterPype.ClassHoa },
            LocationCharacter = new Vector3(45f, 4f, 0)
        };
        BaseData = saveData;
    }
    void Load()
    {
        if (!File.Exists(savePaths)) // Nếu như chưa có dữ liệu thì tạo mới dữ liệu người dùng.
        {
            Debug.LogError("Chưa có dữ liệu ng dùng");
            Save();
            LoadComplete = false;
            //Save();
        }
        using (var stream = new FileStream(savePaths, FileMode.Open))
        {
            var data = (string)_formatter.Deserialize(stream);
            BaseData = JsonUtility.FromJson<SaveData>(MahoaJson(data)); // mã hóa ngược lại đoạn dữ liệu
            ShowLoadData = BaseData;

            LoadComplete = true;
        }
    }
#if UNITY_EDITOR
    // Mã chỉ dùng trong chế độ Editor
    [MenuItem("Deverloper/Delete Saved Game")] // Tạo một Menu trên thanh công cụ của unity. Mặc định sẽ gọi hàm ngay dưới nó là DeleteSave.
#endif
    public static void DeleteSave()
    {
        if (File.Exists(savePaths))
        {
            File.Delete(savePaths);
            File.Delete(savePaths + ".meta");
        }
    }
#if UNITY_EDITOR
    // Mã chỉ dùng trong chế độ Editor
    [MenuItem("Deverloper/ Saved Game")]
#endif
    public static void SaveGameDev()
    {

    }

    public void DeleteGame()
    {
        if (File.Exists(savePaths))
        {
            File.Delete(savePaths);
            File.Delete(savePaths + ".meta");
        }
        MissionManager.Instance.ResetMission();
        ItemManager.Instance.ResetItems();
    }
    public static string MahoaJson(string jsontxt) // Mã hóa XOR với 129  và 1 lần nữa với 129 để giải mã.
    {
        var insb = new StringBuilder(jsontxt);
        var outSb = new StringBuilder(jsontxt.Length);
        for (var i = 0; i < jsontxt.Length; i++)
        {
            var c = insb[i];
            c = (char)(c ^ 129);
            outSb.Append(c);

        }
        return outSb.ToString();
    }


}
public enum CharacterPype
{
    ClassHoa,
    ClassBang,
    ClassPhong,
}
[Serializable]
public class SaveData
{
    public CharacterPype CurenCharactor;
    public List<CharacterPype> UnlockCharactors;
    public string name;
    public int leve;
    public float exp;
    public float Yen;
    public float Xu;
    public float Hp;
    public float Mp;
    public float Dame;
    public float Giap; // Giáp
    public float XuyenGiap;
    public float Crit;


    public int SlotInventory;
    public int leverMapIndex; // Map hien tai
    public bool sound;
    public bool music;
    public bool Vibrate;

    public Vector3 LocationCharacter;
    public object Clone()
    {
        return MemberwiseClone();
    }
    // Copy Constructor
   
}