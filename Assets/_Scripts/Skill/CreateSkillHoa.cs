using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Hoa", menuName = "2D / Skills /New Skill Hoa", order = 1)]
public class CreateSkillHoa : ScriptableObject
{
    public Sprite SkillImage;
    public string skillName;
    public float skillLevel;
    public string skillDescription;
    public float cooldownTim; 
    public float Hpmat;
    public float Mpmat;
    public float PhantramHoasat;
    public float PhantramBong;


    public RuntimeAnimatorController animationControl;
    public AnimationClip clipSkill;
    public SkillTypeHoa skillType;
    public GameObject Objectfired; // bắn ra cái gì nếu là xạ chiến.
}
[Serializable]
public enum SkillTypeHoa
{
    Canchien = 0,
    XaChien = 1
}
