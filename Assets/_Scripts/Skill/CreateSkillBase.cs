using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Skill Base", menuName = "2D / Skills /New Skill Base", order = 4)]
public class CreateSkillBase : ScriptableObject
{
    public Sprite SkillImage;
    public string skillName;
    public float skillLevel;
    public string skillDescription;
    public float cooldownTim; 
    public float Hpmat;
    public float Mpmat;
    public float PhantramSatThuong;

    public RuntimeAnimatorController animationControl;
    public List<AnimationClip> clipSkill;
    public SkillTypeBase skillType;
    public AudioClip AudioAnim;
    public GameObject Objectfired; // bắn ra cái gì nếu là xạ chiến.
}
[Serializable]
public enum SkillTypeBase
{
    Canchien = 0,
    XaChien = 1
}
