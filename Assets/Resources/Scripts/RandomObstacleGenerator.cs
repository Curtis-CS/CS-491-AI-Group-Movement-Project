// Made by: Cody Jackson
// Edited by: Cody Jackson | codyj@nevada.unr.edu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacleGenerator : MonoBehaviour
{
    public static RandomObstacleGenerator inst;
    public void Awake()
    {
        inst = this;
    }

    // ----- //
    [Header("Settings")]
    public int shapesToSpawn = 20;
    public float minSize = 1f;
    public float maxSize = 4f;
    public bool makeCircles = true;
    [Tooltip("The area size across the objects will be spawned.")]
    public Vector2 areaSize = new Vector2(100f, 100f);
    [Header("Prefabs")]
    public GameObject circlePref;
    public GameObject rectPref;
    [Header("Details")]
    public List<GameObject> spawnObjects = new List<GameObject>();
    // ----- //

    public void SpawnShapes(int amount, bool doCircles)
    {
        for (int i = 0; i < amount; i++)
        {
            float size = Random.Range(minSize, maxSize); // Pick a random size
            Vector3 position = new Vector3(Random.Range(-areaSize.x / 2f, areaSize.x / 2f), 0, Random.Range(-areaSize.y / 2f, areaSize.y / 2f)); // Pick a random position
            GameObject shapePrefab = doCircles ? circlePref : rectPref; // Set which shape to spawn
            GameObject shape = Instantiate(shapePrefab, position, Quaternion.identity); // Spawn the shape at the position chosen
            shape.transform.localScale = new Vector3(size, 1f, size); // Set its size
            spawnObjects.Add(shape); // Add it to list
        }
        Debug.Log("Shape spawning complete.");
    }
}
