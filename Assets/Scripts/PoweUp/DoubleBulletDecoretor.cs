using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBulletDecoretor : IDecorator<ShipModel>
{
    public void Execute(ShipModel data)
    {
        data.weapon.currentLevel = 1;
    }

    public void Stop(ShipModel data)
    {
       
    }
}
