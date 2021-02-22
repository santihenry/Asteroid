using UnityEngine;
using System;
using System.Collections.Generic;


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
    public TypesAsteroids types;
    public SerializableVector3 dir;
    public int spawnType;

}



