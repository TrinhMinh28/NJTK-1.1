using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Level Tile ", menuName ="2D / New Tiles / Level Tile" ,order = 1)]
public class LevelTitle : Tile 
{

    public TileType Type;   
    
}
[Serializable]
public enum TileType
{
    //Ground 
    Ground = 0, // co the bo sung cac moi truong khac nhu dung nham hoac van van.
    Water = 1,
    Snow = 2,
    Lava = 3,
    GroundTele = 4,
    // Thật ra chỉ cần 1 loại Type còn nếu muốn chỉnh lại các chỉ số thì Gắn object vào cho đối tượng Scripttable

    //Mob 
    Mob = 100, // 100 chi la layer uoc tinh se dung toi
    //Ocsen = 101,
    //Ech = 102,

    //Npc 
    Npc= 1000

}