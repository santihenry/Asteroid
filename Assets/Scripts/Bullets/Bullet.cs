using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ObjectPool<Bullet> pool;
    Vector3 _dir;
    Flyweight _flyweight;
    float _currentTime;


    public Bullet(Vector3 dir,ObjectPool<Bullet> bulletPool)
    {
        _dir = dir;
        pool = bulletPool;
    }

    public Bullet SetPool(ObjectPool<Bullet> bulletPool)
    {
        pool = bulletPool;
        return this;
    }

    public Bullet SetInitPos(Vector3 pos)
    {
        transform.position = pos;
        return this;

    }

    public Bullet SetDir(Vector3 dir)
    {
        _dir = dir;
        return this;
    }

    public Bullet SetFlyweight(Flyweight flyweight)
    {
        _flyweight = flyweight;
        return this;
    }


    private void Update()
    {
        transform.position += _dir * _flyweight.speed * Time.deltaTime;
        Alive();
    }


    public virtual void Alive()
    {
        _currentTime += Time.deltaTime;
        if (_flyweight.lifeTime < _currentTime)
        {
            _currentTime = 0;
            TurnOff(this);
            pool.Recycle(this);
        }
    }


    public static void TurnOn(Bullet bullet) 
    { 
        bullet.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }






}
