using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using KennethDevelops.Serialization;


public class LifeSystem : MonoBehaviour, IObserver
{
    public bool lose = false;
    public TMP_Text lifesTxt;
    public int lifes = 2;

    public static LifeSystem _intance;

    public LifeSystem Instance
    {
        get
        {
            return _intance;
        }
    }



    public void SaveStats()
    {
        SaveManager.SaveLifeStats(this);
    }

    public void LoadStats()
    {

        LifeData lifeeData = SaveManager.LoadLifeStats();

        lifes = lifeeData.lifes;

    }


    void Start()
    {
        var _ship = FindObjectOfType<ShipController>();
        _ship.SubEvent(this);
        if (_ship == null) _ship.UnSubEvent(this);
    }

   
    void Update()
    {
        lifesTxt.text = $"  {lifes}";
        if(lifes <= 0)
        {
            lose = true;
            GameManager.Instance.GameOver();
        }


    }


    public void OnNotify(string eventName)
    {
       if(eventName == "LoseLife")
       {
            lifes--;
       }
    }

}
