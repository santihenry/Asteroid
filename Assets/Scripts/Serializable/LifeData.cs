using UnityEngine;
using System;

[Serializable]
public class LifeData 
{
    public int lifes;
    public LifeData(LifeSystem life)
    {
        lifes = life.lifes;
    }
}
