using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealBar : MonoBehaviour
{
    [SerializeField] private Image _SpireHPBarImage;
    [SerializeField] private Image _SpireMPBarImage;
    private float _tarGet ;
    [SerializeField] private float _reDucSpeed = 2;
    public void updateHealBar (float maxHeal, float CurentHeal)
    {
        _tarGet = CurentHeal / maxHeal;
    }
    public void updateMpBar(float maxMp, float CurentMP)
    {
        _tarGet = CurentMP / maxMp;
    }
    private void Update()
    {
        _SpireHPBarImage.fillAmount = Mathf.MoveTowards(_SpireHPBarImage.fillAmount,_tarGet,_reDucSpeed*Time.deltaTime);
    }
}
