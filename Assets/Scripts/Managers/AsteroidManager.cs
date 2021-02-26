using KennethDevelops.Serialization;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager instance;
    public List<Asteroids> ast = new List<Asteroids>();
    int indexAstList;

    private void Awake()
    {
        instance = this;
    }




    private void Update()
    { 
        for (int i = 0; i < ast.Count; i++)
        {
            if (ast[i] == null) ast.RemoveAt(i);
        }
    }



    public void Save()
    {
        indexAstList = 0;
        SaveAsteroids data = new SaveAsteroids();

        for (int i = 0; i < ast.Count; i++)
        {
            AsteroidData asteroidData = new AsteroidData();
            asteroidData.position = new SerializableVector3(ast[i].transform.position);
            asteroidData.dir = new SerializableVector3(ast[i].Dir);
            asteroidData.type = ast[i].Fyweight.type;

            data.asteroidDataList.Add(asteroidData);
            indexAstList += 1;
        }

        data.SaveBinary($"{Application.dataPath}/Asteroids_stats.dll");
    }

    public void Load()
    {
        for (int i = 0; i < ast.Count; i++)
        {
            DestroyImmediate(ast[i].gameObject);
        }

        var data = BinarySerializer.LoadBinary<SaveAsteroids>($"{Application.dataPath}/Asteroids_stats.dll");

        for (int i = 0; i < data.asteroidDataList.Count; i++)
        {
            Vector3 pos = data.asteroidDataList[i].position.ToVector3();
            Vector3 dir = data.asteroidDataList[i].dir.ToVector3();



            AsteroidSpawner.Instance.poolsList[(int)data.asteroidDataList[i].type].GetObj()
                                                .SetInitPos(data.asteroidDataList[i].position.ToVector3())
                                                .SetPool(AsteroidSpawner.Instance.poolsList[(int)data.asteroidDataList[i].type])
                                                .SetSpawnAsteroid((int)data.asteroidDataList[i].type)
                                                .SetDir(data.asteroidDataList[i].dir.ToVector3());
        }

        Debug.Log("Count dataList:      " + data.asteroidDataList.Count);
        for (int i = 0; i < data.asteroidDataList.Count; i++)
        {
            Debug.Log($"Position:       {data.asteroidDataList[i].position.ToVector3()}   | Type:     {data.asteroidDataList[i].type}");
        }
    }
}

[System.Serializable]
public class SaveAsteroids
{
    public List<AsteroidData> asteroidDataList = new List<AsteroidData>();
}
