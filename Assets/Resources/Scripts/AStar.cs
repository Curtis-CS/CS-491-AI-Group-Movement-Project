using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

//Author: Curtis Burchfield
//Email: cburchfield@nevada.unr.edu
//Sources: Curtis Burchfield CS 381 AS 6, and Unity A* Tutorial: https://youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW

public class AStar : MonoBehaviour
{

    GridTutorial grid;

    public Transform seeker, target;

    public int counter;
    public bool gridGenerated;

    private void Awake()
    {
        grid = GetComponent<GridTutorial>();
        //CrossSceneDataManager.GenerateNumber = 10;
        //CrossSceneDataManager.Circles = false;
        gridGenerated = false;
    }

    private void Update()
    {
        if (!gridGenerated)
        {
            counter++;
        }
        if (!gridGenerated && counter > 200)
        {
            grid.CreateGrid();
            gridGenerated = true;
        }
        //if (gridGenerated)
        //{
        //    FindBestPath(seeker.position, target.position);
        //}
    }

    public List<Vector3> FindBestPath(Vector3 startPos, Vector3 endPos)
    {
        //Stopwatch sw = new Stopwatch();
        //sw.Start();
        NodeTutorial startNode = grid.NodeFromWorldPoint(startPos);
        NodeTutorial endNode = grid.NodeFromWorldPoint(endPos);

        Heap<NodeTutorial> openSet = new Heap<NodeTutorial>(grid.MaxSize);
        HashSet<NodeTutorial> closedSet = new HashSet<NodeTutorial>();

        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            NodeTutorial curNode = openSet.RemoveFirst();
            closedSet.Add(curNode);
            if (curNode == endNode)
            {
                List<Vector3> bestPath = RetracePath(startNode, endNode);
                return bestPath;
                //sw.Stop();
                //print("Path Found: " + sw.ElapsedMilliseconds + " ms");
            }
            else
            {
                foreach (NodeTutorial neighbor in grid.GetAdjacentNodes(curNode))
                {
                    if (neighbor.blocked || closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    int newMovCostToNeighbour = curNode.gCost + GetDistance(curNode, neighbor);
                    if (newMovCostToNeighbour < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovCostToNeighbour;
                        neighbor.hCost = GetDistance(neighbor, endNode);
                        neighbor.parent = curNode;
                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
        }
        print("NO PATH FOUND");
        return null;
    }

    int GetDistance(NodeTutorial a, NodeTutorial b)
    {
        int distX = Mathf.Abs(a.gridX - b.gridX);
        int distY = Mathf.Abs(a.gridY - b.gridY);
        //14 is the distance for a diagonal move, and 10 is a horizontal move
        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            
            return 14 * distX + 10 * (distY - distX);
        }
    }

    List<Vector3> RetracePath(NodeTutorial startNode, NodeTutorial endNode)
    {
        List<NodeTutorial> path = new List<NodeTutorial>();
        NodeTutorial curNode = endNode;
        while (curNode != startNode)
        {
            path.Add(curNode);
            curNode = curNode.parent;
        }
        path.Reverse();
        List<Vector3> bestPath = SimplifyPath(path);
        //grid.path = path;
        return bestPath;
        

    }

    List<Vector3> SimplifyPath(List<NodeTutorial> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        List<NodeTutorial> nodeWaypoints = new List<NodeTutorial>();
        Vector2 oldDir = Vector2.zero;
        for (int i=1;i<path.Count;i++)
        {
            Vector2 newDir = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (newDir != oldDir)
            {
                waypoints.Add(path[i-1].nodeWorldPos);
                waypoints.Add(path[i].nodeWorldPos);
                nodeWaypoints.Add(path[i-1]);
                nodeWaypoints.Add(path[i]);
            }
            oldDir = newDir;
        }
        waypoints.Add(path[path.Count - 1].nodeWorldPos);
        nodeWaypoints.Add(path[path.Count - 1]);
        grid.path = nodeWaypoints;
        return waypoints;
    }
}
