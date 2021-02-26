using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Granadas : Weapons, IHandler
{
    float currentTime;
    List<Bullet> listBullets = new List<Bullet>();
    Queue<Bullet> bulletSatck = new Queue<Bullet>();

    bool canDropGranade = true;
    bool canExplode = false;
    float time;
    IHandler next;
    int maxGranades = 30;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(FactoryGranadas, Bullet.TurnOn, Bullet.TurnOff, 5, true);
        SetFlyweight(FlyweightWeapon.Granadas);
    }

    
    void Update()
    {
        currentTime += Time.deltaTime;
        time += Time.deltaTime;
    }


    public void Exlotion()
    {      
        StartCoroutine(Sequence(bulletSatck));
    }

    IEnumerator Sequence(Queue<Bullet> l)
    {
        while (bulletSatck.Any())
        {
            Handle();
            yield return new WaitForSeconds(_flyweight.fireRate);
        }
        canDropGranade = true;
    }

    public override void Shoot()
    {
        if (_flyweight.fireRate - currentTime <= 0 && canDropGranade && bulletSatck.Count < maxGranades)
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

    public IHandler SetNext(IHandler nextHandler)
    {
        next = nextHandler;
        return next;
    }

    public object Handle()  
    {
        if (bulletSatck.Any())
        {
            canDropGranade = false;
            var explode = bulletSatck.Dequeue();
            if (explode != null)
            {
                explode.Explotion();
            }
            return explode;
        }
        else
        {
            time = 0;
            return next.Handle();
        }
    }

}
