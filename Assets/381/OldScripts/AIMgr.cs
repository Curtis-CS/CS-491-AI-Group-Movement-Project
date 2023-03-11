using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class is used for contorling the 'AI' part of the ships, like automatic movement to a position, following, intercepting,
//And such
public class AIMgr : MonoBehaviour
{
    public static AIMgr inst;
    private void Awake()
    {
        inst = this;
    }

    public RaycastHit hit;
    public int layerMask;
    
    //How far to look for an entity that was close to the click for selecting
    public float clickRadius = 100;

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
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 2);
                Vector3 pos = hit.point;
                pos.y = 0;
                Entity381 ent = findClosestEntInRadius(pos, clickRadius);
                //if the click was not on an entity
                if (ent == null)
                {
                    //If holding alt when right clicking not on an entity, teleport them their
                    if(Input.GetKey(KeyCode.LeftAlt))
                    {
                        //Teleport Command
                        HandleTeleport(pos);
                        //print("Teleport Branch");
                    }
                    //Else just start moving them there
                    else
                    {
                        //move command
                        HandleMove(pos);
                    }
                }
                //If the right click was on an entity
                else
                {
                    //And they were holding E, intercept the entity
                    if (Input.GetKey(KeyCode.E))
                    {
                        //intercept entity
                        HandleIntercept(ent);
                    }
                    //Else, just follow the entity
                    else
                    {
                        //follow entity
                        HandleFollow(ent);
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

    void HandleMove(Vector3 point)
    {
        MoveCommand move = new MoveCommand(SelectionMgr.inst.selectedEntity, point);
        UnitAI uai = SelectionMgr.inst.selectedEntity.GetComponent<UnitAI>();
        if (Input.GetKey(KeyCode.T))
        {
            uai.AddCommand(move);
        }
        else
        {
            uai.SetCommand(move);
        }
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
}
