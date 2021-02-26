using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointSystem : MonoBehaviour, IObserver
{
    public  TMP_Text scoreTxt;
    int score;

    public void OnNotify(string eventName)
    {
        if(eventName == "TakeScore")
        {
            score += 20;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.Instance.Win();
        }

    }



}
