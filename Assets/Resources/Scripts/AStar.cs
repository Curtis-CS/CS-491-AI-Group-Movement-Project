using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class AStar : MonoBehaviour
{

    GridTutorial grid;
    RandomObstacleGenerator obsGenerator;

    public Transform seeker, target;

    private void Awake()
    {
        obsGenerator = GetComponent<RandomObstacleGenerator>();

        CrossSceneDataManager.CircleGenerateNumber = 30;
        CrossSceneDataManager.RectangleGenerateNumber = 30;

        obsGenerator.SpawnShapes(CrossSceneDataManager.RectangleGenerateNumber, false);

        grid = GetComponent<GridTutorial>();

    }

    private void Update()
    {
        FindBestPath(seeker.position, target.position);
    }

    void FindBestPath(Vector3 startPos, Vector3 endPos)
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
                RetracePath(startNode, endNode);
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

    void RetracePath(NodeTutorial startNode, NodeTutorial endNode)
    {
        List<NodeTutorial> path = new List<NodeTutorial>();
        NodeTutorial curNode = endNode;
        while (curNode != startNode)
        {
            path.Add(curNode);
            curNode = curNode.parent;
        }
        path.Reverse();

        grid.path = path;

    }
}
