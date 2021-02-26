using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointSystem : MonoBehaviour, IObserver
{
    public TMP_Text scoreTxt;
    public int score;
    public static PointSystem Instance { get; private set; }


    public void SaveStats()
    {
        SaveManager.SaveScoreStats(this);
    }

    public void LoadStats()
    {

        ScoreData scoreData = SaveManager.LoadScoreStats();

        score = scoreData.score;

    }




    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Update()
    {
        scoreTxt.text = $"  {score}";

        if (score > 500 || Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.Instance.Win();
        }

    }


    public void OnNotify(string eventName)
    {
        throw new System.NotImplementedException();
    }

    public void OnNotify(string eventName, int scr)
    {
        if (eventName == "SumarPuntos")
        {
            score += scr;
        }
    }


    public int TakeScore
    {
        set
        {
            score += value;
        }
    }


}
