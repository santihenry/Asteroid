using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LifeSystem : MonoBehaviour, IObserver
{
    public bool lose = false;
    public TMP_Text lifesTxt;
    public int lifes = 6;

    public static LifeSystem _intance;

    public LifeSystem Instance
    {
        get
        {
            return _intance;
        }
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
