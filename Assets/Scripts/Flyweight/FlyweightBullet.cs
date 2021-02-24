using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightBullet  
{
    public static Flyweight simpleBullet = new Flyweight
    {
        speed = 200,
        damage = 20,
        lifeTime = 2,
        shotSound = Sounds.shootLazer,
        hitSound = Sounds.hit,
        explosiveBullet = false,
    };

    public static Flyweight rocket = new Flyweight
    {
        speed = 150,
        damage = 100,
        lifeTime = 5,
        shotSound = Sounds.shootRocket,
        hitSound = Sounds.hit,
        explosiveBullet = false,
    };

    public static Flyweight superRocket = new Flyweight
    {
        speed = 160,
        damage = 300,
        lifeTime = 5,
        shotSound = Sounds.shootRocket,
        hitSound = Sounds.hit,
        explosiveBullet = false,
    };

    public static Flyweight Granadas = new Flyweight
    {
        speed = 0,
        damage = 200,
        lifeTime = 100,
        shotSound = Sounds.shootRocket,
        hitSound = Sounds.hit,
        explosiveBullet = true,
        explotionRadius = 12
    };

}
