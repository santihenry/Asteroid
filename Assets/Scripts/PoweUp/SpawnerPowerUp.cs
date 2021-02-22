using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPowerUp : MonoBehaviour
{
    public Vector2 spawnArea;
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
        randomPoweUp = Random.Range(0, PowerUP.Length);
        var position = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 0, Random.Range(-spawnArea.y, spawnArea.y));
        Instantiate(PowerUP[randomPoweUp], position, Quaternion.identity);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan / 3;
        Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(spawnArea.x * 2, 0, spawnArea.y * 2));

    }

}
