using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour, IObservable
{

    Flyweight _flyweight;
    Vector3 _dir;
    public ObjectPool<Asteroids> pool;
    float currentHealth;
    public int spawnType;

    public Asteroids(Vector3 dir, ObjectPool<Asteroids> poolAsteroid)
    {
        _dir = dir;
        pool = poolAsteroid;
    }

    public Asteroids SetInitPos(Vector3 pos)
    {
        transform.position = pos;
        currentHealth = _flyweight.maxHealth;     
        return this;
    }






    public Asteroids SetDir()
    {
        _dir = FindObjectOfType<ShipController>().transform.position - transform.position;
        return this;
    }

    public Asteroids SetDir(Vector3 dir)
    {
        _dir = dir;
        return this;
    }

    public Asteroids SetPool(ObjectPool<Asteroids> poolAsteroid)
    {
        pool = poolAsteroid;
        return this;
    }

    public Asteroids SetSpawnAsteroid(int n)
    {    
        spawnType = n - 1;       
        return this;
    }

    public Asteroids SetFyweight(Flyweight fw)
    {
        _flyweight = fw;
        return this;
    }

    public Vector3 Dir
    {
        get
        {
            return _dir;
        }
    }

    public Flyweight Fyweight
    {
        get
        {
            return _flyweight;
        }
    }


    private void Update()
    {
        Alive();
        transform.position += _flyweight.speed * _dir.normalized * Time.deltaTime;
        Rotarion();
    }


    void Rotarion() 
    {
        transform.Rotate(new Vector3(Time.deltaTime * _flyweight.rotateSpeed, Time.deltaTime * _flyweight.rotateSpeed, Time.deltaTime * _flyweight.rotateSpeed), Space.Self);
    }


    public virtual void Alive()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        PointSystem.Instance.TakeScore = _flyweight.score;
        InstantiateAsteroids(Random.Range(2, 5));
        TurnOff(this);
        pool.Recycle(this);
    }


    public virtual void InstantiateAsteroids(int cantAsteroids)
    {
        if (spawnType < 0) return;
        for (int i = 1; i < cantAsteroids; i++)
        {
            AsteroidSpawner.Instance.poolsList[spawnType].GetObj()
                                                         .SetInitPos(transform.position)
                                                         .SetPool(AsteroidSpawner.Instance.poolsList[spawnType])
                                                         .SetSpawnAsteroid(spawnType)
                                                         .SetDir();                                           
        }
    }

    public float TakeDamage
    {
        set
        {
            currentHealth -= value;
        }
    }


    public static void TurnOn(Asteroids asteroids)
    {
        asteroids.gameObject.SetActive(true);
        AsteroidManager.instance.ast.Add(asteroids);
    }

    public static void TurnOff(Asteroids asteroids)
    {             
        asteroids.gameObject.SetActive(false);
        AsteroidManager.instance.ast.Remove(asteroids);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            currentHealth -= other.GetComponent<Bullet>().TakeDamage;
        }

        if(other.gameObject.layer == 9)
        {
            PointSystem.Instance.TakeScore = _flyweight.score;
            TurnOff(this);
            pool.Recycle(this);
        }

        if (other.gameObject.layer == 11)
        {
            TurnOff(this);
            pool.Recycle(this);
        }


    }



    public void Notify(string eventName)
    {
        throw new System.NotImplementedException();
    }

    public void SubEvent(IObserver obs)
    {
        throw new System.NotImplementedException();
    }

    public void UnSubEvent(IObserver obs)
    {
        throw new System.NotImplementedException();
    }
}
