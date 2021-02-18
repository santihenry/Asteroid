using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightAsteroid : MonoBehaviour
{
    public static Flyweight asteroidSmall = new Flyweight
    {
        maxHealth = 100,
        speed = Random.Range(16, 20),
        score = 10,
    };

    public static Flyweight asteroidMedium = new Flyweight
    {
        maxHealth = 200,
        speed = Random.Range(16, 20),
        score = 20,
    };

    public static Flyweight asteroidBig = new Flyweight
    {
        maxHealth = 300,
        speed = Random.Range(16, 20),
        score = 25,
    };

    public static Flyweight asteroidExtraBig = new Flyweight
    {
        maxHealth = 500,
        speed = Random.Range(16, 20),
        score = 30,
    };

}
