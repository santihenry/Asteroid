using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightWeapon : MonoBehaviour
{
    public static Flyweight machineGun = new Flyweight
    {
        fireRate = .13f
    };

    public static Flyweight rocket = new Flyweight
    {
        fireRate = .3f
    };

    public static Flyweight Granadas = new Flyweight
    {
        fireRate = .2f
    };

}
