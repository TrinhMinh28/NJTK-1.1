using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    public float heartAmount = 2f; // Lượng máu dc hồi
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            PlayerHeal _objPLH = collision.gameObject.GetComponent<PlayerHeal>();
            _objPLH.addHealth(heartAmount);
            Destroy(gameObject);
        }
    }
}
