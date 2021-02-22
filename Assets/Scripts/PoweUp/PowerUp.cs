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
    protected float waitTime;
    [SerializeField]
    protected float startWaitTime;
    [SerializeField]
    protected float speed;
    protected Transform[] moveSpot;
    protected int randomSpot;
    protected float _currentTime;

    #region

    [SerializeField]
    protected Transform randomMoveSpot;
    [SerializeField]
    protected float minX, maxX, minY, maxY, minZ, maxZ;
    #endregion

    public TypesPoweUp type;

    private void Start()
    {

    }


    

    virtual public void Update()
    {
        _currentTime += Time.deltaTime;

        MoveRandom();
    }

    virtual public  void MoveRandom()
    {
        transform.position = Vector3.MoveTowards(transform.position, randomMoveSpot.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, randomMoveSpot.position) < 0.2f)
        {
            if (waitTime - _currentTime <= 0)
            {
                randomMoveSpot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
                _currentTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ShipController>() || other.GetComponent<Bullet>())
        {
            if (other.gameObject.GetComponent<ShipController>())
            {
                if (type == TypesPoweUp.turbo)
                    other.gameObject.GetComponent<ShipController>().GetModel.powerUpSpeed = true;
                if (type == TypesPoweUp.forceField)
                    other.gameObject.GetComponent<ShipController>().GetModel.powerUp = true;
                if (type == TypesPoweUp.upgradeWeapon1)
                    other.gameObject.GetComponent<ShipController>().GetModel.weapon.currentLevel = 1;
                if (type == TypesPoweUp.upgradeWeapon2)
                    other.gameObject.GetComponent<ShipController>().GetModel.weapon.currentLevel = 2;
                if (type == TypesPoweUp.downgradeWeapon)
                    other.gameObject.GetComponent<ShipController>().GetModel.weapon.currentLevel = 0;
            }
            Destroy(gameObject);
        }
    }

}
