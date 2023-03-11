using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Class for the basic controls, other controls are present in Selection Manager and AI mgr
public class ControlMgr : MonoBehaviour
{
    public static ControlMgr inst;
    private void Awake()
    {
        inst = this;
    }

    public float speedStep = 1;
    public float headingStep = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ESC Goes back to menu
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        //For all of the currently selected entities
        for (int i=0; i<SelectionMgr.inst.selectedIDs.Count;i++)
        {
            Entity381 selectedEntity = EntityMgr.inst.entities[SelectionMgr.inst.selectedIDs[i]];
            selectedEntity.isSelected = true;
            selectedEntity.selecitonCylinder.SetActive(true); //Show that they are selected

            //Increase the selected entities speed with up arrow
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                selectedEntity.desSpeed += speedStep;
            }
            //Decrease the speed
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                selectedEntity.desSpeed -= speedStep;
            }

            //Set the entities desired speed, clamp (don't let it go past) the max and min
            selectedEntity.desSpeed = Utilities.Clamp(
                selectedEntity.desSpeed, selectedEntity.minSpeed, selectedEntity.maxSpeed);

            //Change the entities current heading/direction
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                selectedEntity.desHeading -= headingStep;
            }
            //Change the entities current heading/direction
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                selectedEntity.desHeading += headingStep;
            }

            selectedEntity.desHeading = Utilities.Degrees360(selectedEntity.desHeading);
        }
    }
}
