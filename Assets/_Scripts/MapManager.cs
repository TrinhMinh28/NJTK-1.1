using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private GameObject _mainObject;
    [SerializeField] private Tilemap _decorateMap, _groundMap, _groundMap2, _groundTeleMap, _sNowMap, _waTerMap, _LavaMap, _mobMap, _NpcMap;
    [SerializeField] public  ScripTableLevel CurrentMap, LeftMap, RightMap,RightMap2; // Map hiện tại của nhân vật, map bên trái, map bên phải, map trên đầu
    [SerializeField] public  int leverIndex ,LeftMapint, RightMapint, RightMap2int;
    [SerializeField] private  string CurrentMapName ; // Tên map Map hiện tại của nhân vật.
                                                      // public Transform TeleRightTrans, TeleLeftTrans;
    [SerializeField] public static bool ReadyLoadRight;
    [SerializeField]  public static bool ReadyLoadLeft;
    [SerializeField]  public GameObject ThongBaoMap ;
    //  public bool loadmapComplete;
    private static MapManager instance;
    public static MapManager Instance
    {
        get
        {
            // Nếu instance đã bị hủy bỏ, tìm lại và gán lại
            if (instance == null)
            {
                instance = FindObjectOfType<MapManager>();

                // Nếu không tìm thấy, tạo mới instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<MapManager>();
                    singletonObject.name = "MapManager (Generated)";
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

    private void Start()
    {
        _mainObject = GameObject.Find("GridMap");
        _decorateMap = GameManager.Instance.FindObjectInChildren(_mainObject.transform, "Decormap").GetComponent<Tilemap>();
        _groundMap = GameManager.Instance.FindObjectInChildren(_mainObject.transform, "GroundMap").GetComponent<Tilemap>();
        _groundMap2 = GameManager.Instance.FindObjectInChildren(_mainObject.transform, "GroundMap2").GetComponent<Tilemap>();
        _groundTeleMap = GameManager.Instance.FindObjectInChildren(_mainObject.transform, "GroundTele").GetComponent<Tilemap>();
        _sNowMap = GameManager.Instance.FindObjectInChildren(_mainObject.transform, "SnowTile").GetComponent<Tilemap>();
        _waTerMap = GameManager.Instance.FindObjectInChildren(_mainObject.transform, "WaterTile").GetComponent<Tilemap>();
       _LavaMap = GameManager.Instance.FindObjectInChildren(_mainObject.transform, "LavaTile").GetComponent<Tilemap>();
        _mobMap = GameManager.Instance.FindObjectInChildren(_mainObject.transform, "TileMob").GetComponent<Tilemap>();
        _NpcMap = GameManager.Instance.FindObjectInChildren(_mainObject.transform, "NpcMap").GetComponent<Tilemap>();
        WhatMap();
    }

    public void WhatMap()
    {
        if (SaveManager.Instance.LoadComplete == true)
        {
            LoadMaps(SaveManager.Instance.BaseData.leverMapIndex);
        }
        else
        {
            StartCoroutine(WaitLoadGameData());
        }
        // Kiểm tra xem là Load map bao nhiêu
      
    }

    private IEnumerator WaitLoadGameData()
    {
        while (true)
        {
            if (SaveManager.Instance.LoadComplete == true)
            {
                LoadMaps(SaveManager.Instance.BaseData.leverMapIndex);
                break;
            }
            yield return null;
        }
    }

    private void RefeshMap(int _leverIndex)
    {
        leverIndex = _leverIndex;
        CurrentMap = Resources.Load<ScripTableLevel>($"Levels/Level {_leverIndex}");
        LeftMap = Resources.Load<ScripTableLevel>($"Levels/Level {LeftMapint}");
        RightMap = Resources.Load<ScripTableLevel>($"Levels/Level { RightMapint}");
        if (RightMap2int ==100)
        {
            RightMap2 = null;
        }
        else
        {
          RightMap2 = Resources.Load<ScripTableLevel>($"Levels/Level {RightMap2int}");

        }
    }
    public void ClearMap()
    {
        var tilemaps = FindObjectsOfType<Tilemap>(); // Xóa các map thừa.
        foreach (var tilemap in tilemaps)
        {
            // Thực hiện các thao tác trên tilemap tại đây
            tilemap.ClearAllTiles();
        }
    }
    public  void LoadMaps(int LevelIndex) // Có thể thêm các điều kiện để gọi tới từ levelNamaer hoặc nhiều hơn thế
    {
        var level = Resources.Load<ScripTableLevel>($"Levels/Level {LevelIndex}");


        //ScripTableLevel level = AssetDatabase.LoadAssetAtPath<ScripTableLevel>($"Assets/Res/Levels/Level {_leverIndex}.asset");
        if (level == null)
        {
            Debug.LogError($" Level {LevelIndex} Do not exist");
            return;
        }
        ClearMap();
        //LoadGroundTile(level);
        //LoadDecorateTile(level);
        //LoadMobTile(level);
        //LoadNpcTile(level);
        LoadTileWith(level.GroundTile, _groundMap);
        LoadTileWith(level.GroundTile2, _groundMap2);
        LoadTileWith(level.DecorateMap, _decorateMap);
        LoadTileWith(level.MobTile, _mobMap);
        LoadTileWith(level.NpcTile, _NpcMap);
        LoadTileWith(level.SnowTile, _sNowMap);
        LoadTileWith(level.WaterTile, _waTerMap);
        LoadTileWith(level.TeleTile, _groundTeleMap);
        LoadTileWith(level.LavaTile, _LavaMap);

        LeftMapint = level.LevelLeftIndex;
        RightMapint = level.LevelRightIndex;
        RightMap2int = level.LevelUpIndex;
        RefeshMap(LevelIndex);
        ThongBaomap();
    }
    public void ThongBaomap()
    {
        ThongBaoMap.gameObject.SetActive(true);
        TextMeshProUGUI text = GameManager.Instance.FindObjectInChildren(ThongBaoMap.transform, "ThongBaoMapText").GetComponent<TextMeshProUGUI>();
        text.text = "";
        text.text = CurrentMap.LevelName;
        Invoke("TatThongBao", 6f);
    }
    void TatThongBao()
    {
        ThongBaoMap.gameObject.SetActive(false);
    }
    private void LoadTileWith(List<SavedTile> _SavetileList, Tilemap _tileMap)
    {
        //foreach (var levelGround in Level.NpcTile)
        foreach (var levelGround in _SavetileList)
        {
            if (levelGround.LevelTile != null)
            {
                switch (levelGround.LevelTile.Type)
                {
                    case TileType.Ground:
                        // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
                        setTitle(_groundMap, levelGround);
                        break;
                    case TileType.GroundTele:
                        // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
                        setTitle(_groundTeleMap, levelGround);
                        break;
                    case TileType.Mob:
                        // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
                        setTitle(_mobMap, levelGround);
                        break;
                    case TileType.Npc:
                        // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
                        setTitle(_NpcMap, levelGround);
                        break;
                    case TileType.Snow:
                        // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
                        setTitle(_sNowMap, levelGround);
                        break;
                    case TileType.Water:
                        // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
                        setTitle(_waTerMap, levelGround);
                        break;
                    case TileType.Lava:
                        // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
                        setTitle(_LavaMap, levelGround);
                        break;
                    default:
                        break;
                }
            }
            else if (levelGround.Tile != null)
            {
                setBaseTitle(_tileMap, levelGround);
            }
        }
    }

    void setTitle(Tilemap map, SavedTile tile)
    {
        map.SetTile(tile.Position, tile.LevelTile);
    }
    void setBaseTitle(Tilemap map, SavedTile tile)
    {
        map.SetTile(tile.Position, tile.Tile);
    }
    private void Update()
    {
        //RefeshMap(_leverIndex);
    }

}
