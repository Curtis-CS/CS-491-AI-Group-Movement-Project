using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Curtis Burchfield
//Email: cburchfield@nevada.unr.edu
//Sources: Curtis Burchfield CS 381 AS 6, and Unity A* Tutorial: https://youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW

public class Entity381 : MonoBehaviour
{

    public bool isSelected = false;
    public int ID;

    public Vector3 position = Vector3.zero;
    public Vector3 velocity = Vector3.zero;

    public float speed;
    public float desSpeed;

    public float heading;
    public float desHeading;
    //--------------------Values below don't change
    public float maxSpeed;
    public float minSpeed;

    public float acceleration;
    public float turnRate;

    public GameObject cameraRig;

    public GameObject selecitonCylinder;

    public Vector3 finalPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
