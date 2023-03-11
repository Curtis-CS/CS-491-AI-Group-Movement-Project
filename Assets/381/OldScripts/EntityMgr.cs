using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class Just to hold the list of all entities
public class EntityMgr : MonoBehaviour
{
    public static EntityMgr inst;
    private void Awake()
    {
        inst = this;
    }

    public List<Entity381> entities;
    
}
