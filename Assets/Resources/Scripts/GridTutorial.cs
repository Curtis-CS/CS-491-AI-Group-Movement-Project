using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Curtis Burchfield
//Email: cburchfield@nevada.unr.edu
//Sources: Curtis Burchfield CS 381 AS 6, and Unity A* Tutorial: https://youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW

public class GridTutorial : MonoBehaviour
{

    public bool onlyDisplayPath;

    public Transform entity;
    public LayerMask obstaclesMask;//The layer mask for obstacles
    public Vector2 gridWorldSize;//The size of the grid we are generating
    public float nodeRadius;//The size of the nodes
    NodeTutorial[,] grid;//Our 2D array of out nodes, forming our grid

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    public List<NodeTutorial> path;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    public void CreateGrid()
    {
        grid = new NodeTutorial[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;//Find the bottom left of the grid size in the 2D array

        //Create a node at each position in the gird based off of the node size
        for (int x=0;x < gridSizeX;x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool blocked = (Physics.CheckSphere(worldPoint, nodeRadius, obstaclesMask));
                grid[x, y] = new NodeTutorial(blocked, worldPoint, x, y);
            }
        }

    }

    //Returns the node that is at the position entered
    public NodeTutorial NodeFromWorldPoint(Vector3 worldPosition)
    {
        float xPercent = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float yPercent = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        xPercent = Mathf.Clamp01(xPercent);
        yPercent = Mathf.Clamp01(yPercent);
        int x = Mathf.RoundToInt((gridSizeX - 1) * xPercent);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPercent);
        return grid[x, y];
    }

    public List<NodeTutorial> GetAdjacentNodes(NodeTutorial node)
    {
        List<NodeTutorial> neighbors = new List<NodeTutorial>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x==0 && y ==0)
                {
                    continue;
                }
                else
                {
                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;
                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbors.Add(grid[checkX, checkY]);
                    }
                }
            }
        }
        return neighbors;
    }

    //Drawing the nodes and information
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 2, gridWorldSize.y));
        if (grid!=null)
        {
            if (onlyDisplayPath)
            {
                if (path != null)
                {
                    NodeTutorial entityNode = NodeFromWorldPoint(entity.position);
                    foreach (NodeTutorial node in grid)
                    {
                        if (path.Contains(node))
                        {
                            Gizmos.color = Color.black;
                            Gizmos.DrawCube(node.nodeWorldPos, Vector3.one * (nodeDiameter - 4f));
                        }
                    }

                }
            }
            else
            {

                NodeTutorial entityNode = NodeFromWorldPoint(entity.position);
                foreach (NodeTutorial node in grid)
                {

                    if (node.blocked)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(node.nodeWorldPos, Vector3.one * (nodeDiameter - 4f));
                    }
                    if (!node.blocked)
                    {
                        Gizmos.color = Color.white;
                    }
                    if (path != null)
                    {
                        if (path.Contains(node))
                        {
                            Gizmos.color = Color.black;
                            Gizmos.DrawCube(node.nodeWorldPos, Vector3.one * (nodeDiameter - 4f));
                        }
                    }
                    

                }
            }
        }
    }

}
