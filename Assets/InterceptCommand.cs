using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptCommand : Command
{
    public Vector3 movePosition;
    public Vector3 difference = Vector3.positiveInfinity;
    public float slowDownDistanceSquared = 100;
    public float doneDistanceSquared = 100;
    public Vector3 moveVector;
    public float newDesiredHeading;

    public float time;
    public float relativeSpeed;
    public Vector3 differencePosition;
    public Vector3 differenceVelocity;
    public float distance;

    public Vector3 predictedPosition;

    public Entity381 movingToEntity;

    public InterceptCommand(Entity381 entityIntercepting, Entity381 entityIntercepted) : base(entityIntercepting)
    {
        movingToEntity = entityIntercepted;
    }

    public override void Init()
    {
        Debug.Log("Moving Entity to " + movingToEntity);
    }

    public override void Tick()
    {
        //Find Desired Heading and speed
        differencePosition = movingToEntity.position - ent.position;
        distance = differencePosition.magnitude;
        differenceVelocity = movingToEntity.velocity - ent.velocity;
        relativeSpeed = differenceVelocity.magnitude;
        time = distance / relativeSpeed;

        predictedPosition = movingToEntity.position + (movingToEntity.velocity * time);

        movePosition = predictedPosition;
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
            ent.desSpeed = 6;
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
        ent.desHeading = ent.heading;
    }
}
