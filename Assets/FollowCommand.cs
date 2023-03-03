using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCommand : Command
{
    public Vector3 movePosition;
    public Vector3 difference = Vector3.positiveInfinity;
    public float slowDownDistanceSquared = 10000;
    public float doneDistanceSquared = 1000;
    public Vector3 moveVector;
    public float newDesiredHeading;

    public Entity381 movingToEntity;
    public Vector3 offset = new Vector3(30, 0, -50);

    public FollowCommand(Entity381 entityFollower, Entity381 entityFollowed) : base(entityFollower)
    {
        movingToEntity = entityFollowed;
    }

    public override void Init()
    {
        Debug.Log("Moving Entity to " + movingToEntity);
    }

    public override void Tick()
    {
        //Find Desired Heading and speed
        movePosition = movingToEntity.position + offset;
        moveVector = movePosition - ent.position;
        Debug.Log("Current Positon: " + ent.position + "Move Position: " + movePosition);
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
        //difference = movePosition - ent.position;

        return false;//(difference.sqrMagnitude < doneDistanceSquared);
    }

    public override void Stop()
    {
        //ent.speed = 0;
        ent.desSpeed = 0;
        ent.desHeading = ent.heading;
    }
}
