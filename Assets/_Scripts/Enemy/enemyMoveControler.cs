using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyMoveControler : MonoBehaviour
{
    public float bossSpeed; // tốc độ bosss
    public GameObject ObjectDieuKhien;
    EnemyTrigger Enemytrigger;
    public Vector3 OldTransform;
    Rigidbody2D EnemyRb;
    Animator anim;
    // kb các biến để enemy di chuyen
    bool checkFace = true;
    float facingTime = 5f;
    float nextFlip = 0f;
    bool canFlip = true;
    public Vector2 directions;
    Vector3 theScale;
    Vector3 theScale2;
    // Start is called before the first frame update
    void Start()
    {
        theScale = ObjectDieuKhien.transform.localScale;
        theScale2 = ObjectDieuKhien.transform.localScale;
        OldTransform = ObjectDieuKhien.transform.position;
        EnemyRb = ObjectDieuKhien.GetComponent<Rigidbody2D>();
        anim = GameManager.Instance.FindObjectInChildren(ObjectDieuKhien.transform, "ChildObjectAnim").GetComponent<Animator>();
        Enemytrigger = ObjectDieuKhien.GetComponent<EnemyTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFlip)
        {
            nextFlip = Time.time + facingTime;
            Flip();
        }
    }
    void CheckFaceFlip()
    {

    }
    void Flip()
    {
        if (!canFlip) { return; }
        checkFace = !checkFace;
        theScale.x *= -1;
        ObjectDieuKhien.transform.localScale = theScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Debug.Log("Va chạm voi Player");
            if (checkFace && collision.transform.position.x < ObjectDieuKhien.transform.position.x)
            {
                Flip();
            }
            else if (!checkFace && collision.transform.position.x > ObjectDieuKhien.transform.position.x)
            {
                Flip();
            }
            canFlip = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector2 direction = new Vector2(collision.transform.position.x, collision.transform.position.y) - EnemyRb.position;
            MoveEnemy(direction);
            directions = direction;
            //StartCoroutine(TimeMove());

        }
    }

    private void MoveEnemy(Vector2 direction)
    {
        if (Enemytrigger.Run)
        {
            // EnemyRb.velocity = new Vector2(-1, 0) * bossSpeed;
            //EnemyRb.velocity = direction.normalized * EnemyRb.velocity.magnitude * bossSpeed;
            EnemyRb.velocity = direction.normalized * bossSpeed;
            if (direction.normalized.x > 0)
            {
                ObjectDieuKhien.transform.localScale = new Vector3(theScale2.x * 1, theScale2.y, theScale2.z);
            }
            else if (direction.normalized.x < 0)
            {
                ObjectDieuKhien.transform.localScale = new Vector3(theScale2.x * -1, theScale2.y, theScale2.z);
            }
            anim.SetTrigger("run");
        }
    } 


    private IEnumerator TimeMove()
    {
        Enemytrigger.Run = false;

        yield return new WaitForSeconds(0.3f);

        Enemytrigger.Run = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canFlip = true;
            EnemyRb.velocity = new Vector2(0, 0);
            EnemyRb.MovePosition (OldTransform);
            anim.SetTrigger("run");
        }
    }
}
