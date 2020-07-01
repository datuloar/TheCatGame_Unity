using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public const int MaxLenght = 20;
    private const string _saveName = "HighScore.sav";

    private static ScoreList _scoreList;
    [SerializeField] private ScoreCell[] _cells;

    private void Awake()
    {
        _scoreList = Saver.LoadData<ScoreList>(_saveName);
        if (_scoreList == null)
        {
            _scoreList = new ScoreList();
            Saver.SaveData(_scoreList, _saveName);
        }
        FillTable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Application.isEditor)
            {               
                ClearTable();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Application.isEditor)
            {
                foreach (var num in _scoreList.Score)
                {
                    Print(num);
                }
            }
        }
    }

    public static void AddScore(int value)
    {
        _scoreList.Score.Add(value);

        _scoreList.Score = (from i in _scoreList.Score
                           orderby i descending
                           select i).ToList();

        while (_scoreList.Score.Count > MaxLenght)
        {
            _scoreList.Score.RemoveAt(_scoreList.Score.Count - 1);
        }
        
        Saver.SaveData(_scoreList, _saveName);
    }

    public void ClearTable()
    {
        Saver.DeleteFile(_saveName);
        _scoreList = new ScoreList();
        Saver.SaveData(_scoreList, _saveName);
        FillTable();
    }

    private void FillTable()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].SetData(i + 1, _scoreList.Score[i]);
        }
    }
}
