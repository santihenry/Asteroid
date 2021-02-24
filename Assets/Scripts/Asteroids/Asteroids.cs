using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{

    Flyweight _flyweight;
    Vector3 _dir;
    public ObjectPool<Asteroids> pool;
    float currentHealth;
    public int spawnType;
    //public TypesAsteroids type;



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


    private void Awake()
    {
        AsteroidManager.instance.ast.Add(this);
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

        if(other.gameObject.layer == 9)
        {
            TurnOff(this);
            pool.Recycle(this);
        }

        if (other.gameObject.layer == 11)
        {
            TurnOff(this);
            pool.Recycle(this);
        }


    }

}
