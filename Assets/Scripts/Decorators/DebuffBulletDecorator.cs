using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffBulletDecorator : IDecorator<ShipModel>      //MyA1-P2
{
    public void Execute(ShipModel data)
    {
        data.weapon.currentLevel = 0;
    }

    public void Stop(ShipModel data)
    {

    }
}
