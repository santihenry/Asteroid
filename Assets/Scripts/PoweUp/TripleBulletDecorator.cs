using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBulletDecorator : IDecorator<ShipModel>
{
    public void Execute(ShipModel data)
    {
        data.weapon.currentLevel = 2;
    }

    public void Stop(ShipModel data)
    {
      
    }
}
