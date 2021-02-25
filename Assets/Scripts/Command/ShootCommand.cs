using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : ICommand    //MyA1-P3
{

    ShipModel model;


    public void Execute(GameObject obj)
    {

        model.weapons[model.currentWeapon].Shoot();
    }

    public void Undo(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    public void Init(GameObject obj)
    {
        model = obj.GetComponent<ShipModel>();
    }
}
