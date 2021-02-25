using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerAdapter ////MyA1-P1
{
    public static void Suscribe (string eventID,EventManager.Callback callback)
    {
        EventManager.Instance.AddListener(eventID, callback);
    }

    public static void Unsuscribe (string eventID,EventManager.Callback callback)
    {
        EventManager.Instance.RemoveListener(eventID, callback);
    }

    public static void Trigger (string eventID,params object[] parameters)
    {
        List<object> _parameters = new List<object>();
        _parameters.AddRange(parameters);

        EventManager.Instance.ExecuteCallback(eventID, _parameters);
    }
}
