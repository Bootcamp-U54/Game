using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScorpion : MonoBehaviour
{
    public GameObject scorpionPrefab; // Akep prefab'�

    public Transform[] spawnPoints; // Spawn noktalar�
    public GameObject parentObject;

    private void Awake()
    {
        SpawnScorpionAtAllPoints();
    }

    private void SpawnScorpionAtAllPoints()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 spawnPosition = spawnPoints[i].position; // Spawn noktas�n�n pozisyonu
            Quaternion spawnRotation = spawnPoints[i].rotation; // Spawn noktas�n�n rotasyonu

            GameObject scorpion = Instantiate(scorpionPrefab, spawnPosition, spawnRotation); // Akep objesi spawn edilir
            scorpion.transform.SetParent(parentObject.transform);
        }
    }
}
