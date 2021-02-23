using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : ICommand
{
    List<Weapons> weaponsList = new List<Weapons>();
    Weapons weapon;
    static int currentWeapon;


    public void Execute(GameObject obj)
    {
        weaponsList[currentWeapon].Shoot();
    }

    public void Undo(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    public void Init(GameObject obj)
    {
        weaponsList.Add(obj.GetComponentInChildren<MachineGun>());
        weaponsList.Add(obj.GetComponentInChildren<RocketGun>());
        weaponsList.Add(obj.GetComponentInChildren<Granadas>());
    }
}
