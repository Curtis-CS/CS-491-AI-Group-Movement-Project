using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
