using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointSystem : MonoBehaviour, IObserver
{
    public TMP_Text scoreTxt;
    int score;
    public static PointSystem Instance { get; private set; }


    public void OnNotify(string eventName)
    {
        if (eventName == "TakeScore")
        {
            score += 20;
        }
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


    public int TakeScore
    {
        set
        {
            score += value;
        }
    }


}
