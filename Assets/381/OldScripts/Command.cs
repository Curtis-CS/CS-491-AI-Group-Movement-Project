using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

//Author: Curtis Burchfield
//Email: cburchfield@nevada.unr.edu
//Sources: Curtis Burchfield CS 381 AS 6, and Unity A* Tutorial: https://youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW

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
