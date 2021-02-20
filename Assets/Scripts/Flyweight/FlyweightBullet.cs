using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightBullet : MonoBehaviour
{
    public static Flyweight simpleBullet = new Flyweight
    {
        speed = 200,
        damage = 20,
        lifeTime = 2,
        sound = Sounds.shootLazer
    };

    public static Flyweight rocket = new Flyweight
    {
        speed = 150,
        damage = 100,
        lifeTime = 5,
        sound = Sounds.shootRocket
    };

    public static Flyweight superRocket = new Flyweight
    {
        speed = 160,
        damage = 300,
        lifeTime = 5,
        sound = Sounds.shootRocket
    };

}
