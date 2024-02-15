using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGunHit : MonoBehaviour
{
    public float bulletDamage = 0.5f;
    BulletControler _objBulletCtr;
    public GameObject bulletExplodEffect;
    private bool checkDestroy = true;
    // Start is called before the first frame update
    private void Awake()
    {
        _objBulletCtr = GetComponentInParent<BulletControler>(); // đọc các component từ đối tượng cha.
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision) // va chạm istriger 
    {
        if (collision.gameObject.tag == "Cotheban")// chỉ bắn vật k có máu
        {
            _objBulletCtr.removeBullet();
            Destroy(gameObject); // xóa viên đạn
            GameObject clone = Instantiate(bulletExplodEffect, transform.position, transform.rotation);
            Destroy(clone,1f);
        }
        if (collision.gameObject.tag == "Cothetieudiet") // bắn vật có máu
        {
            _objBulletCtr.removeBullet();
            Destroy(gameObject); // xóa viên đạn
            GameObject clone = Instantiate(bulletExplodEffect, transform.position, transform.rotation);
            Destroy(clone, 1f);
            EnemyTrigger _objheal = collision.gameObject.GetComponent<EnemyTrigger>();
            CreateSkillBase baseSkill = SkillManager.Instance.SkillSelected.SkillBase;
            _objheal.UpdateCurrentHeal(InfomationManager.Instance.LoadData.Dame + Mathf.RoundToInt(InfomationManager.Instance.LoadData.Dame * baseSkill.PhantramSatThuong / 100), InfomationManager.Instance.LoadData.XuyenGiap,InfomationManager.Instance.LoadData.Crit);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Cothetieudiet"))// nếu đối tượng đã có tag thì sử dụng layer để phân loại
        {

        }
    }
    //private void OnTriggerStay2D(Collider2D collision) // sảy ra sau khi đã va chạm với colider đi vào bên trong colidrer
    //{
    //    if (collision.gameObject.tag == "Cotheban") // chỉ bắn vật k có máu
    //    {
    //        _objBulletCtr.removeBullet();
    //        Destroy(gameObject);
    //        Instantiate(bulletExplodEffect, transform.position, transform.rotation);
    //    }
    //    if (collision.gameObject.tag == "Cothetieudiet")// bắn vật có máu
    //    {
    //        _objBulletCtr.removeBullet();
    //        Destroy(gameObject); // xóa viên đạn
    //        GameObject clone = Instantiate(bulletExplodEffect, transform.position, transform.rotation);
    //        Destroy(clone, 1f);
    //        EnemyTrigger _objheal = collision.gameObject.GetComponent<EnemyTrigger>();
    //        _objheal.UpdateCurrentHeal(bulletDamage);
    //    }
    //}
}
