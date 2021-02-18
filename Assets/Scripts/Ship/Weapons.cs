using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    public Flyweight _flyweight;
    public List<Transform> spawnPos = new List<Transform>();
    public ObjectPool<Bullet> bulletPool;
    public Bullet prefab;
    public int maxLevel = 2;
    public int currentLevel = 0;
    public int prevLevel;

    public abstract void Shoot();

    public Weapons SetFlyweight(Flyweight flyweight)
    {
        _flyweight = flyweight;
        return this;
    }
}
