using System;
using UnityEngine;

[Serializable]
public class ScoreData 
{
    public int score;
    public ScoreData(PointSystem s)
    {
        score = s.score;
    }
}
