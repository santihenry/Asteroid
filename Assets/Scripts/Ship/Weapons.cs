using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    public Flyweight _flyweight;
    public Transform spawnPos;
    public ObjectPool<Bullet> bulletPool;
    public Bullet prefab;

    public abstract void Shoot();

    public Weapons SetFlyweight(Flyweight flyweight)
    {
        _flyweight = flyweight;
        return this;
    }
}
