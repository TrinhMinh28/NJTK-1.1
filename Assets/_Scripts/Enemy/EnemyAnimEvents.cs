using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    public static EnemyAnimEvents instance;
    public EnemyTrigger Enemytriger;
    // *******************
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Enemytriger = gameObject.GetComponentInParent<EnemyTrigger>();
    }  

    public void AttackEvent()
    {
        Debug.Log(" Đã tấn công");
        Enemytriger.ShootProjectile();
    }
  
    public void HitEvent()
    {
        Debug.Log(" Đã Hit");
       
    }


}
