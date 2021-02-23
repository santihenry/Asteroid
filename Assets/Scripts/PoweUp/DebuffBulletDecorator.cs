using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffBulletDecorator : IDecorator<ShipModel>
{
    public void Execute(ShipModel data)
    {
        data.weapon.currentLevel = 0;
    }

    public void Stop(ShipModel data)
    {

    }
}
