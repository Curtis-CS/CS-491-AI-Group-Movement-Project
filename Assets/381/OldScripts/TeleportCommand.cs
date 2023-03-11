using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCommand : Command
{
    public Vector3 teleportPosition;

    public Vector3 difference = Vector3.positiveInfinity;
    public float doneDistanceSquared = 1000;

    public TeleportCommand(Entity381 entity, Vector3 pos) : base(entity)
    {
        teleportPosition = pos;
    }

    public override void Init()
    {
        Debug.Log("Teleporting Entity to " + teleportPosition);
    }

    public override void Tick()
    {
        ent.position = teleportPosition;
    }

    public override bool IsDone()
    {
        difference = teleportPosition - ent.position;

        return (difference.sqrMagnitude < doneDistanceSquared);
    }

    public override void Stop()
    {

    }

}
