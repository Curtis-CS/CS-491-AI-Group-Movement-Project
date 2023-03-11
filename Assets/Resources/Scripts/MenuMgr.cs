using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuMgr : MonoBehaviour
{
    public static MenuMgr inst;
    public void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set the default values
        CrossSceneDataManager.CircleGenerateNumber = 20;
        CrossSceneDataManager.RectangleGenerateNumber = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadCircular(int obstacles)
    {
        //Set the number of obstacles to be generated and then accessed in the other scene
        CrossSceneDataManager.CircleGenerateNumber = obstacles;
        SceneManager.LoadScene("Circular");
    }

    public void LoadRectangular(int obstacles)
    {
        //Set the number of obstacles to be generated and then accessed in the other scene
        CrossSceneDataManager.RectangleGenerateNumber = obstacles;
        SceneManager.LoadScene("Rectangular");
    }

    public void LoadAStar()
    {
        SceneManager.LoadScene("AStarNav");
    }

    public void LoadOffice()
    {
        SceneManager.LoadScene("Office");
    }

    public void LoadControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
