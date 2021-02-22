using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPowerUp : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] PowerUP;
    int randomSpawnPoint, randomPoweUp;

    float _currentTime;

    
    private void Update()
    {
        _currentTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        if(_currentTime >= 3)
        {
            if(FindObjectOfType<PowerUp>() == null)
                SpawnPowerUp();

            _currentTime = 0;    
        }
    }
    

    void SpawnPowerUp()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        randomPoweUp = Random.Range(0, PowerUP.Length);
        Instantiate(PowerUP[randomPoweUp], spawnPoints[randomSpawnPoint].position, Quaternion.identity);

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        foreach (var spawn in transform.GetComponentsInChildren<Transform>())
        {
            Gizmos.DrawSphere(spawn.position, 2.5f);

        }
    }

}
