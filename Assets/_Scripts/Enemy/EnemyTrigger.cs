using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTrigger : MonoBehaviour
{
    public static EnemyTrigger instance;
    public CreateEnemy EnemyData;
    [SerializeField] private GameObject main_gameObject;
    [SerializeField] private GameObject canvasGameObject;
    public float pushBackFore = 25f; // lực nhảy khi chạm vào gai
    private Image imgHealBar;
    private float curentHeal;
    float taget = 1;
    float reducSpeed = 2;
    Animator animator;
    public float DodaiTiaQuet;
    public LayerMask layerMask;
    public GameObject enemyDeadEffect;
    public bool Run;
    public bool NhanDame;
    bool canHit;
    bool nextHit = true;
    GameObject ShowDame;

    public GameObject projectilePrefab; // Prefab của vật thể cần bắn 
    public Transform spawnPoint; // Vị trí bắn vật thể
    public float projectileForce = 10f; // Lực bắn vật thể
    public float recoilForce = 5f; // Lực phản giật ngược lại
    // *******************
    public GameObject TheDrop;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        imgHealBar = GameManager.Instance.FindObjectInChildren(canvasGameObject.transform, "EnemyHealImage").GetComponent<Image>();
        main_gameObject = transform.parent.gameObject; // lấy đối tượng cha
        curentHeal = EnemyData.HpEnemy;
        animator = GameManager.Instance.FindObjectInChildren(gameObject.transform, "ChildObjectAnim").GetComponent<Animator>();
        ShowDame = GameManager.Instance.FindObjectInChildren(canvasGameObject.transform, "ShowDame");
    }
    private void OnEnable()
    {
        curentHeal = EnemyData.HpEnemy;
        UpdateHealbar(EnemyData.HpEnemy, curentHeal);
    }
    private void OnTriggerStay2D(Collider2D collision) // va chạm với đối tượng nào đó.
    {
        //if (collision.gameObject.tag == "Player" && nextDame < Time.time)
        //{
        //    PlayerHeal _objPlayerHeal = collision.gameObject.GetComponent<PlayerHeal>();
        //    _objPlayerHeal.playerAddDamage(EnemyData.DameEnemy);
        //    nextDame = dameRate + Time.time;
        //    pushBackFor(collision.transform);
        //}
        if (collision.gameObject.tag == "Skill" && PlayerControler.Instance.CharAttack == true)
        {
           // Debug.Log(" có thể bị chem");
            instance = this;
            NhanDame = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Skill")
        {
            NhanDame = true;
            instance = this;
            SendDame();
            Debug.Log("Nhan dame");
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Skill" && PlayerControler.Instance.CharAttack == true)
        {
            Debug.Log(" k the bị chem");
            instance = null;
            NhanDame = false;
        }
    }

    public void SendDame()
    {
        if (NhanDame)
        {
            if (SkillManager.Instance.SkillSelected.SkillHoa != null)
            {
                CreateSkillHoa hoa = SkillManager.Instance.SkillSelected.SkillHoa;

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
                CreateSkillBase baseSkill = SkillManager.Instance.SkillSelected.SkillBase;
                // Xử lý khi SkillBase có giá trị
                UpdateCurrentHeal(InfomationManager.Instance.LoadData.Dame + Mathf.RoundToInt(InfomationManager.Instance.LoadData.Dame * (baseSkill.PhantramSatThuong * baseSkill.skillLevel) / 100), InfomationManager.Instance.LoadData.XuyenGiap, InfomationManager.Instance.LoadData.Crit);
               
            }
        }

    }

  
   
    #region
    //void pushBackFor(Transform transformPlayer)
    //{
    //    Vector2 pushDerection = new Vector2(0, (transformPlayer.position.y - this.transform.position.y)).normalized; // lấy vị trí layer trừ vị trí của enermy và trả về normal là 1
    //    pushDerection *= pushBackFore;
    //    Rigidbody2D _objPushRb = transformPlayer.GetComponent<Rigidbody2D>();
    //    _objPushRb.velocity = Vector2.zero; // khi này play đang chịu các lực ấn từ bàn phím và cần cho layer đứng im đã
    //    _objPushRb.AddForce(pushDerection * Time.deltaTime, ForceMode2D.Impulse);
    //}
    #endregion
    public void UpdateCurrentHeal(float damage, float xuyengiap, float tileCrit)
    {
        float dame;
        ShowDame.SetActive(true);
        //imgHealBar.gameObject.SetActive(true);
        if (GetRandomResult(tileCrit))
        {
            dame = (damage - Mathf.Max(EnemyData.ArmorEnemy - xuyengiap, 0)) * 2;
            curentHeal -= dame;
            GameManager.Instance.FindObjectInChildren(ShowDame.transform, "ShowDameText").GetComponent<TextMeshProUGUI>().text = dame.ToString();
            GameManager.Instance.FindObjectInChildren(ShowDame.transform, "ShowDameText").GetComponent<Animator>().SetTrigger("Crit");
        }
        else
        {
            dame = damage - Mathf.Max(EnemyData.ArmorEnemy - xuyengiap, 0);
            curentHeal -= dame;
            GameManager.Instance.FindObjectInChildren(ShowDame.transform, "ShowDameText").GetComponent<TextMeshProUGUI>().text = dame.ToString();
            GameManager.Instance.FindObjectInChildren(ShowDame.transform, "ShowDameText").GetComponent<Animator>().SetTrigger("NoCrit");
        }
        float exp = dame * SaveManager.Instance.BaseData.leve + ((dame * SaveManager.Instance.BaseData.leve) * (EnemyData.EnemyLevel - SaveManager.Instance.BaseData.leve) / 100);
        InfomationManager.Instance.UpdateExp(exp);
        animator.SetTrigger("hit");
        UpdateHealbar(EnemyData.HpEnemy, curentHeal);

        if (curentHeal <= 0)
        {
            animator.SetTrigger("hit");
            Invoke("dead", 1f);
            // dead();
        }
    }
    public static bool GetRandomResult(float probability)
    {
        // Đảm bảo giá trị xác suất nằm trong khoảng từ 0 đến 100
        probability = Mathf.Clamp(probability, 0f, 100f);

        // Tạo một số ngẫu nhiên từ 0 đến 100
        float randomValue = UnityEngine.Random.Range(0f, 100f);

        // So sánh giá trị ngẫu nhiên với xác suất và trả về kết quả tương ứng
        return randomValue <= probability;
    }
    public void UpdateHealbar(float maxHeal, float currenHeal)
    {
        taget = currenHeal / maxHeal;
    }
    public void ShootProjectile()
    {
        // Tạo một vật thể từ Prefab và vị trí bắn
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.transform);
        damageEnemy enemydame = projectile.GetComponent<damageEnemy>();
        enemydame.enemyDamage = EnemyData.DameEnemy;

        // Lấy Rigidbody của vật thể
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        // Tạo vector hướng bắn bằng cách lấy hướng nhìn hiện tại của đối tượng gốc
        Vector3 shootDirection = transform.forward;

        // Áp dụng lực bắn vật thể theo hướng đã tính và với lực projectileForce
        projectileRigidbody.AddForce(shootDirection * projectileForce, ForceMode2D.Impulse);

        // Áp dụng lực phản giật ngược lại cho đối tượng gốc
        GetComponent<Rigidbody2D>().AddForce(-shootDirection * recoilForce, ForceMode2D.Impulse);
    }
    void dead()
    {
        GameObject clone = Instantiate(enemyDeadEffect, transform.position, transform.rotation);// khai bao hieu ung dead
        Destroy(clone, 0.3f);
        // Rơi Item từ Enemy .
        if (EnemyData.DropItem == DropItem.DropItem)
        {
            Instantiate(TheDrop, transform.position, transform.rotation);
        }
        // Destroy(main_gameObject);// huy enemy
        main_gameObject.SetActive(false);// huy enemy
        Invoke("hoisinhEnemy", 10f);
    }
    void hoisinhEnemy()
    {
        main_gameObject.SetActive(true);
    }
    private void Update()
    {
        imgHealBar.fillAmount = Mathf.MoveTowards(imgHealBar.fillAmount, taget, reducSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        QuetVatThe();
        EnemyAttack();

        //    tạo tịa raycat để  // kiểm tra xem đã đến đủ gần để tấn công chưa, nếu đủ gần thì dừng velocity lại.
    }

    private void EnemyAttack()
    {
        if (canHit && nextHit)
        {
            StartCoroutine(InvokeHitAnimWithDelay());
            nextHit = false;
        }
    }

    private IEnumerator InvokeHitAnimWithDelay()
    {
        // Gọi hàm cần chờ
        HitAnim();

        // Chờ 1.5 giây
        yield return new WaitForSeconds(EnemyData.nextDamerate);

        // Cho phép gọi hàm lần tiếp theo
        nextHit = true;
    }

    private void HitAnim()
    {
        animator.SetTrigger("attack");
    }
    private void QuetVatThe()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right * transform.localScale.x) * DodaiTiaQuet, Color.yellow);

        // Vector2 raycastOrigin = characterCollider.bounds.center - Vector3.up * characterCollider.bounds.extents.x;

        // RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection( Vector2.up), Mathf.Infinity, LayerMask.GetMask("GroundTile")); // Bắn tia  max
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right * transform.localScale.x), DodaiTiaQuet, layerMask); // Bắn dài 4f
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right * transform.localScale.x) * hit.distance, Color.red);
            Run = false;
            canHit = true;
        }
        else
        {
            Run = true;
            canHit = false;
        }
    }
}
