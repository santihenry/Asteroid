using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel : MonoBehaviour
{

    public float speed = 60;
    public float currentV = 0;
    public float currentH = 0;
    public float turnLeftRigth = 0;
    public float interpolation = 50;
    public float turnSpeed = 200;

    public float aceleration;
    public float dirHTwo;
    public float dirV;
    public float dirH;

}
