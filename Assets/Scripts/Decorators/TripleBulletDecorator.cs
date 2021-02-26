using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBulletDecorator : IDecorator<ShipModel>  //MyA1-P2
{
    public void Execute(ShipModel data)
    {
        data.weapon.currentLevel = 2;
    }

    public void Stop(ShipModel data)
    {
      
    }
}
