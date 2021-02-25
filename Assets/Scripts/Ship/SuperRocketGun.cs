using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRocketGun : Weapons
{
    float currentTime;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(FactorySuperRocket, Bullet.TurnOn, Bullet.TurnOff, 5, true);
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
            for (int i = 0; i < spawnPos.Count; i++)
            {
                var bullet = bulletPool.GetObj().SetInitPos(spawnPos[i].position)
                                                .SetDir(transform.parent.forward)
                                                .SetPool(bulletPool);
            }
            currentTime = 0;
        }
    }

    public Bullet FactorySuperRocket()
    {
        var b = Instantiate(prefab).SetFlyweight(FlyweightBullet.superRocket);
        return b;
    }
}
