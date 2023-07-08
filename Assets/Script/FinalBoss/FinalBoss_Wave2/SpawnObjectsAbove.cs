using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsAbove : MonoBehaviour
{

    public void Start()
    {
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }
    public GameObject[] objectPrefab;
    public float minSpeed = 3f;
    public float maxSpeed = 5f;
    public float minAngle = 10f;
    public float maxAngle = 40f;
    public float spawnInterval = 1f;

    public Vector2 spawnArea = new Vector2(10f, 10f);


    public void SpawnObject()
    {

        float spawnX = Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f);
        float spawnY = 5f;

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(objectPrefab[i], spawnPosition, Quaternion.identity);
            float speed = Random.Range(minSpeed, maxSpeed);
            float angle = Random.Range(minAngle, maxAngle);
            Vector2 direction = Quaternion.Euler(0f, 0f, angle) * Vector2.down;

            Rigidbody2D objRigidbody = obj.GetComponent<Rigidbody2D>();
            objRigidbody.velocity = direction * speed;
        }
    }




}
