using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldPowerUp : IDecorator<ShipModel>
{
    public IDecorator<ShipModel> next;

    public void Execute(ShipModel data)
    {
        if (data.powerUp)
        {
            data.currentTime = 0;
            data.forceField.SetActive(true);
        }    
    }
    public void Stop(ShipModel data)
    {
        data.forceField.SetActive(false);
    }
}
