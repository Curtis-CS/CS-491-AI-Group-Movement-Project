using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Curtis Burchfield
//Email: cburchfield@nevada.unr.edu
//Sources: Curtis Burchfield CS 381 AS 6, and Unity A* Tutorial: https://youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW

public class UnitAI : MonoBehaviour
{
    public Entity381 ent;
    public List<Command> commands = new List<Command>();

    public void Update()
    {
        if (commands.Count > 0)
        {
            Command currentCommand = commands[0];
            //Debug.log("Current Command: " + currentCommand);
            if (currentCommand.IsDone())
            {
                currentCommand.Stop();
                commands.RemoveAt(0);
            }
            else
            {
                currentCommand.Tick();
            }
        }
        else
        {

        }
    }

    public void SetCommand(Command command)
    {
        if (commands.Count > 0)
        {
            for (int i =0;i<commands.Count;i++)
            {
                commands[i].Stop();
            }
            commands.Clear();
        }
        AddCommand(command);
    }

    public void AddCommand(Command command)
    {
        commands.Add(command);
        //print("Command Added");
    }

    public void ClearCommands()
    {
        commands.Clear();
    }
}
