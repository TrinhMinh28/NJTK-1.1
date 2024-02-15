using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
#if UNITY_EDITOR
// Mã chỉ dùng trong chế độ Editor
using UnityEditor.VersionControl;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;
using static Cinemachine.DocumentationSortingAttribute;

public class TileMapManager : MonoBehaviour
{

    [SerializeField] private Tilemap _decorateMap, _groundMap , _groundMap2, _groundTeleMap, _sNowMap,_waTerMap,_LavaMap, _mobMap,_NpcMap;
    [SerializeField] private int _leverIndex;
    [SerializeField] private int _leverLeftIndex;
    [SerializeField] private int _leverRightIndex;
    [SerializeField] private int _leverUpIndex;
    [SerializeField] private string _leverName; // Tên map
    [SerializeField] private AudioClip _leverAudioName; // Tên map
    [SerializeField] private GameObject _leverEfect; // Tên map
    public void SaveMap()
    {
        var newlevel = ScriptableObject.CreateInstance<ScripTableLevel>();
        newlevel.MapAudio = _leverAudioName;
        newlevel.MapEfect = _leverEfect;
        newlevel.LevelIndex = _leverIndex;
        newlevel.LevelLeftIndex = _leverLeftIndex; // map cạnh cửa trái
        newlevel.LevelRightIndex = _leverRightIndex; // Mp cửa phải
        newlevel.LevelUpIndex = _leverUpIndex;// map cửa trên
        newlevel.LevelName = _leverName; // tên map
        newlevel.name = $"Level {_leverIndex}"; // Tên khi lưu
        newlevel.DecorateMap = GetTilesFromMap(_decorateMap).ToList();
       // newlevel.GroundTile.AddRange(GetTilesFromMap(_groundMap).ToList());
        newlevel.GroundTile = GetTilesFromMap(_groundMap).ToList();
        newlevel.GroundTile2 = GetTilesFromMap(_groundMap2).ToList();
        newlevel.TeleTile = GetTilesFromMap(_groundTeleMap).ToList();
        newlevel.SnowTile = GetTilesFromMap(_sNowMap).ToList();
        newlevel.WaterTile = GetTilesFromMap(_waTerMap).ToList();
        newlevel.LavaTile = GetTilesFromMap(_LavaMap).ToList();
        newlevel.MobTile = GetTilesFromMap(_mobMap).ToList();
        newlevel.NpcTile = GetTilesFromMap(_NpcMap).ToList();

        var json = JsonUtility.ToJson(newlevel); // Debug dòng bày để biết chuỗi sẽ được mã hóa thành Json ntn. Nếu muốn đưa vào csdl thì.
                                                 //https://www.youtube.com/watch?v=TeEWLC-QKjw&list=PLKeKudbESdcyRmaa30z-vDBWVV4iz29LB&index=31 Xem lại để biết thêm về nén Json.
#if UNITY_EDITOR
        ScripttablbeObjectUyility. SaveLevelFile(newlevel);
#endif
        //newlevel.GroundTile =
        IEnumerable<SavedTile> GetTilesFromMap(Tilemap tilemap)
        {
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                if (tilemap.HasTile(pos))
                {
                    var tileBase = tilemap.GetTile(pos); // đây sẽ lấy toàn bộ tilebase bao gồm cả k dùng đến.
                    if (tileBase is LevelTitle LevelTile)
                    {
                        yield return new SavedTile()
                        {
                            Position = pos,
                            LevelTile = LevelTile,
                        };
                    }
                    else if (tileBase is Tile tile) // Lấy tile thôi
                    {  
                        yield return new SavedTile()
                        {
                            Position = pos,
                            Tile = tile,
                        };
                    }
                }
            }
        }
    }
    public void ClearMap()
    {
        var tilemaps = FindObjectsOfType<Tilemap>();
        foreach (var tilemap in tilemaps)
        {
            // Thực hiện các thao tác trên tilemap tại đây
            tilemap.ClearAllTiles();
        }
    }
    public void LoadMap() // Có thể thêm các điều kiện để gọi tới từ levelNamaer hoặc nhiều hơn thế
    {
        var level = Resources.Load<ScripTableLevel>($"Levels/Level {_leverIndex}");
        //ScripTableLevel level = AssetDatabase.LoadAssetAtPath<ScripTableLevel>($"Assets/Res/Levels/Level {_leverIndex}.asset");
        if (level == null )
        {
            Debug.LogError($" Level { _leverIndex} Do not exist");
            return;
        }
        ClearMap();
        #region
        //LoadGroundTile(level);
        //LoadDecorateTile(level);
        //LoadMobTile(level);
        //LoadNpcTile(level);
        #endregion
        LoadTileWith(level.GroundTile,_groundMap);
        LoadTileWith(level.GroundTile2, _groundMap2);
        LoadTileWith(level.DecorateMap,_decorateMap);
        LoadTileWith(level.MobTile,_mobMap);
        LoadTileWith(level.NpcTile,_NpcMap);
        LoadTileWith(level.SnowTile,_sNowMap);
        LoadTileWith(level.WaterTile,_waTerMap);
        LoadTileWith(level.TeleTile,_groundTeleMap);
        LoadTileWith(level.LavaTile,_LavaMap);
    }
    #region
    //private void LoadDecorateTile(ScripTableLevel Level)
    //{
    //    foreach (var levelGround in Level.DecorateMap)
    //    {
    //        if (levelGround.Tile != null)
    //        {
    //            setBaseTitle(_decorateMap, levelGround);
    //        }
    //    }
    //}
    //private void LoadGroundTile(ScripTableLevel Level)
    //{
    //    foreach (var levelGround in Level.GroundTile)
    //    {
    //        if (levelGround.LevelTile!= null)
    //        {
    //            switch (levelGround.LevelTile.Type)
    //            {
    //                case TileType.Ground:
    //                    // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
    //                    setTitle(_groundMap, levelGround);
    //                    break;
    //                case TileType.GroundTele:
    //                    // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
    //                    setTitle(_groundTeleMap, levelGround);
    //                    break;
    //                case TileType.Snow:
    //                    // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
    //                    setTitle(_sNowMap, levelGround);
    //                    break;
    //                case TileType.Water:
    //                    // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
    //                    setTitle(_waTerMap, levelGround);
    //                    break;
    //                case TileType.Lava:
    //                    // nếu có nhiều loại địahình cùng thuộc tính thì có thể xếp chồng lên 
    //                    setTitle(_LavaMap, levelGround);
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //        else if (levelGround.Tile != null)
    //        {
    //            setBaseTitle(_groundMap, levelGround);
    //        }
    //    }
    //}

    //private void LoadMobTile(ScripTableLevel Level)
    //{
    //    foreach (var levelGround in Level.MobTile)
    //    {
    //        switch (levelGround.LevelTile.Type)
    //        {

    //            case TileType.Mob:
    //                setTitle(_mobMap, levelGround);
    //                break;
    //                // Game manager add them Enemi hoac 1 cai gif do
    //            default:
    //                break;
    //        }
    //    }
    //}
    //private void LoadNpcTile(ScripTableLevel Level)
    //{
    //    foreach (var levelGround in Level.NpcTile)
    //    {
    //        switch (levelGround.LevelTile.Type)
    //        {

    //            case TileType.Npc:
    //                setTitle(_NpcMap, levelGround);
    //                break;
    //            // Game manager add them Enemi hoac 1 cai gif do
    //            default:
    //                break;
    //        }
    //    }
    //}
    #endregion
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

    void setTitle(Tilemap map , SavedTile tile)
    {
        map.SetTile(tile.Position, tile.LevelTile);
    }
    void setBaseTitle(Tilemap map , SavedTile tile)
    {
        map.SetTile(tile.Position, tile.Tile);
    }
}
#if UNITY_EDITOR
public static class ScripttablbeObjectUyility
{
    public static void SaveLevelFile (ScripTableLevel level)
    {
        AssetDatabase.CreateAsset(level,$"Assets/Resources/Levels/{level.name}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

#endif