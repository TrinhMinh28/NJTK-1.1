using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleTileTriger : MonoBehaviour
{
    public static TeleTileTriger instance;
    public GameObject mainObject;
    public Transform TagetTransform;
    public bool ReadyLoadRight;
    public bool ReadyLoadLeft;

    private bool canCallEvent = true;

    private void Start()
    {

        instance = this;
        mainObject = instance.gameObject;
      //  TagetTransform = GameManager.Instance.FindObjectInChildren(mainObject.transform, "TeleTransTaget").GetComponent<Transform>();
        SendTransTele();
        // Debug.Log("Tele chạy " + mainObject.name);
    }
    private void OnDestroy()
    {
        if (mainObject.name == "TeleLeftGroundObject" || mainObject.name == "TeleLeftGroundObject(Clone)")
        {
            MapManager.ReadyLoadLeft = false;
        }
        else if (mainObject.name == "TeleRightGroundObject" || mainObject.name == "TeleRightGroundObject(Clone)")
        {
            MapManager.ReadyLoadRight = false;
        }
        else if (mainObject.name == "TeleRight2GroundObject" || mainObject.name == "TeleRight2GroundObject(Clone)")
        {
            MapManager.ReadyLoadRight = false;
        }
      //  Debug.LogError("Gọi False Set Load");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            instance = this;
            PlayerControler.Instance.LoadMap();
        }
    }
    private void SendTransTele()
    {
        if (mainObject.name == "TeleLeftGroundObject" || mainObject.name == "TeleLeftGroundObject(Clone)")
        {
            //MapManager.Instance.TeleLeftTrans = TagetTransform;
            // Debug.Log("Đã gán cho trái tọa độ " + MapManager.Instance.TeleLeftTrans.position.ToString());
            MapManager.ReadyLoadLeft = true;
        }
        else if (mainObject.name == "TeleRightGroundObject" || mainObject.name == "TeleRightGroundObject(Clone)" || mainObject.name == "TeleRight2GroundObject(Clone)")
        {
            //MapManager.Instance.TeleRightTrans = TagetTransform;
            MapManager.ReadyLoadRight = true;

            //Debug.Log("Đã gán cho phải tọa độ " + MapManager.Instance.TeleRightTrans.position.ToString());
        }
       // Debug.LogError("Gọi true Set Load");
    }

    //private void OnTriggerEnter2D(Collider2D collision) // lỗi gọi sự kiện 2 lần 
    #region
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (canCallEvent)
    //    {
    //        StartCoroutine(WaitAndCallEvent());
    //        if (collision.gameObject.CompareTag("Player"))
    //        {
    //            if (mainObject.name == "TeleLeftGroundObject" || mainObject.name == "TeleLeftGroundObject(Clone)")
    //            {
    //                Debug.Log("Load map Curent -1");
    //                if (MapManager.Instance.LeftMap != null)
    //                {
    //                    // MapManager.Instance.leverIndex = MapManager.Instance.LeftMap.LevelIndex;
    //                    // MapManager.Instance.LoadMaps(MapManager.Instance.leverIndex);
    //                    SaveManager.LoadData.leverMapIndex = MapManager.Instance.LeftMap.LevelIndex;
    //                    MapManager.Instance.WhatMap();
    //                    //LeverManager.Instance.LoadScence("Demo");
    //                    LeverManager.Instance.UpdateTargetValue(); // Thanh load map
    //                                                               // PlayerControler.Instance.MoveInstantly(MapManager.TeleRightTrans);
    //                    collision.gameObject.GetComponent<Rigidbody2D>().MovePosition(MapManager.TeleRightTrans.position);


    //                }
    //                else
    //                {
    //                    Debug.LogError("Bạn chưa thể tới khu vực này");
    //                }

    //            }
    //            else if (mainObject.name == "TeleRightGroundObject" || mainObject.name == "TeleRightGroundObject(Clone)")
    //            {
    //                Debug.Log("Load map Curent +1");
    //                if (MapManager.Instance.RightMap != null)
    //                {
    //                    //MapManager.Instance.leverIndex = MapManager.Instance.RightMap.LevelIndex;
    //                    //MapManager.Instance.LoadMaps(MapManager.Instance.leverIndex);
    //                    SaveManager.LoadData.leverMapIndex = MapManager.Instance.RightMap.LevelIndex;
    //                    MapManager.Instance.WhatMap();
    //                    // LeverManager.Instance.LoadScence("Demo");
    //                    LeverManager.Instance.UpdateTargetValue();
    //                    // PlayerControler.Instance.MoveInstantly(MapManager.TeleLeftTrans);
    //                    collision.gameObject.GetComponent<Rigidbody2D>().MovePosition(MapManager.TeleLeftTrans.position);


    //                }
    //                else
    //                {
    //                    Debug.LogError("Bạn chưa thể tới khu vực này");
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Di chyển chậm thôi bạn ôi !");
    //    }
    //}


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (canCallEvent)
    //    {
    //        StartCoroutine(WaitAndCallEvent());
    //        if (collision.tag == "Player")
    //        {
    //            if (mainObject.name == "TeleLeftGroundObject" || mainObject.name == "TeleLeftGroundObject(Clone)")
    //            {
    //                Debug.Log("Load map Curent -1");
    //                if (MapManager.Instance.LeftMap != null)
    //                {
    //                    SaveManager.LoadData.leverMapIndex = MapManager.Instance.LeftMap.LevelIndex;
    //                    MapManager.Instance.WhatMap();
    //                    //LeverManager.Instance.LoadScence("Demo");
    //                    LeverManager.Instance.UpdateTargetValue(); // Thanh load map // PlayerControler.Instance.MoveInstantly(MapManager.TeleRightTrans);
    //                    StartCoroutine(WaitForLoadMap(collision, "TeleRightGroundObject(Clone)"));
    //                }
    //                else
    //                {
    //                    Debug.LogError("Bạn chưa thể tới khu vực này");
    //                }

    //            }
    //            else if (mainObject.name == "TeleRightGroundObject" || mainObject.name == "TeleRightGroundObject(Clone)")
    //            {
    //                Debug.Log("Load map Curent +1");
    //                if (MapManager.Instance.RightMap != null)
    //                {
    //                    SaveManager.LoadData.leverMapIndex = MapManager.Instance.RightMap.LevelIndex;
    //                    MapManager.Instance.WhatMap();
    //                    // LeverManager.Instance.LoadScence("Demo");
    //                    LeverManager.Instance.UpdateTargetValue();
    //                    // PlayerControler.Instance.MoveInstantly(MapManager.TeleLeftTrans);
    //                    StartCoroutine(WaitForLoadMap(collision, "TeleLeftGroundObject(Clone)"));


    //                }
    //                else
    //                {
    //                    Debug.LogError("Bạn chưa thể tới khu vực này");
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Di chyển chậm thôi bạn ôi !");
    //    }

    //}

    //private IEnumerator WaitForLoadMap(Collider2D collision,string Child)
    //{

    //    while (ReadyLoadLeft == true && ReadyLoadRight == true)
    //    {
    //        Debug.LogError("Gọi Move");
    //        collision.transform.SetPositionAndRotation(GameManager.Instance.FindObjectInChildren(GameObject.Find(Child).transform, "TeleTransTaget").GetComponent<Transform>().transform.position, Quaternion.identity);
    //        yield return null;
    //    }
    //        // rb.MovePosition(MapManager.Instance.TeleRightTrans.position);
    //}
    #endregion
    private IEnumerator WaitAndCallEvent()
    {
        canCallEvent = false;
        yield return new WaitForSeconds(0.5f);
        // Thực hiện các hành động sau 0.5 giây
        canCallEvent = true;
    }
}
