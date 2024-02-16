using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowExp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnDisable()
    {

    }

    // Update is called once per frame
    public void CloseShowExp()
    {
        Debug.Log("CloseShowExp");
        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.GetComponent<TextMeshProUGUI>().text = "";
    }
}
