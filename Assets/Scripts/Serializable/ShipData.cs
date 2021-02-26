using System;
using UnityEngine;

[Serializable]
public class ShipData
{
    public int currentIndexWeapon;
    public SerializableVector3 position;
    public SerializableRotation rotation;
    public bool powerUpSeed, poewerUpField;
    public int weaponLvl;

    public ShipData(ShipModel ship)
    {
        currentIndexWeapon = ship.currentWeapon;
        position = new SerializableVector3(ship.transform.position);
        rotation = new SerializableRotation(ship.transform.rotation);
        poewerUpField = ship.powerUp;
        poewerUpField = ship.powerUpSpeed;
        weaponLvl = ship.weapon.currentLevel;
    }

}


[Serializable]
public class SerializableVector3
{

    public float x;
    public float y;
    public float z;

    public SerializableVector3(Vector3 vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}


[Serializable]
public class SerializableRotation
{
    public float x, y, z, w;

    public SerializableRotation(Quaternion q)
    {
        x = q.x;
        y = q.y;
        z = q.z;
        w = q.w;
    }


    public Quaternion ToQuaternion()
    {
        return new Quaternion(x, y, z, w);
    }

}
