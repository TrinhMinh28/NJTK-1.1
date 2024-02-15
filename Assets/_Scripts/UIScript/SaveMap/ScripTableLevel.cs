using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScripTableLevel : ScriptableObject
{
    public int LevelIndex;
    public int LevelLeftIndex;
    public int LevelRightIndex;
    public int LevelUpIndex;
    public AudioClip MapAudio;
    public GameObject MapEfect; // lá rơi, tuyết  v.v...
    public string LevelName;
    public List<SavedTile> DecorateMap;
    public List<SavedTile> GroundTile;
    public List<SavedTile> GroundTile2;
    public List<SavedTile> WaterTile;
    public List<SavedTile> SnowTile;
    public List<SavedTile> TeleTile;
    public List<SavedTile> LavaTile;
    public List<SavedTile> MobTile; 
    public List<SavedTile> NpcTile;
    //public List<SavedTile> SpecialTerrainTile;// địa hình đặc biệt 
}
[Serializable]
 public class SavedTile
{
    public Vector3Int Position;
    public LevelTitle LevelTile;
    public TileBase Tile;
}