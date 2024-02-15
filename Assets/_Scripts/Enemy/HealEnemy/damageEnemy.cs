using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageEnemy : MonoBehaviour
{
    public float pushBackFore = 25f; // lực nhảy khi chạm vào gai
    public float enemyDamage = 0f;
    float dameRate = 1f;
    float nextDame;// thời gian gây dame lần 2
    // Start is called before the first frame update
    void Start()
    {
        nextDame = 0f;
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision) // va chạm với đối tượng nào đó.
    {
        if (collision.gameObject.tag == "Player" && nextDame <Time.time)
        {
            PlayerHeal _objPlayerHeal = collision.gameObject.GetComponent<PlayerHeal>();
            _objPlayerHeal.playerAddDamage(enemyDamage);
            nextDame = dameRate+Time.time;
            pushBackFor(collision.transform);
            Destroy(gameObject);
        }
    }
    void pushBackFor(Transform transformPlayer)
    {
        Vector2 pushDerection = new Vector2(0, (transformPlayer.position.y - this.transform.position.y)).normalized; // lấy vị trí layer trừ vị trí của enermy và trả về normal là 1
        pushDerection *= pushBackFore;
        Rigidbody2D _objPushRb = transformPlayer.GetComponent<Rigidbody2D>();
        _objPushRb.velocity = Vector2.zero; // khi này play đang chịu các lực ấn từ bàn phím và cần cho layer đứng im đã
        _objPushRb.AddForce(pushDerection*Time.deltaTime,ForceMode2D.Impulse);
    }
}
