using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Granadas : Weapons
{
    float currentTime;
    List<Bullet> listBullets = new List<Bullet>();
    Queue<Bullet> bulletSatck = new Queue<Bullet>();

    bool canDropGranade = true;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(FactoryGranadas, Bullet.TurnOn, Bullet.TurnOff, 5, true);
        SetFlyweight(FlyweightWeapon.Granadas);
    }

    
    void Update()
    {
        currentTime += Time.deltaTime;
    }


    public void Exlotion()
    {
        if (!bulletSatck.Any()) return;
        StartCoroutine(Sequence(bulletSatck));
        canDropGranade = false;
    }

    IEnumerator Sequence(Queue<Bullet> l)
    {
        while(bulletSatck.Any())
        {
            var c4 = bulletSatck.Dequeue();
            c4.Explotion();
            yield return new WaitForSeconds(.12f);
        }
        canDropGranade = true;
    }

    public override void Shoot()
    {
        if (_flyweight.fireRate - currentTime <= 0 && canDropGranade && bulletSatck.Count < 20)
        {
            var bullet = bulletPool.GetObj().SetInitPos(spawnPos[0].position)
                                            .SetDir(transform.parent.forward)
                                            .SetPool(bulletPool);

            listBullets.Add(bullet);
            bulletSatck.Enqueue(bullet);

            currentTime = 0;
        }
    }



    public Bullet FactoryGranadas()
    {
        var b = Instantiate(prefab).SetFlyweight(FlyweightBullet.Granadas);
        return b;
    }
}
