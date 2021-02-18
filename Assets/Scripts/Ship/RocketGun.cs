using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGun : Weapons
{
    float currentTime;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(FactoryRocket, Bullet.TurnOn, Bullet.TurnOff, 5, true);
        SetFlyweight(FlyweightWeapon.rocket);
    }

    public void Update()
    {
        currentTime += Time.deltaTime;
    }

    public override void Shoot()
    {
        if (_flyweight.fireRate - currentTime <= 0)
        {
            var bullet = bulletPool.GetObj().SetInitPos(spawnPos.position)
                                            .SetDir(transform.parent.forward)
                                            .SetPool(bulletPool);
            currentTime = 0;
        }
    }

    public Bullet FactoryRocket()
    {
        var b = Instantiate(prefab).SetFlyweight(FlyweightBullet.rocket);
        return b;
    }
}
