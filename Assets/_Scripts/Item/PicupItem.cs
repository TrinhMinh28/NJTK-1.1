using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PicupItem : MonoBehaviour
{
    public Item _Itemdata;
    public GameObject PicupEffect;
    // Start is called before the first frame update
    private void Start()
    {
        LoadImageItems();
    }

    private void LoadImageItems()
    {
        if (_Itemdata != null)
        {
            SpriteRenderer ImageItems = transform.gameObject.GetComponentInChildren<SpriteRenderer>();
            ImageItems.sprite = _Itemdata.ItemSprite;
        }
        else
        {
            Debug.LogWarning("Item chua co du lieu...");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag== "Player")
        {
            if (ItemManager.Instance.Items.Count < ItemManager.Instance.Slots.Length)
            {
                // Instantiate (PicupEffect, transform.position,Quaternion.identity); tạm thời chưa cần dùng efect
                ItemManager.Instance.AddItem(_Itemdata);

                Destroy(gameObject);
            }
            else
            {
                // Debug.Log(" Hành Trang đã đầy, không thể nhặt,");
                ThongBaoItems();
            }
           
        }
    }
    private void ThongBaoItems()
    {
        // GameObject obj = GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ViewportThongBao");

        if (GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ViewportThongBao") != null || GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ViewportThongBao(Clone)") != null)
        {
            TextMeshProUGUI Text = GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ThongBaoText").GetComponent<TextMeshProUGUI>();
            Text.text += " , " + _Itemdata.ItemName;
        }
        else
        {
            Instantiate(ItemManager.Instance.ViewPortThongBao, GameObject.Find("ThongBao").transform);
            TextMeshProUGUI Text = GameManager.Instance.FindObjectInChildren(GameObject.Find("ThongBao").transform, "ThongBaoText").GetComponent<TextMeshProUGUI>();
            Text.text = "Hành trang đầy, cần bỏ bớt vật phẩm để nhặt " + _Itemdata.ItemName;
        }
    }
}
