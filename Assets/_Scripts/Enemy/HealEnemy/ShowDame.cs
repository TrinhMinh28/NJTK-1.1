using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShowDame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnDisable()
    {
        
    }
   
    // Update is called once per frame
    public void CloseShowDame()
    {
        Debug.Log("CloseShowDame");
        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.GetComponent<TextMeshProUGUI>().text = "";
    }
}
