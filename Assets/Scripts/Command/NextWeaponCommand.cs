using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWeaponCommand : ICommand
{
    ShipModel model;

    public void Execute(GameObject obj)
    {
        model.currentWeapon++;

        if (model.currentWeapon > model.weapons.Count - 1)
            model.currentWeapon = 0;

        model.weapon = model.weapons[model.currentWeapon];
    }

    public void Init(GameObject obj)
    {
        model = obj.GetComponent<ShipModel>();
    }

    public void Undo(GameObject obj)
    {
        model.currentWeapon--;
        if (model.currentWeapon < 0)
            model.currentWeapon = model.weapons.Count - 1;

        model.weapon = model.weapons[model.currentWeapon];
    }
}
