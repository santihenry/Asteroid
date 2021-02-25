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
        prevLevel = currentLevel;
    }

    public void Update()
    {
        currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            prevLevel = currentLevel;
            currentLevel--;
            if (currentLevel < 0)
            {
                prevLevel = currentLevel;
                currentLevel = _flyweight.maxLevel;

            }       
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            prevLevel = currentLevel;
            currentLevel++;
            if (currentLevel > _flyweight.maxLevel)
            {
                prevLevel = currentLevel;
                currentLevel = 0;

            }
        }
    }

    public override void Shoot()
    {
        if(_flyweight.fireRate - currentTime <= 0)
        {
            for (int i = (currentLevel == 0 ? 0 : (currentLevel == 1 ? 1 : 0)); i < (currentLevel == 0 ? 1 : (currentLevel == 1 ? spawnPos.Count : spawnPos.Count)); i++)
            {
               bulletPool.GetObj().SetInitPos(spawnPos[i].position)
                                  .SetDir(transform.parent.forward)
                                  .SetPool(bulletPool);
            }
            currentTime = 0;
        } 
    }

    public Bullet FactoryMachineGun()
    {
        var b = Instantiate(prefab).SetFlyweight(FlyweightBullet.simpleBullet);
        return b;
    }

}
