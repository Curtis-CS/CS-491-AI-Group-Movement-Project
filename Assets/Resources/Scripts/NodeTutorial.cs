using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTutorial : IHeapItem<NodeTutorial>
{
    //Variable as to whether or not there is an obstacle present
    public bool blocked;
    //The nodes position
    public Vector3 nodeWorldPos;
    //The index of the node, in the 2D array grid[x, y]
    public int gridX;
    public int gridY;

    public NodeTutorial parent;

    public int gCost;//The path/edge cost
    public int hCost;//The cost the heuristic gives

    int heapIndex;

    public NodeTutorial(bool b, Vector3 pos, int gX, int gY)
    {
        blocked = b;
        nodeWorldPos = pos;
        gridX = gX;
        gridY = gY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(NodeTutorial compareNode)
    {
        int compare = fCost.CompareTo(compareNode.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(compareNode.hCost);
        }
        return -compare;
    }

}
