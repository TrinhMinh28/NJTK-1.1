using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class AnimControl : MonoBehaviour
{
    private Animator AnimTor;
    private Animator EfectSkillOnCharAnimTor;
    public GameObject SkillAnim;
    public GameObject EfectSkillOnCharacterAnim;
    private void Start()
    {
        AnimTor = SkillAnim.GetComponent<Animator>();
        EfectSkillOnCharAnimTor = EfectSkillOnCharacterAnim.GetComponent<Animator>();
    }

    public void EightLeveSkill(string value)
    {
        PlaySkillOnCharAnim("Skill8x");
        PlayEfectSkillOnCharAnim("8xEffect");
    }
    public void TenLeveSkill(string value)
    {
        PlaySkillOnCharAnim("Skill10x");
        PlayEfectSkillOnCharAnim("10xEffect"); 
    }
    public void StartAnimEvents(int value)
    {
        if (value == 1)
        {
            PlayerControler.Instance.CharAttack = true;
            AudioSource audio = GameManager.Instance.FindObjectInChildren(GameObject.Find("Audio").transform,"Animation").GetComponent<AudioSource>();
            if (SkillManager.Instance.SkillBase !=null)
            {
                audio.PlayOneShot(SkillManager.Instance.SkillBase.AudioAnim);
            }
           
        }
        else
        {
            PlayerControler.Instance.CharAttack = false;
        }
        
    }
    public void ChuongAnimEvents(int value)
    {
        if (value == 1)
        {
            AudioSource audio = GameManager.Instance.FindObjectInChildren(GameObject.Find("Audio").transform, "Animation").GetComponent<AudioSource>();
            if (SkillManager.Instance.SkillBase != null)
            {
                audio.PlayOneShot(SkillManager.Instance.SkillBase.AudioAnim);
            }
        }
        else
        {
          
        }
    }
    public void TenMoveEnd(string value)
    {
        Debug.Log("PrintEvent: " + value+ " called at: " + Time.time);
    }
    private void PlaySkillOnCharAnim(string lever)
    {
        AnimTor.SetTrigger(lever); // Kích hoạt trigger có tên là "Water"
    }
    private void PlayEfectSkillOnCharAnim(string lever)
    {
        EfectSkillOnCharAnimTor.SetTrigger(lever); // Kích hoạt trigger có tên là "Water"
    }

}
