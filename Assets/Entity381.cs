using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
