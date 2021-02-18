using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapons
{

    float currentTime;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(FactoryMachineGun, Bullet.TurnOn, Bullet.TurnOff, 5,true);
        SetFlyweight(FlyweightWeapon.machineGun);
    }

    public void Update()
    {
        currentTime += Time.deltaTime;
    }

    public override void Shoot()
    {
        if(_flyweight.fireRate - currentTime <= 0)
        {
            var bullet = bulletPool.GetObj().SetInitPos(spawnPos.position)
                                            .SetDir(transform.parent.forward)
                                            .SetPool(bulletPool);
            currentTime = 0;
        } 
    }

    public Bullet FactoryMachineGun()
    {
        var b = Instantiate(prefab).SetFlyweight(FlyweightBullet.simpleBullet);
        return b;
    }

}
