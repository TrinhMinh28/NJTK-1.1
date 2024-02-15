using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Skill Phong", menuName = "2D / Skills /New Skill Phong", order = 2)]
public class CreateSkillPhong : ScriptableObject
{
    public Sprite SkillImage;
    public string skillName;
    public string skillLevel;
    public string skillDescription;
    public float cooldownTim; 
    public float Hpmat;
    public float Mpmat;

    public float PhantramPhongsat;
    public float PhantramChoang;


    public RuntimeAnimatorController animationControl;
    public AnimationClip clipSkill;
    public SkillTypePhong skillType;
    public GameObject Objectfired; // bắn ra cái gì nếu là xạ chiến.
}
[Serializable]
public enum SkillTypePhong
{
    Canchien = 0,
    XaChien = 1
}
