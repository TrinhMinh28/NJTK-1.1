using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using System;

public class ClickyButton : MonoBehaviour ,IPointerUpHandler, IPointerDownHandler
{

    [SerializeField] private Image _img;
    [SerializeField] private Sprite _Defau,Presser;
    [SerializeField] private AudioClip _Compress, _UnCompress;
    [SerializeField] private AudioSource _AuSource;

    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = Presser;
        _AuSource.PlayOneShot(_Compress);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
         _img.sprite = _Defau;
        _AuSource.PlayOneShot(_UnCompress);
    }
 
  
}
