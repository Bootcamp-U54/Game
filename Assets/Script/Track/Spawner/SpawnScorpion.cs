using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScorpion : MonoBehaviour
{
    public GameObject scorpionPrefab; // Akep prefab'ý

    public Transform[] spawnPoints; // Spawn noktalarý
    public GameObject parentObject;

    private void Awake()
    {
        SpawnScorpionAtAllPoints();
    }

    private void SpawnScorpionAtAllPoints()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 spawnPosition = spawnPoints[i].position; // Spawn noktasýnýn pozisyonu
            Quaternion spawnRotation = spawnPoints[i].rotation; // Spawn noktasýnýn rotasyonu

            GameObject scorpion = Instantiate(scorpionPrefab, spawnPosition, spawnRotation); // Akep objesi spawn edilir
            scorpion.transform.SetParent(parentObject.transform);
        }
    }
}
