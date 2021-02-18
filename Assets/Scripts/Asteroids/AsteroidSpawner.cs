using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public static AsteroidSpawner _instance;

    public List<Transform> spawnPos = new List<Transform>();
    public Asteroids smallPrefab,mediumPrefab,bigPrefab,extraBigPrefab;
    public ObjectPool<Asteroids> smallPool;
    public ObjectPool<Asteroids> mediumPool;
    public ObjectPool<Asteroids> bigPool;
    public ObjectPool<Asteroids> extrBigPool;

    public List<ObjectPool<Asteroids>> poolsList = new List<ObjectPool<Asteroids>>();

    public float spawnRate;
    float _currentTime;


    public static AsteroidSpawner Instance
    {
        get
        {
            return _instance;
        }
    }


    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {       
        smallPool = new ObjectPool<Asteroids>(FactorySmallAsteroid, Asteroids.TurnOn, Asteroids.TurnOff, 2, true);
        mediumPool = new ObjectPool<Asteroids>(FactoryMediumAsteroid, Asteroids.TurnOn, Asteroids.TurnOff, 2, true);
        bigPool = new ObjectPool<Asteroids>(FactoryBigAsteroid, Asteroids.TurnOn, Asteroids.TurnOff, 2, true);
        extrBigPool = new ObjectPool<Asteroids>(FactoryExtraBigAsteroid, Asteroids.TurnOn, Asteroids.TurnOff, 2, true);

        poolsList.Add(smallPool);
        poolsList.Add(mediumPool);
        poolsList.Add(bigPool);
        poolsList.Add(extrBigPool);

        

    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        if(spawnRate - _currentTime <= 0)
        {
            var randPool = Random.Range(0, poolsList.Count);
            poolsList[randPool].GetObj().SetInitPos(spawnPos[Random.Range(0, spawnPos.Count - 1)].position)
                                        .SetDir()
                                        .SetSpawnAsteroid(randPool)
                                        .SetPool(poolsList[randPool]);
             _currentTime = 0;
        }
    }

    public Asteroids FactorySmallAsteroid()
    {
        var b = Instantiate(smallPrefab).SetFyweight(FlyweightAsteroid.asteroidSmall);
        return b;
    }
    public Asteroids FactoryMediumAsteroid()
    {
        var b = Instantiate(mediumPrefab).SetFyweight(FlyweightAsteroid.asteroidMedium);
        return b;
    }

    public Asteroids FactoryBigAsteroid()
    {
        var b = Instantiate(bigPrefab).SetFyweight(FlyweightAsteroid.asteroidBig);
        return b;
    }

    public Asteroids FactoryExtraBigAsteroid()
    {
        var b = Instantiate(extraBigPrefab).SetFyweight(FlyweightAsteroid.asteroidExtraBig);
        return b;
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        foreach (var spawn in transform.GetComponentsInChildren<Transform>())
        {
            Gizmos.DrawSphere(spawn.position, 2.5f);

        }
    }

}
