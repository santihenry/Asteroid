using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightBullet : MonoBehaviour
{
    public static Flyweight simpleBullet = new Flyweight
    {
        speed = 200,
        lifeTime = 2,
    };

    public static Flyweight rocket = new Flyweight
    {
        speed = 150,
        lifeTime = 5,
    };
}
