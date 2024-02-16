using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Skill Bang", menuName = "2D / Skills /New Skill Bang", order = 3)]
public class CreateSkillBang : ScriptableObject
{
    public Sprite SkillImage;
    public string skillName;
    public string skillLevel;
    public string skillDescription;
    public float cooldownTim; 
    public float Hpmat;
    public float Mpmat;
    public float PhantramBangsat;
    public float PhantramDongBang;


    public RuntimeAnimatorController animationControl;
    public AnimationClip clipSkill;
    public SkillTypeBang skillTypeBang;
    public GameObject Objectfired; // bắn ra cái gì nếu là xạ chiến.
}
[Serializable]
public enum SkillTypeBang
{
    Canchien = 0,
    XaChien = 1
}
