using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    public Settings Settings;


    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Camera.transform.transform.RotateAround(Vector3.zero, Vector3.right, Speed / 360.0f);
        gameObject.transform.RotateAround(Vector3.zero, Vector3.right, Settings.Speed / 360.0f);
        gameObject.transform.position = gameObject.transform.position.normalized * Settings.Height;
        
    }

    
}
