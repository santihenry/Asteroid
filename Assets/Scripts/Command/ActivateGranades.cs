using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGranades : ICommand
{
    ShipModel model;

    public void Execute(GameObject obj)
    {
        model.GetComponentInChildren<Granadas>().Exlotion();
         
    }


    public void Init(GameObject obj)
    {
        model = obj.GetComponent<ShipModel>();
    }

    public void Undo(GameObject obj)
    {
        throw new System.NotImplementedException();
    }
}
