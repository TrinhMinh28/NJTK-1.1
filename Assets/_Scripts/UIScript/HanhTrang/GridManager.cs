using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private int _With, _Height;
    [SerializeField] private TitleEvent _titleFrefab;
    [SerializeField] private Transform _cam; // Dung để căn cỉnh các ô vào đúng vị trí cam hoặc là ở vị trí mong muốn
    private Dictionary<Vector2, TitleEvent> _titles;
    [SerializeField] float baseExp = 100;
    [SerializeField] float exponent = 10;
    [SerializeField] float currentLevel = 50;
    private void Start()
    {
        GenerateGrid();
        TinhExxp();
    }

    private void TinhExxp()
    {

        double requiredExp = baseExp * (Math.Pow(currentLevel + 1, exponent)) - baseExp * (Math.Pow(currentLevel, exponent));

        string text = "Kinh nghiệm cần thiết để lên từ cấp độ "+ currentLevel+" lên cấp đ "+(currentLevel + 1) +" là :" + requiredExp;
        Debug.Log(text);
    }

    void GenerateGrid()
    {
        _titles = new Dictionary<Vector2, TitleEvent>();
        for (int x = 0; x < _With; x++)
        {
            for (int y = 0; y < _Height; y++)
            {
                var SpawnedTitle = Instantiate(_titleFrefab, new Vector3(x, y), Quaternion.identity, _parent.transform);
                SpawnedTitle.name = $"Title {x} {y}";
                var isOffset = (x % 2 == 0 && y %2 !=0) || (x % 2 != 0 && y % 2 == 0)  ;
                SpawnedTitle.Init(isOffset);

                _titles[ new Vector2(x, y)]= SpawnedTitle;
            }
        }
        _cam.position = new Vector3((float) _With /2 - 0.5f, (float) _Height / 2 - 0.5f,-10);// -10 cần thay đổi nếu bị chìm quá sâu
    }
     public TitleEvent GetTitlesAtPosition( Vector2 Pos ) // Sử dụng để lấy vị trí các ô nếu cần ! 
    {
        if (_titles.TryGetValue( Pos,out var Title))
        {
            return Title;
        }
        return null;
    }
}
