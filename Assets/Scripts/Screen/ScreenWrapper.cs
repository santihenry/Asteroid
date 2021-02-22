using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScreenWrapper : MonoBehaviour
{
    bool isWrappingX, isWrappingZ;
    Camera cam;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (gameObject.GetComponent<ShipController>() == null)
            WrapperAsteroid();
        else 
            Wrapper();

    }



    void Wrapper()
    {
        var viewportPosition = cam.WorldToViewportPoint(transform.position);

        Vector3 newPosition = transform.position;


        if (!isWrappingX && transform.position.x > 110 || transform.position.x < -110)
        {
            newPosition.x = -newPosition.x;
        }
        if (!isWrappingZ && transform.position.z > 60 || transform.position.z < -60)
        {
            newPosition.z = -newPosition.z;
        }


        transform.position = newPosition;

    }


    void WrapperAsteroid()
    {
        var viewportPosition = cam.WorldToViewportPoint(transform.position);

        Vector3 newPosition = transform.position;


        if (!isWrappingX && transform.position.x > 120 || transform.position.x < -120)
        {
            newPosition.x = -newPosition.x;
        }
        if (!isWrappingZ && transform.position.z > 70 || transform.position.z < -70)
        {
            newPosition.z = -newPosition.z;
        }


        transform.position = newPosition;

    }


    private void OnBecameInvisible()
    {
        isWrappingX = false;
        isWrappingZ = false;
    }


    private void OnBecameVisible()
    {
        isWrappingX = true;
        isWrappingZ = true;
    }


}
