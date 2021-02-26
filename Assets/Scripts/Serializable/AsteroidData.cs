using UnityEngine;
using System;

public enum TypesAsteroids 
{
    S,
    M,
    L,
    XL
}

[Serializable]
public class AsteroidData
{

    public SerializableVector3 position;
    public SerializableRotation rotation;
    public TypesAsteroids type;
    public SerializableVector3 dir;
    public int spawnType;

}



