using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    public List<GameObject> list;


    public void SetImageSelected(GameObject gameObject)
    {
        foreach (GameObject Object in list)
        {
            if (gameObject.name == Object.name)
            {
                Object.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                Object.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
