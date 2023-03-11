using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is a git test
public class AIMgr : MonoBehaviour
{
    public static AIMgr inst;
    private void Awake()
    {
        inst = this;
    }

    public RaycastHit hit;
    public int layerMask;

    public float clickRadius = 100;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 9; ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, layerMask))
            {
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 2);
                Vector3 pos = hit.point;
                pos.y = 0;
                Entity381 ent = findClosestEntInRadius(pos, clickRadius);
                if (ent == null)
                {
                    if(Input.GetKey(KeyCode.LeftAlt))
                    {
                        //Teleport Command
                        HandleTeleport(pos);
                        //print("Teleport Branch");
                    }
                    else
                    {
                        //move command
                        HandleMove(pos);
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        //intercept entity
                        HandleIntercept(ent);
                    }
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
