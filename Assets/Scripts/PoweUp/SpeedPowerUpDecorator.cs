using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUpDecorator : IDecorator<ShipModel>
{
    public IDecorator<ShipModel> next;

    public void Execute(ShipModel data)
    {    
        if(data.powerUpSpeed)
        {
            data.currentTime = 0;
            data.speed = data.speed + data.speed;
            data.turbo.SetActive(true);
        }
       // if (next != null) next.Execute(data);
    }

    public void Stop(ShipModel data)
    {
        data.turbo.SetActive(false);
        data.speed = data.speed - data.speed / 2;
        data.powerUpSpeed = false;
    }
}
