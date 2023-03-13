using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Curtis Burchfield
//Email: cburchfield@nevada.unr.edu
//Sources: Curtis Burchfield CS 381 AS 6, and Unity A* Tutorial: https://youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW

public class OrientatedPhysics : MonoBehaviour
{

    public Entity381 entity;
    public Vector3 eulerRotation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        entity.position = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        

        if (Utilities.ApproximatelyEqual(entity.speed, entity.desSpeed))
        {
            ;
        }
        else if (entity.speed < entity.desSpeed)
        {
            entity.speed = entity.speed + entity.acceleration * Time.deltaTime;
        }
        else if (entity.speed > entity.desSpeed)
        {
            entity.speed = entity.speed - entity.acceleration * Time.deltaTime;
        }

        entity.speed = Utilities.Clamp(entity.speed, entity.minSpeed, entity.maxSpeed);

        //heading stuff
        if (Utilities.ApproximatelyEqual(entity.heading, entity.desHeading))
        {
            ;
        }
        else if (Utilities.AndgleDiffPosNeg(entity.desHeading, entity.heading) > 0)
        {
            entity.heading = entity.heading + entity.turnRate * Time.deltaTime;
        }
        else if (Utilities.AndgleDiffPosNeg(entity.desHeading, entity.heading) < 0)
        {
            entity.heading = entity.heading - entity.turnRate * Time.deltaTime;
        }

        //Debug.Log("Desired Heading: " + entity.desHeading);
        entity.heading = Utilities.Degrees360(entity.heading);


        entity.velocity.x = Mathf.Sin(entity.heading * Mathf.Deg2Rad) * entity.speed;
        entity.velocity.y = 0;
        entity.velocity.z = Mathf.Cos(entity.heading * Mathf.Deg2Rad) * entity.speed;

        entity.position = entity.position + entity.velocity * Time.deltaTime;
        transform.localPosition = entity.position;

        eulerRotation.y = entity.heading;
        transform.localEulerAngles = eulerRotation;

    }


}
