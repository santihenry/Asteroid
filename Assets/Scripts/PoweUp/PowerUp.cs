using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypesPoweUp
{
    turbo,
    forceField,
    downgradeWeapon,
    upgradeWeapon1,
    upgradeWeapon2,
}

public class PowerUp : MonoBehaviour
{
    float waitTime;
    [SerializeField]
    float startWaitTime;
    [SerializeField]
    float speed;
    List<Vector3> moveSpot = new List<Vector3>();
    int randomSpot;
    float _currentTime;

    #region    
    Vector3 randomMoveSpot;
    public Vector2 spawnArea;
    #endregion

    public TypesPoweUp type;


    private void Start()
    {
        var pos = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 0, Random.Range(-spawnArea.y, spawnArea.y));
        randomMoveSpot = pos;
        moveSpot.Add(pos);
    }

    virtual public void Update()
    {
        _currentTime += Time.deltaTime;

        MoveRandom();
    }

    virtual public  void MoveRandom()
    {
        transform.position = Vector3.MoveTowards(transform.position, randomMoveSpot, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, randomMoveSpot) < 0.2f)
        {
            if (waitTime - _currentTime <= 0)
            {
                var pos2 = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 0, Random.Range(-spawnArea.y, spawnArea.y));
                randomMoveSpot = pos2;
                _currentTime = 0;
                moveSpot.Add(pos2);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ShipController>() || other.GetComponent<Bullet>())
        {
            if (other.gameObject.GetComponent<ShipController>())
            {
                var ship = other.gameObject.GetComponent<ShipController>().GetModel;
                if (type == TypesPoweUp.turbo)
                {                   
                    ship.powerUpSpeed = true;
                    ship.speedDecorator.Execute(ship);
                }
                if (type == TypesPoweUp.forceField)
                {
                    ship.powerUp = true;
                    ship.forceFieldDecorator.Execute(ship);
                }
                if (type == TypesPoweUp.upgradeWeapon1)
                    ship.weapon.currentLevel = 1;
                if (type == TypesPoweUp.upgradeWeapon2)
                    ship.weapon.currentLevel = 2;
                if (type == TypesPoweUp.downgradeWeapon)
                    ship.weapon.currentLevel = 0;
            }
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {    
        Gizmos.color = Color.green;
        if(moveSpot.Count > 0)
            Gizmos.DrawSphere(moveSpot[0], 3f);

        Gizmos.color = Color.red;
        for (int i = 0; i < moveSpot.Count-1; i++)
        {
            if(moveSpot.Count > 1)
            {
                Gizmos.DrawLine(moveSpot[i], moveSpot[i + 1]);
                Gizmos.DrawSphere(moveSpot[i + 1], 2.5f);
            }
            

        }
    }

}
