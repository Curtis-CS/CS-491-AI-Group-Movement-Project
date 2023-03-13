using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Curtis Burchfield
//Email: cburchfield@nevada.unr.edu
//Sources: Curtis Burchfield CS 381 AS 6, and Unity A* Tutorial: https://youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW

//Class for handling all of the selecting mechanics, like TAB, left click, and click and drag
public class SelectionMgr : MonoBehaviour
{
    public static SelectionMgr inst;
    private void Awake()
    {
        inst = this;
    }

    public int selectedEntityIndex = 1;
    public Entity381 selectedEntity;

    bool isSelecting = false;
    Vector3 mousePosition1;
    public List<int> selectedIDs = new List<int>();

    public Vector3 intersectionPoint = Vector3.zero;
    public float radius = 100;
    public List<int> IDs = new List<int>();
    public int closestEntity = 0;

    public bool multipleSelected;


    // Start is called before the first frame update
    void Start()
    {
        UnselectAll();
        selectedEntity.isSelected = true;
        selectedEntity.selecitonCylinder.SetActive(true);
        selectedIDs.Add(selectedEntityIndex);
    }

    // Update is called once per frame
    void Update()
    {
        //Tab Key cycles through entites
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            SelectNextEntity();
            multipleSelected = false;
        }

        //Draw the selection bounds
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;
        }
        //Once the selection bound done, select all entities within the are of the selection zone
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonUp(0))
        {
            UnselectAll();
            selectedIDs.Clear();
            foreach (Entity381 ent in EntityMgr.inst.entities)
            {
                if (IsWithinSelectionBounds(ent.position))
                {
                    selectedIDs.Add(ent.ID);
                }
            }
            isSelecting = false;
            if (selectedIDs.Count > 0)
            {
                multipleSelected = true;
            }
        }

        //If just selecting a singular entity
        RaycastHit hit;
        if (Input.GetMouseButtonUp(0) && !Input.GetKey(KeyCode.LeftControl))
        {
            IDs.Clear();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                intersectionPoint = hit.point;

                for (int i =0; i< EntityMgr.inst.entities.Count; i++)
                {
                    Entity381 Obj = EntityMgr.inst.entities[i];
                    if (Vector3.Distance(hit.point, EntityMgr.inst.entities[i].position) < radius)
                    {
                        //print(intersectionPoint + " " + Obj.position + " " + Vector3.Distance(hit.point, EntityMgr.inst.entities[i].position));
                        IDs.Add(EntityMgr.inst.entities[i].ID);
                    }
                }
            }
            if (IDs.Count > 0)
            {
                closestEntity = FindClosestEntity(intersectionPoint);
                SelectClosestEntity(closestEntity);
            }
            multipleSelected = false;
        }
    }
    void SelectNextEntity()
    {
        if (selectedEntityIndex >= EntityMgr.inst.entities.Count -1)
        {
            selectedEntityIndex = 0;
        }
        else
        {
            selectedEntityIndex = selectedEntityIndex + 1;
        }
        selectedEntity = EntityMgr.inst.entities[selectedEntityIndex];
        selectedIDs.Clear();
        selectedIDs.Add(selectedEntityIndex);
        UnselectAll();
        selectedEntity.isSelected = true;
        selectedEntity.selecitonCylinder.SetActive(true);

    }

    void UnselectAll()
    {
        multipleSelected = false;
        foreach (Entity381 ent in EntityMgr.inst.entities)
        {
            ent.isSelected = false;
            ent.selecitonCylinder.SetActive(false);
        }
    }

    public int FindClosestEntity(Vector3 intPoint)
    {
        int ID = selectedEntityIndex;
        float min = 10000000000;
        for (int i=0;i < IDs.Count;i++)
        {
            if (Vector3.Distance(intPoint, EntityMgr.inst.entities[IDs[i]].position) < min)
            {
                min = Vector3.Distance(intPoint, EntityMgr.inst.entities[IDs[i]].position);
                ID = IDs[i];
            }
        }
        return ID;
    }

    public void SelectClosestEntity(int id)
    {
        selectedEntityIndex = EntityMgr.inst.entities[id].ID;
        selectedEntity = EntityMgr.inst.entities[selectedEntityIndex];
        UnselectAll();
        selectedIDs.Clear();
        selectedIDs.Add(selectedEntityIndex);
        selectedEntity.isSelected = true;
        selectedEntity.selecitonCylinder.SetActive(true);
    }

    public bool IsWithinSelectionBounds(Vector3 pos)
    {
        if(!isSelecting)
        {
            return false;
        }
        var camera = Camera.main;
        var viewportBounds = Utilities.GetViewportBounds(camera, mousePosition1, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(pos));
    }

    private void OnGUI()
    {
        if (isSelecting)
        {
            Rect rect = Utilities.GetScreenRect(mousePosition1, Input.mousePosition);
            Utilities.DrawScreenRect(rect, new Color(0.8f, 0.8f, .95f, 0.25f));
            Utilities.DrawScreenRectBorder(rect, 2, new Color(.08f, .08f, .95f));
        }
    }

}
