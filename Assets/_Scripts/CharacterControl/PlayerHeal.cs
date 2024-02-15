using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeal : MonoBehaviour
{
    public static PlayerHeal Instance;
    public Slider playerHealSlider;
    private float maxHeal;
    private float maxMP;
    public float currentHeal;
    public float currentMp;
    public GameObject bloodEffect;
    public GameObject DeadUI;
    [SerializeField] private HealBar _healBar; 
    [SerializeField] private HealBar _MpBar; 
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartHeal", 0.5f);
        Instance = this;
    }
    public void StartHeal()
    {
        maxHeal = InfomationManager.Instance.LoadData.Hp;
        maxMP = InfomationManager.Instance.LoadData.Mp;
        currentHeal = maxHeal;
        currentMp = maxMP;
        playerHealSlider.maxValue = maxHeal;
        playerHealSlider.value = currentHeal;
        _healBar.updateHealBar(maxHeal, currentHeal);
        _MpBar.updateMpBar(maxHeal, currentHeal);
    }
    // Update is called once per frame
    public void playerAddDamage(float damge)
    {
        #region
        //Debug.Log("b goi len");
        //if (damge <= 0) { return; }
        //currentHeal -= damge;
        //playerHealSlider.value = currentHeal;
        //if (currentHeal <= 0) { makeDead(); }
        #endregion

        currentHeal -= damge;
        if (currentHeal <0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Die");

        }
        else
        {
        _healBar.updateHealBar(maxHeal, currentHeal);

        }
    }

    public void SHowDeadUI() // được  gọi tới từ Animation Dead
    {
        DeadUI.SetActive(true);
         GameManager.Instance.PauseGame(true);
      //  gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void playerTruMp(float Mp)
    {

        currentMp -= Mp;
        _healBar.updateMpBar(maxMP, currentMp);
    }
    void makeDead()
    {
        // gọi thêm 1 hàm chạy animation
        GameObject clone = Instantiate(bloodEffect, transform.position, transform.rotation);
        Destroy(clone, 1f);
    }
    // Hooi máu khi ăn Item Máu
    public void addHealth (float healthItem)
    {
      //  Debug.Log("b goi hoi mau");
        if (maxHeal <= (currentHeal += healthItem))
        {
            currentHeal += healthItem;
        }
        if (currentHeal > maxHeal)
        {
            currentHeal = maxHeal;
        }
        //  playerHealSlider.value = currentHeal;
        _healBar.updateHealBar(maxHeal, currentHeal);
    }
}
