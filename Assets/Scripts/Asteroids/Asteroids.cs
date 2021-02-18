using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{

    Flyweight _flyweight;
    Vector3 _dir;
    public ObjectPool<Asteroids> pool;
    float currentHealth;
    int spawnType;



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


    private void Update()
    {
        Alive();
        transform.position += _flyweight.speed * _dir.normalized * Time.deltaTime;
    }

    public virtual void Alive()
    {
        if (currentHealth <= 0)
        {         
            InstantiateAsteroids(Random.Range(2, 5));
            TurnOff(this);
            pool.Recycle(this);
        }
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


    public static void TurnOn(Asteroids asteroids)
    {
        asteroids.gameObject.SetActive(true);
    }

    public static void TurnOff(Asteroids asteroids)
    {             
        asteroids.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            currentHealth -= other.GetComponent<Bullet>().TakeDamage;
        }
    }

}
