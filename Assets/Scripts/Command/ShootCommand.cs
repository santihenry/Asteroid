using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : ICommand
{
    //List<Weapons> weaponsList = new List<Weapons>();
    //Weapons weapon;
    //static int currentWeapon;


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
