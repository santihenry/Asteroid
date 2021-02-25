using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCommand : ICommand     //MyA1-P3
{
    public void Execute(GameObject obj)
    {
        obj.transform.Rotate(0, obj.GetComponent<ShipModel>().turnSpeed * Time.deltaTime, 0);
    }

    public void Undo(GameObject obj)
    {
        obj.transform.Rotate(0, -obj.GetComponent<ShipModel>().turnSpeed * Time.deltaTime, 0);
    }

    public void Init(GameObject obj)
    {
        throw new System.NotImplementedException();
    }
}
