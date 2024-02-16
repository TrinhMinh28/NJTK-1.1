using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    public GameObject theBomb;
    public Transform shootTrans;
    public float ShootDelay;
    float shootTime  = 0f;
    Animator BombAnim;
    private void Awake()
    {
        BombAnim = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag =="Player" && Time.time > shootTime)
        {
            shootTime = Time.time + ShootDelay;
            Instantiate(theBomb, shootTrans.position,Quaternion.identity);
            BombAnim.SetTrigger("bombShoot");
        }
    }
}
