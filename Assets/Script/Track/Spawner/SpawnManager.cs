using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager: MonoBehaviour
{
    public GameObject  prefab;
    public Transform spawnPoint;
    public float spawnInterval = 2f;

    private float spawnTimer = 0f;


    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            Spawn();
            spawnTimer = 0f;
        }
    }

    private void Spawn()
    {
        GameObject newFrog = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        


    }
}
