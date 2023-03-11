// Author: Cody Jackson | codyj@nevada.unr.edu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public static GridGenerator inst;
    public void Awake()
    {
        inst = this;
    }

    [Tooltip("Prefab for navigation node")]
    public GameObject nodePrefab;
    [Tooltip("Size of eaach node in the grid")]
    public float nodeSize = 1f;
    [Tooltip("Navigation area to match size of")]
    public GameObject navPlane;

    private int gridSizeX, gridSizeY; // Size of the grid
    private Vector3 bottomLeft; // Bottom left corner of the plane

    public Dictionary<Vector3 , GameObject> grid = new Dictionary<Vector3, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the size of the grid based on the plane object
        gridSizeX = Mathf.RoundToInt(navPlane.transform.localScale.x / nodeSize);
        gridSizeY = Mathf.RoundToInt(navPlane.transform.localScale.z / nodeSize);

        // Calculate the bottom left corner of the plane
        bottomLeft = navPlane.transform.position - new Vector3(navPlane.transform.localScale.x / 2, 0, navPlane.transform.localScale.z / 2);
    }

    public void GenerateGrid()
    {
        // Loop through the grid and create empty game objects at each node
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 nodePos = bottomLeft + new Vector3(x * nodeSize + nodeSize / 2, 0, y * nodeSize + nodeSize / 2); // Calculate the position of the node

                GameObject node = Instantiate(nodePrefab, nodePos, Quaternion.identity); // Create the node game object

                node.transform.parent = transform; // Set the node's parent to this game object for organizational purposes

                node.GetComponent<Node>().location = nodePos; // Set the node's position inside the prefab

                grid.Add(nodePos, node); // Add it to the dictionary
            }
        }
    }
}
