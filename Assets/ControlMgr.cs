using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        for (int i=0; i<SelectionMgr.inst.selectedIDs.Count;i++)
        {
            Entity381 selectedEntity = EntityMgr.inst.entities[SelectionMgr.inst.selectedIDs[i]];
            selectedEntity.isSelected = true;
            selectedEntity.selecitonCylinder.SetActive(true);
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                selectedEntity.desSpeed += speedStep;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                selectedEntity.desSpeed -= speedStep;
            }

            selectedEntity.desSpeed = Utilities.Clamp(
                selectedEntity.desSpeed, selectedEntity.minSpeed, selectedEntity.maxSpeed);


            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                selectedEntity.desHeading -= headingStep;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                selectedEntity.desHeading += headingStep;
            }

            selectedEntity.desHeading = Utilities.Degrees360(selectedEntity.desHeading);
        }
    }
}
