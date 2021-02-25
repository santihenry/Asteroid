using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightWeapon   //MyA1-P4
{
    public static Flyweight machineGun = new Flyweight
    {
        fireRate = .13f,
        maxLevel = 2
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
