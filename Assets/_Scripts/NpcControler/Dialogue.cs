using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;
    [SerializeField] private GameObject _winDows , _inDicator, _SetMissionEffectTrans;
    public TMP_Text DialogueText;
    public TMP_Text _NpcName;
    public float WritingSpeed;
    private int inDex;
    private int charIndex;
    private bool startTed;
    private bool waitNextText = false;
    [SerializeField] private List<string> taskList = new List<string>();

    private void Start()
    {
        instance = this;
        SetWindows(false);
        SetIndicator(false);
        SetMissionEffectTrans(false);
    }
    public void SetWindows(bool _Set)
    {
        _winDows.SetActive(_Set);
    }
    public void SetIndicator(bool _Set)
    {
        _inDicator.SetActive(_Set);
    }
    public void SetMissionEffectTrans(bool _Set)
    {
        _SetMissionEffectTrans.SetActive(_Set);
        //MissionEffect
    }
    public void SetMissionEffectTransWith(bool _Set,string BoolTrue, string BoolFlase)
    {
        _SetMissionEffectTrans.SetActive(_Set);
        _SetMissionEffectTrans.gameObject.GetComponentInChildren<Animator>().SetBool(BoolTrue, _Set);
        _SetMissionEffectTrans.gameObject.GetComponentInChildren<Animator>().SetBool(BoolFlase, !_Set);
        //MissionEffect
    }
    public void SetNpcName(string Name)
    {
        _NpcName.text = Name;
    }
    public void StartDialogue(List<string> _taskList)
    {
        if (_taskList == null)
        {
            taskList.Add("");
            Debug.Log("ListRong");
        }
        else
        {
            taskList.Clear();
            foreach (var item in _taskList)
            {
                taskList.Add(item.ToString());
            }
        }
        if (startTed)
        {
            return;
        }
        else
        {
            startTed = true;
            SetWindows(true);
            SetIndicator(false);
            getDialog(0);
        }
    
    }
     public void EndDialogue ()
    {
        StopAllCoroutines();
        SetWindows(false);
        startTed = false;
        waitNextText = false;
    }
    public void getDialog(int i) 
    {
        inDex = i;
        charIndex = 0;
        DialogueText.text = string.Empty;
        StartCoroutine(Writing());
    }

    private IEnumerator Writing()
    {
        yield return new WaitForSeconds(WritingSpeed);
       // string curentDialogue = _dialogues[inDex];
        string curentDialogue1 = taskList[inDex];
        DialogueText.text += curentDialogue1[charIndex];
        charIndex++;
        if (charIndex <curentDialogue1.Length)
        {
            yield return new WaitForSeconds(WritingSpeed);
            StartCoroutine(Writing());
        }
        else
        {
            waitNextText = true;
        }
    }
    void  AgainTalking()
    {
        if (!startTed) return;
        //if (waitNextText && Input.GetKeyDown(KeyCode.Space)) // nhấn Space để chạy tiếp nhưng có lẽ k cần
        if (waitNextText)
        {
            waitNextText = false;
            inDex++;
            if (inDex < taskList.Count) // Vẫn còn câu thoại
            {
                getDialog(inDex);
            }
            else
            {
                // Kết thúc thoại
                inDex = 0;
                EndDialogue();
                SetIndicator(true);
            }

        }
    }
    private void Update()
    {
        AgainTalking();
    }
}
