using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler Instance;
    private Animator AnimTor;
    public Rigidbody2D rb;
    private bool checkFacing;// Trạng thái hướng mặt nhân vật. 
    // ********* Các biến để bắn 
    public Transform gunShot;
    private GameObject bullet;
    float fierate = 0.5f;
    float nextfire = 0f;
    public int ideIndex;
    public bool NextHit;

    private Transform target;
    public float moveSpeed = 5f;
    bool canCallEvent = true;
    public bool CharAttack;
    public GameObject ShowExpObj;

    private void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        // SetPosition();
        transform.SetPositionAndRotation(SaveManager.Instance.BaseData.LocationCharacter, Quaternion.identity);
        NextHit = true;
        Invoke("UpdateTransForm", 1f);
    }

    private void UpdateTransForm()
    {
        SaveManager.Instance.BaseData.LocationCharacter = rb.transform.position;
        Invoke("UpdateTransForm", 0.7f);
    }

    void Attack1() // chức năng bắn 
    {
        if (Time.time > nextfire)
        {
            nextfire = Time.time + fierate;
            if (checkFacing)
            {
                Instantiate(bullet, gunShot.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
            else if (!checkFacing)
            {
                Instantiate(bullet, gunShot.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
        }
    }

    void Attack() // chức năng bắn 
    {
        Debug.Log("Goi Ban");
        if (SkillManager.Instance.SkillNull)
        {
            GameManager.Instance.ShowThongBao("Vui long chon mot ky nang de su dung");

            return;
        }
        if (SkillManager.Instance.SkillSelected.SkillHoa != null)
        {
            var value1 = SkillManager.Instance.SkillSelected.SkillHoa;
            // Xử lý khi SkillHoa có giá trị
        }
        else if (SkillManager.Instance.SkillSelected.SkillBang != null)
        {
            var value2 = SkillManager.Instance.SkillSelected.SkillBang;
            // Xử lý khi SkillBang có giá trị
        }
        else if (SkillManager.Instance.SkillSelected.SkillPhong != null)
        {
            var value3 = SkillManager.Instance.SkillSelected.SkillPhong;
            // Xử lý khi SkillPhong có giá trị
        }
        else if (SkillManager.Instance.SkillSelected.SkillBase != null)
        {
            AnimTor = gameObject.GetComponent<Animator>();
            CreateSkillBase value4 = SkillManager.Instance.SkillSelected.SkillBase;
            // Xử lý khi SkillBase có giá trị
            if (value4.skillType == SkillTypeBase.Canchien)
            {
                //if (Time.time > nextfire)
                //{
                //    nextfire = Time.time + value4.cooldownTim;
                //    AnimTor.SetTrigger("danhtay");
                //}
                if (NextHit)
                {
                    NextHit = false;
                    nextfire = Time.time + value4.cooldownTim;
                    AnimTor.SetTrigger("danhtay");
                    GameObject.Find("SkillCanchien").GetComponent<Canchien>().UpdateTargetValue(value4.cooldownTim);
                    GameManager.Instance.FindObjectInChildren(gameObject.transform, "SkillAnim").GetComponent<PolygonCollider2D>().enabled = true;
                    //if (EnemyTrigger.instance != null)
                    //{
                    //    EnemyTrigger.instance.SendDame();
                    //}

                }


            }
            else
            {
                //if (Time.time > nextfire)
                //{
                //    nextfire = Time.time + value4.cooldownTim;

                //AnimTor.SetTrigger("chuongluc");
                //}
                if (NextHit)
                {
                    NextHit = false;
                    AnimTor.SetTrigger("chuongluc");
                    GameObject obj = Instantiate(SkillManager.Instance.SkillSelected.SkillBase.Objectfired, gunShot.position, gunShot.rotation);
                    if (obj.transform.position.x > gameObject.transform.position.x)
                    {
                        obj.GetComponent<BulletControler>().addForceRight();
                    }
                    else
                    {
                        obj.transform.Rotate(Vector3.up, 180f);
                        obj.GetComponent<BulletControler>().addForceLeft();
                    }
                    GameObject.Find("SkillXachien").GetComponent<Xachien>().UpdateTargetValue(value4.cooldownTim);
                }
            }
        }
        else
        {
            // Xử lý khi không có phần tử con nào chứa giá trị
        }

    }

    private void FixedUpdate()
    {

        // bẵn từ bàn phím
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            Attack(); // Gọi bắn.
        }
    }
    // Di chuyển từ từ tới vị trí của đối tượng khác
    public void MoveGradually()
    {
        StartCoroutine(MoveGraduallyCoroutine());
    }

    private IEnumerator MoveGraduallyCoroutine()
    {
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = target.position;
        float distance = Vector3.Distance(initialPosition, targetPosition);

        while (distance > 0.01f)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);

            distance = Vector3.Distance(transform.position, targetPosition);
            yield return null;
        }
    }

    // Di chuyển tức thì tới vị trí của đối tượng khác
    public void MoveInstantly(Transform _taget)
    {

        float originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0f; // Vô hiệu hóa trọng lực

        rb.MovePosition(_taget.position);

        // rb.gravityScale = originalGravityScale; // Khôi phục trọng lực
        Debug.Log("Đã di chuyển tới." + _taget.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GroundTele")
        {
        }
    }
    public void LoadMap()
    {
        if (canCallEvent)
        {
            StartCoroutine(WaitAndCallEvent());
            {
                if (TeleTileTriger.instance.gameObject.name == "TeleLeftGroundObject" || TeleTileTriger.instance.gameObject.gameObject.name == "TeleLeftGroundObject(Clone)")
                {
                    Debug.Log("Load map Curent -1");
                    if (MapManager.Instance.LeftMap != null)
                    {
                        SaveManager.Instance.BaseData.leverMapIndex = MapManager.Instance.LeftMap.LevelIndex;
                        MapManager.ReadyLoadLeft = false;
                        MapManager.Instance.WhatMap();
                        //LeverManager.Instance.LoadScence("Demo");
                        LeverManager.Instance.UpdateTargetValue(); // Thanh load map // PlayerControler.Instance.MoveInstantly(MapManager.TeleRightTrans);
                        StartCoroutine(WaitForLoadMap("TeleRightGroundObject(Clone)"));


                    }
                    else
                    {
                        Debug.LogError("Bạn chưa thể tới khu vực này");
                    }

                }
                else if (TeleTileTriger.instance.gameObject.gameObject.name == "TeleRightGroundObject" || TeleTileTriger.instance.gameObject.gameObject.name == "TeleRightGroundObject(Clone)")
                {
                    Debug.Log("Load map Curent +1");
                    if (MapManager.Instance.RightMap != null)
                    {
                        SaveManager.Instance.BaseData.leverMapIndex = MapManager.Instance.RightMap.LevelIndex;
                        MapManager.ReadyLoadRight = false;
                        MapManager.Instance.WhatMap();
                        // LeverManager.Instance.LoadScence("Demo");
                        LeverManager.Instance.UpdateTargetValue();
                        // PlayerControler.Instance.MoveInstantly(MapManager.TeleLeftTrans);
                        StartCoroutine(WaitForLoadMap( "TeleLeftGroundObject(Clone)"));
                    }
                    else
                    {
                        Debug.LogError("Bạn chưa thể tới khu vực này");
                    }
                }
                else if (TeleTileTriger.instance.gameObject.gameObject.name == "TeleRight2GroundObject" || TeleTileTriger.instance.gameObject.gameObject.name == "TeleRight2GroundObject(Clone)")
                {
                    Debug.Log("Load map Curent +2");
                    if (MapManager.Instance.RightMap2 != null)
                    {
                        SaveManager.Instance.BaseData.leverMapIndex = MapManager.Instance.RightMap2.LevelIndex;
                        MapManager.ReadyLoadRight = false;
                        MapManager.Instance.WhatMap();
                        // LeverManager.Instance.LoadScence("Demo");
                        LeverManager.Instance.UpdateTargetValue();
                        // PlayerControler.Instance.MoveInstantly(MapManager.TeleLeftTrans);
                        StartCoroutine(WaitForLoadMap("TeleLeftGroundObject(Clone)"));
                    }
                    else
                    {
                        Debug.LogError("Bạn chưa thể tới khu vực này");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Di chyển chậm thôi bạn ôi !");
        }
    }
   
    public IEnumerator WaitForLoadMap( string Child)
    {

        while (true)
        {
            if (MapManager.ReadyLoadRight == true && MapManager.ReadyLoadLeft == true)
            {
                Debug.LogError("Gọi Move");
                transform.SetPositionAndRotation(GameManager.Instance.FindObjectInChildren(GameObject.Find(Child).transform, "TeleTransTaget").GetComponent<Transform>().transform.position, Quaternion.identity);
                break;
            }
            yield return null;
        }

    }

    private IEnumerator WaitAndCallEvent()
    {
        canCallEvent = false;
        yield return new WaitForSeconds(0.5f);
        // Thực hiện các hành động sau 0.5 giây
        canCallEvent = true;
    }
    public void ShowExp(string _what)
    {
        ShowExpObj.SetActive(true);
        GameManager.Instance.FindObjectInChildren(ShowExpObj.transform, "ShowExpText").GetComponent<TextMeshProUGUI>().text = _what.ToString();
        GameManager.Instance.FindObjectInChildren(ShowExpObj.transform, "ShowExpText").GetComponent<Animator>().SetTrigger("Exp");
    }

}