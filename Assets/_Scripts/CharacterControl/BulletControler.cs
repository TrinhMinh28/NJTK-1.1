using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public float bulletSpeed;
    Rigidbody2D rb;
    public float aliveTime = 1.5f;
    private void Awake() // gọi kể cả khi không dc chạy
    {
        rb = GetComponent<Rigidbody2D>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, aliveTime); // sau 1 s viên đạn bắn ra bị hủy
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // cho viên đạn dừng lại
    public void addForceRight()
    {
        rb.AddForce(new Vector2(1, 0) * bulletSpeed, ForceMode2D.Impulse);
    }
    public void addForceLeft()
    {

            rb.AddForce(new Vector2(-1, 0) * bulletSpeed, ForceMode2D.Impulse);
         
    }
    public void removeBullet()
    {
        rb.velocity = new Vector2(0, 0);
        //Destroy(gameObject);
    }
}
