using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleEvent : MonoBehaviour
{
    [SerializeField] private Color _BaseColor, _OffsetColor;
    [SerializeField] private SpriteRenderer _Render;
    [SerializeField] private GameObject _Hilight; // Sau này thay thế bằng ô vuong trống
    public void Init (bool isOffset)
    {
        _Render.color = isOffset ? _OffsetColor : _BaseColor;
    }
    private void OnMouseEnter()
    {
        _Hilight.SetActive(true);
    }
    private void OnMouseExit()
    {
        _Hilight.SetActive(false);
    }
}
