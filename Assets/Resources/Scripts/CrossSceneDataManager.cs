using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Curtis Burchfield
//Email: cburchfield@nevada.unr.edu
//Sources: Curtis Burchfield CS 381 AS 6, and Unity A* Tutorial: https://youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW

//Class just for accessing data across scenes
public static class CrossSceneDataManager
{
    //Data that can be accessed in any scene, used for accessing the number that we want to pass to the random generating scenes
    public static int GenerateNumber = 0;
    public static bool Circles;
}
