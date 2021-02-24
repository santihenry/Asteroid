using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightAsteroid 
{
    public static Flyweight asteroidSmall = new Flyweight
    {
        maxHealth = 100,
        speed = Random.Range(16, 20),
        score = 10,
        rotateSpeed = Random.Range(16, 50),
        type = TypesAsteroids.S
    };

    public static Flyweight asteroidMedium = new Flyweight
    {
        maxHealth = 200,
        speed = Random.Range(16, 20),
        score = 20,
        rotateSpeed = Random.Range(16, 50),
        type = TypesAsteroids.M
    };

    public static Flyweight asteroidBig = new Flyweight
    {
        maxHealth = 300,
        speed = Random.Range(16, 20),
        score = 25,
        rotateSpeed = Random.Range(16, 50),
        type = TypesAsteroids.L
    };

    public static Flyweight asteroidExtraBig = new Flyweight
    {
        maxHealth = 500,
        speed = Random.Range(16, 20),
        score = 30,
        rotateSpeed = Random.Range(16, 50),
        type = TypesAsteroids.XL
    };

}
