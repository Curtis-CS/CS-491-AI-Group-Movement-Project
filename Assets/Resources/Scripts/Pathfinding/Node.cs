using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 location;
    public GameObject visSphere;
    public bool visualize = false;
    public bool blocked = false;

    private void Start()
    {
        //When spawned in, check if it is blocked by an obstacle
    }

}
