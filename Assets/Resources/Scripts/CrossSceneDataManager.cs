using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class just for accessing data across scenes
public static class CrossSceneDataManager
{
    //Data that can be accessed in any scene, used for accessing the number that we want to pass to the random generating scenes
    public static int CircleGenerateNumber = 0;
    public static int RectangleGenerateNumber = 0;
}
