using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class XachienVukhi : MonoBehaviour, IPointerDownHandler
{
    private Image BoderImage;
    private Image SkillImage;
    private Text SkillText;
    private Transform Transparent;
    private SkillPanel SkillPanel;
    public SkillWeapons ThisSkill;


    // Start is called before the first frame update
    private void Awake()
    {
        Transparent = transform.parent;
        SkillPanel = Transparent.gameObject.GetComponent<SkillPanel>();
        BoderImage = gameObject.transform.GetChild(2).GetComponent<Image>();
        SkillText = gameObject.transform.GetChild(1).GetComponent<Text>();
        BoderImage.gameObject.SetActive(false);
        SkillImage = gameObject.GetComponent<Image>();
    }
    void Start()
    {
        CheckSkill();
    }

    public void CheckSkill()
    {
        if (ThisSkill.SkillBang != null || ThisSkill.SkillHoa != null || ThisSkill.SkillPhong != null)
        {
            SkillText.text = "4";
        }
        else
        {
            SkillText.text = "Trống";
        }
    }

    public void ShowSkill(SkillWeapons skillweapons)
    {
        if (skillweapons.SkillBang != null || skillweapons.SkillHoa != null || skillweapons.SkillPhong != null)
        {
            ThisSkill = skillweapons;
        }
        else
        {
            Debug.LogWarning("Vũ khí không có kỹ năng");
            ThisSkill = skillweapons;
        }

        switch (CheckSkill(ThisSkill))
        {
            case "bang":
                SkillImage.sprite = ThisSkill.SkillBang.SkillImage;
                break;
            case "hoa":
                SkillImage.sprite = ThisSkill.SkillHoa.SkillImage;
                break;
            case "phong":
                SkillImage.sprite = ThisSkill.SkillPhong.SkillImage;
                break;

            default:
                SkillImage.sprite = null;
                break;
        }
        CheckSkill();
    }
    public string CheckSkill(SkillWeapons skillweapons)
    {
        string retunskill = "";
        if (skillweapons.SkillBang != null)
        {
            retunskill = "bang";

        }
        else if (skillweapons.SkillHoa != null)
        {
            retunskill = "hoa";
        }
        else if (skillweapons.SkillPhong != null)
        {
            retunskill = "phong";
        }
        return retunskill;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        SkillPanel.SetImageSelected(gameObject);
        SkillManager.Instance.UpdateUseSkill(ThisSkill);
    }

    public void RemoveSkill(SkillWeapons skillweapons)
    {
        ThisSkill = skillweapons;
        CheckSkill();
        SkillImage.sprite = null;
    }
}
