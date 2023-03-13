using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

//Author: Curtis Burchfield
//Email: cburchfield@nevada.unr.edu
//Sources: Curtis Burchfield CS 381 AS 6, and Unity A* Tutorial: https://youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW
// Minor Edit by: Cody Jackson | codyj@nevada.unr.edu

//This class is used for contorling the 'AI' part of the ships, like automatic movement to a position, following, intercepting,
//And such
public class AIMgr : MonoBehaviour
{
    public static AIMgr inst;
    private void Awake()
    {
        inst = this;
    }
    public AStar aStar;
    public RaycastHit hit;
    public int layerMask;
    
    //How far to look for an entity that was close to the click for selecting
    public float clickRadius = 50;

    // Start is called before the first frame update
    void Start()
    {
        //I can't exactly remember what this is for, I think it sets up the layers of the scene so that the raycast hits the ocean
        layerMask = 1 << 9; ;
    }

    // Update is called once per frame
    void Update()
    {
        //If the right mouse button was clicked
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, layerMask))
            {
                //Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 2);
                Vector3 pos = hit.point;
                pos.y = 0;
                Entity381 ent = findClosestEntInRadius(pos, clickRadius);
                List<Vector3> bestPath = aStar.FindBestPath(SelectionMgr.inst.selectedEntity.transform.position, pos);
                //if the click was not on an entity
                if (ent == null || ent == SelectionMgr.inst.selectedEntity)
                {
                    //If holding alt when right clicking not on an entity, teleport them their
                    if (Input.GetKey(KeyCode.LeftAlt))
                    {
                        //Teleport Command
                        HandleTeleport(pos);
                        //print("Teleport Branch");
                    }
                    //Else just start moving them there
                    else
                    {
                        //move command
                        //Debug.Log("HERE");
                        if (Input.GetKey(KeyCode.T))
                        {
                            if (SelectionMgr.inst.multipleSelected)
                            {
                                foreach (int entityIndex in SelectionMgr.inst.selectedIDs)
                                {
                                    bestPath = aStar.FindBestPath(EntityMgr.inst.entities[entityIndex].finalPosition, pos);
                                    HandleMoveAStar(bestPath, true, EntityMgr.inst.entities[entityIndex]);
                                }
                            }
                            else
                            {
                                bestPath = aStar.FindBestPath(SelectionMgr.inst.selectedEntity.finalPosition, pos);
                                HandleMoveAStar(bestPath, true, SelectionMgr.inst.selectedEntity);
                            }
                        }
                        else
                        {
                            if (SelectionMgr.inst.multipleSelected)
                            {
                                foreach (int entityIndex in SelectionMgr.inst.selectedIDs)
                                {
                                    bestPath = aStar.FindBestPath(EntityMgr.inst.entities[entityIndex].transform.position, pos);
                                    HandleMoveAStar(bestPath, false, EntityMgr.inst.entities[entityIndex]);
                                }
                            }
                            else
                            {
                                HandleMoveAStar(bestPath, false, SelectionMgr.inst.selectedEntity);
                            }
                        }
                    }
                }
                //If the right click was on an entity
                //---------------------I removed the following and intercepting as it is not needed for the assignment and is complicated
                else
                {
                    //And they were holding E, intercept the entity
                    if (Input.GetKey(KeyCode.E))
                    {
                        //intercept entity
                        //HandleIntercept(ent);
                        if (Input.GetKey(KeyCode.T))
                        {
                            if (SelectionMgr.inst.multipleSelected)
                            {
                                foreach (int entityIndex in SelectionMgr.inst.selectedIDs)
                                {
                                    bestPath = aStar.FindBestPath(EntityMgr.inst.entities[entityIndex].finalPosition, pos);
                                    HandleMoveAStar(bestPath, true, EntityMgr.inst.entities[entityIndex]);
                                }
                            }
                            else
                            {
                                bestPath = aStar.FindBestPath(SelectionMgr.inst.selectedEntity.finalPosition, pos);
                                HandleMoveAStar(bestPath, true, SelectionMgr.inst.selectedEntity);
                            }
                        }
                        else
                        {
                            if (SelectionMgr.inst.multipleSelected)
                            {
                                foreach (int entityIndex in SelectionMgr.inst.selectedIDs)
                                {
                                    bestPath = aStar.FindBestPath(EntityMgr.inst.entities[entityIndex].transform.position, pos);
                                    HandleMoveAStar(bestPath, false, EntityMgr.inst.entities[entityIndex]);
                                }
                            }
                            else
                            {
                                HandleMoveAStar(bestPath, false, SelectionMgr.inst.selectedEntity);
                            }
                        }
                    }
                    //Else, just follow the entity
                    else
                    {
                        //follow entity
                        //HandleFollow(ent);
                        if (Input.GetKey(KeyCode.T))
                        {
                            if (SelectionMgr.inst.multipleSelected)
                            {
                                foreach (int entityIndex in SelectionMgr.inst.selectedIDs)
                                {
                                    bestPath = aStar.FindBestPath(EntityMgr.inst.entities[entityIndex].finalPosition, pos);
                                    HandleMoveAStar(bestPath, true, EntityMgr.inst.entities[entityIndex]);
                                }
                            }
                            else
                            {
                                bestPath = aStar.FindBestPath(SelectionMgr.inst.selectedEntity.finalPosition, pos);
                                HandleMoveAStar(bestPath, true, SelectionMgr.inst.selectedEntity);
                            }
                        }
                        else
                        {
                            if (SelectionMgr.inst.multipleSelected)
                            {
                                foreach (int entityIndex in SelectionMgr.inst.selectedIDs)
                                {
                                    bestPath = aStar.FindBestPath(EntityMgr.inst.entities[entityIndex].transform.position, pos);
                                    HandleMoveAStar(bestPath, false, EntityMgr.inst.entities[entityIndex]);
                                }
                            }
                            else
                            {
                                HandleMoveAStar(bestPath, false, SelectionMgr.inst.selectedEntity);
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Right mouse button did not collide with anything");
            }
        }
    }

    //Finds the closest entity within the mouse click
    public Entity381 findClosestEntInRadius(Vector3 point, float RadiusSquared)
    {
        Entity381 minEnt = null;
        float minDistance = float.MaxValue;
        foreach(Entity381 ent in EntityMgr.inst.entities)
        {
            float distanceSquared = (ent.position - point).sqrMagnitude;
            if (distanceSquared < RadiusSquared)
            {
                if (distanceSquared < minDistance)
                {
                    minDistance = distanceSquared;
                    minEnt = ent;
                }
            }
        }
        return minEnt;
    }
    //Commands: If T is held when any commands were placed, they are added onto a queue, so that they will be followed out in order
    //Telleport
    void HandleTeleport(Vector3 point)
    {
        TeleportCommand teleport = new TeleportCommand(SelectionMgr.inst.selectedEntity, point);
        UnitAI uai = SelectionMgr.inst.selectedEntity.GetComponent<UnitAI>();
        if (Input.GetKey(KeyCode.T))
        {
            uai.AddCommand(teleport);
        }
        else
        {
            uai.SetCommand(teleport);
        }
    }

    void HandleMoveAStar(List<Vector3> path, bool addition, Entity381 entity)
    {
        UnitAI uai = entity.GetComponent<UnitAI>();
        if (!addition)
        {
            //Debug.Log("HERE Clearing the Commands");
            uai.ClearCommands();
            entity.speed = 1;
        }
        for (int i = 0; i < path.Count;i++)
        {
            if (addition)
            {
                MoveCommand move = new MoveCommand(entity, path[i]);
                uai.AddCommand(move);
            }
            else
            {
                MoveCommand move = new MoveCommand(entity, path[i]);
                uai.AddCommand(move);
            }
            
        }
        entity.finalPosition = path[path.Count - 1];
    }

    void HandleFollow(Entity381 entity)
    {
        FollowCommand follow = new FollowCommand(SelectionMgr.inst.selectedEntity, entity);
        UnitAI uai = SelectionMgr.inst.selectedEntity.GetComponent<UnitAI>();
        if (Input.GetKey(KeyCode.T))
        {
            uai.AddCommand(follow);
        }
        else
        {
            uai.SetCommand(follow);
        }
    }

    void HandleIntercept(Entity381 entity)
    {
        InterceptCommand intercept = new InterceptCommand(SelectionMgr.inst.selectedEntity, entity);
        UnitAI uai = SelectionMgr.inst.selectedEntity.GetComponent<UnitAI>();
        if (Input.GetKey(KeyCode.T))
        {
            uai.AddCommand(intercept);
        }
        else
        {
            uai.SetCommand(intercept);
        }
    }

    // Resume traveling after adjustment due to potential fields
    public void ResumeAstar(Entity381 entity)
    {
        if(entity.finalPosition == null || entity.finalPosition == Vector3.zero) // No destination set
        {
            entity.desHeading = entity.heading; // Stop turning
            entity.desSpeed = 0; // Stop moving
        }
        else // We were going somewhere
        {
            List<Vector3> bestPath = aStar.FindBestPath(entity.transform.position, entity.finalPosition);
            //bestPath = aStar.FindBestPath(entity.transform.position, entity.finalPosition);
            HandleMoveAStar(bestPath, false, entity);
        }
    }
}
