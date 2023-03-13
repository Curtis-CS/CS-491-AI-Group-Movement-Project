// Made by: Cody Jackson
// Edited by: Cody Jackson | codyj@nevada.unr.edu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script implements potential fields for ships or world obstacles. 
/// Place this onto any object you want to give a potential field to.
/// </summary>

public class PotentialField : MonoBehaviour
{
    public float fieldSizeMultiplier = 1f; // Multiplier for the size of the potential field
    public float maxForce = 1f; // Maximum force that can be applied to the ship

    [SerializeField] private List<GameObject> shipsInField = new List<GameObject>(); // List of ships in the potential field

    private void Start()
    {
        shipsInField.Add(this.transform.parent.gameObject); // The parent ship is always in this potential field
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Check if this object is a ship.
        if(collision.gameObject.GetComponent<Entity381>() != null)
        {
            shipsInField.Add(collision.gameObject); // Add it to the list
            ModifyHeading(collision.gameObject.GetComponent<Entity381>()); // Change it's course to avoid collision
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        // Check if this object is a ship.
        if (collision.gameObject.GetComponent<Entity381>() != null)
        {
            shipsInField.Remove(collision.gameObject); // Remove it from the list
            RestoreHeading(collision.gameObject.GetComponent<Entity381>()); // Restore it's course
        }
    }

    // Alter a ship's course
    public void ModifyHeading(Entity381 ship)
    {
        Debug.Log("Changing course...");
        Vector3 pf = ship.transform.position;

        // Calculate the potential field force from all the ships and obstacles in the field
        foreach (GameObject obj in shipsInField)
        {
            if (obj != ship.gameObject) // Don't include ship being modified
            {
                Vector3 vect = obj.transform.position; // Position of object
                Vector3 diff = pf - vect; // Calculate difference
                pf += diff; // Adjust PF based on difference
                //Debug.Log("GO HERE: " + pf + "  -- Adjustment> " + vect + "  -- Difference> " + diff);
            }
        }

        

        // Calculate the new desired heading and speed of the ship
        float angleDiff = ship.desHeading - ship.heading;
        ship.desSpeed = ship.minSpeed + ((ship.maxSpeed - ship.minSpeed) * Mathf.Cos(angleDiff));
        ship.desHeading = Mathf.Atan2(pf.x, pf.z);
        //Debug.Log("New course: " + ship.desHeading + " <heading : speed> " + ship.desSpeed + " for: " + ship.gameObject.name);
    }

    // Restore a ship's course
    public void RestoreHeading(Entity381 ship)
    {
        Debug.Log("Returning to pathfinding...");
        AIMgr.inst.ResumeAstar(ship);
        //ship.desSpeed = 0;
        //ship.desHeading = ship.heading;
    }

    // Restore all ships in current field
    public void RestoreAll()
    {
        foreach (GameObject ship in shipsInField)
        {
            if (ship != this.transform.parent.gameObject)
            {
                Debug.Log("Returning to pathfinding...");
                RestoreHeading(ship.GetComponent<Entity381>());
            } 
        }
    }
}
