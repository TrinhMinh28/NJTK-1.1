using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Canchien : MonoBehaviour, IPointerDownHandler
{
    private Image SkillPanell;
    private Image BoderImage;
    private Transform Transparent;
    private SkillPanel SkillPanel;
    public SkillWeapons ThisSkill;
    public Text canchientext;
    private float _target;

    // Start is called before the first frame update
    void Start()
    {
        Transparent = transform.parent;
        SkillPanel = Transparent.gameObject.GetComponent<SkillPanel>();
        SkillPanell = gameObject.transform.GetChild(3).GetComponent<Image>();
        BoderImage = gameObject.transform.GetChild(2).GetComponent<Image>();
        canchientext = gameObject.transform.GetChild(4).GetComponent<Text>();
        BoderImage.gameObject.SetActive(false);
    }


    public async void UpdateTargetValue(float timeSkill)
    {
        float elapsedTime = timeSkill;
        while (elapsedTime > 0)
        {
            //await Task.Delay(Mathf.RoundToInt(Time.deltaTime)); 
            await Task.Delay(Mathf.RoundToInt(Time.deltaTime*1000f)); 
            elapsedTime -= Time.deltaTime;
            canchientext.text = elapsedTime.ToString();
            float fillAmount = elapsedTime / timeSkill;
            SkillPanell.fillAmount = fillAmount;
            if (fillAmount <= 0.05 )
            {
                Debug.LogError("Da Break");
                canchientext.text = "";
                PlayerControler.Instance.NextHit = true;
                GameManager.Instance.FindObjectInChildren(PlayerControler.Instance.gunShot, "SkillAnim").GetComponent<PolygonCollider2D>().enabled = false;
                break;
            }
        }

        SkillPanell.fillAmount = 0f;
    }
    private void Update()
    {
        //SkillPanell.fillAmount = Mathf.MoveTowards(SkillPanell.fillAmount, _target, 3 * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SkillPanel.SetImageSelected(gameObject);
        SkillManager.Instance.UpdateUseSkill(ThisSkill);
    }
}
