using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Curtis Burchfield
//Email: cburchfield@nevada.unr.edu
//Sources: Curtis Burchfield CS 381 AS 6, and Unity A* Tutorial: https://youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW

public class MoveCommand : Command
{
    public Vector3 movePosition;
    public Vector3 difference = Vector3.positiveInfinity;
    public float slowDownDistanceSquared = 400;
    public float doneDistanceSquared = 200;
    public Vector3 moveVector;
    public float newDesiredHeading;

    public MoveCommand(Entity381 entity, Vector3 pos) : base(entity)
    {
        movePosition = pos;
    }

    public override void Init()
    {
        Debug.Log("Moving Entity to " + movePosition);
    }

    public override void Tick()
    {
        //Find Desired Heading and speed
        moveVector = movePosition - ent.position;
        //Debug.Log("Current Positon: " + ent.position + "Move Position: " + movePosition);
        newDesiredHeading = Utilities.ToDegrees(Mathf.Atan2(moveVector.x, moveVector.z));
        //newDesiredHeading = Utilities.Degrees360(newDesiredHeading);
        ent.desHeading = newDesiredHeading;


        if (moveVector.sqrMagnitude < doneDistanceSquared)
        {
            ent.desSpeed = 0;
        }
        else if (moveVector.sqrMagnitude < slowDownDistanceSquared)
        {
            ent.desSpeed = 8;
        }
        else
        {
            ent.desSpeed = ent.maxSpeed;
        }
    }

    public override bool IsDone()
    {
        difference = movePosition - ent.position;

        return (difference.sqrMagnitude < doneDistanceSquared);
    }

    public override void Stop()
    {
        //ent.speed = 0;
        ent.desSpeed = 0;
        if (ent.speed > 10)
        {
            ent.speed = 12;
        }
        ent.desHeading = ent.heading;
    }
}
