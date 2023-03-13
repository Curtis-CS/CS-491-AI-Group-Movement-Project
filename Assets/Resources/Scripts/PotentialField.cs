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
    public float fieldSizeMultiplier = 2f; // Multiplier for the size of the potential field
    public float maxForce = 1f; // Maximum force that can be applied to the ship

    private List<GameObject> shipsInField = new List<GameObject>(); // List of ships in the potential field

    // Collider2D because the ships always navigate in 2D space.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if this object is a ship.
        if(collision.gameObject.GetComponent<Entity381>() != null)
        {
            shipsInField.Add(collision.gameObject); // Add it to the list
            ModifyHeading(collision.gameObject.GetComponent<Entity381>()); // Change it's course to avoid collision
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
        // Calculate force based on distance
        Vector2 force = transform.position - ship.transform.position;
        float distance = force.magnitude;
        force.Normalize();
        force *= maxForce / distance;

        // Calculate new speed & heading based on force
        Vector2 newVelocity = new Vector2(ship.speed * Mathf.Cos(ship.heading * Mathf.Deg2Rad), ship.speed * Mathf.Sin(ship.heading * Mathf.Deg2Rad)) + force; // Math ;_;
        ship.desSpeed = newVelocity.magnitude; // Set desired speed
        ship.desHeading = Mathf.Atan2(newVelocity.y, newVelocity.x) * Mathf.Rad2Deg; // Set desired heading
    }

    // Restore a ship's course
    public void RestoreHeading(Entity381 ship)
    {
        ship.desSpeed = ship.speed;
        ship.desHeading = ship.heading;
    }

    // Restore all ships in current field
    public void RestoreAll()
    {
        foreach (GameObject ship in shipsInField)
        {
            RestoreHeading(ship.GetComponent<Entity381>());
        }
    }
}
