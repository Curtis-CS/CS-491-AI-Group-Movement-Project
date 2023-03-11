using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Command
{
    public Entity381 ent;
    public Command(Entity381 entity)
    {
        ent = entity;
    }

    public virtual void Init()
    {

    }

    public virtual void Tick()
    {

    }

    public virtual bool IsDone()
    {
        return false;
    }

    public virtual void Stop()
    {

    }
}
