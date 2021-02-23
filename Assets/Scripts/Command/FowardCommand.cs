using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FowardCommand : ICommand
{
    public void Execute(GameObject obj)
    {
        obj.transform.position += obj.transform.forward * obj.GetComponent<ShipModel>().speed * Time.deltaTime;
        obj.GetComponent<ShipModel>().aceleration = 1;
    }

    public void Undo(GameObject obj)
    {
        obj.transform.position -= obj.transform.forward * obj.GetComponent<ShipModel>().speed * Time.deltaTime;
        obj.GetComponent<ShipModel>().aceleration = 1;
    }

    public void Init(GameObject obj)
    {
        throw new System.NotImplementedException();
    }
}
