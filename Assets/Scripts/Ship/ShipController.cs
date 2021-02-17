using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    ShipModel model;


    void Start()
    {
        model = GetComponent<ShipModel>();
    }

   
    void Update()
    {
        Movement();
    }


    public void Movement()
    {

        model.aceleration = Input.GetAxisRaw("Vertical");
        model.dirHTwo = Input.GetAxisRaw("Horizontal");
        model.dirV = 0;
        model.dirH = 0;

        model.currentV = Mathf.Lerp(model.currentV, model.dirV, model.interpolation * Time.deltaTime);
        model.currentH = Mathf.Lerp(model.currentH, model.dirH, model.interpolation * Time.deltaTime);
        model.turnLeftRigth = Mathf.Lerp(model.turnLeftRigth, model.dirHTwo, model.interpolation * Time.deltaTime);

        transform.Rotate(model.currentV * model.turnSpeed * Time.deltaTime, model.turnLeftRigth * model.turnSpeed * Time.deltaTime, model.currentH * model.turnSpeed * Time.deltaTime);

        if (model.aceleration > 0)
        {
            transform.position += transform.forward * model.speed * Time.deltaTime;
        }
    }


    public void Rotate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * model.turnSpeed), Space.Self);
        }
    }

}
