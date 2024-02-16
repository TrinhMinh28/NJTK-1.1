using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "2D / Enemy /New Enemys ",order =0)]
public class CreateEnemy : ScriptableObject
{
    public int EnemyIndex;
    public string EnemyName;
    public float EnemyLevel;
    public string EnemyDes; // mo ta ve quai vat
    public EnemyItems _EnemyItem; // VatPhamroi ra
    public float HpEnemy;// mau quai
    public float DameEnemy;// Dame quai
    public float ArmorEnemy;// Giáp quai
    public float nextDamerate; // thoi gian tấn công
    public Sprite EnemySprite;// Tạn thời chưa dùng đến
    public DropItem DropItem; // Quai roi item khong
}
[Serializable]
public enum DropItem
{
   DropItem = 0,
   NoDropItem = 1

}
[Serializable]
public struct EnemyItems
{
    public List<GameObject> EnemyItemList;
}
