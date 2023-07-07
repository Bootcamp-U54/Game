using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : Move, IBulletSpawner
{
    public Transform[] spawnPoints; // Spawn noktalar�
    public float fireRate = 1f; // Mermi atma h�z�
    private float fireTimer = 0f; // Mermi atma zamanlay�c�s�
    public GameObject parentObject;
    private void Update()
    {
        MoveObjectBetweenPoints();

        // Mermi atma zamanlay�c�s�n� g�ncelle
        fireTimer += Time.deltaTime;

        // E�er mermi atma zaman� geldiyse, t�m spawn noktalar�ndan mermileri ayn� anda at
        if (fireTimer >= fireRate)
        {
           FireBullet();
            fireTimer = 0f; // Zamanlay�c�y� s�f�rla
        }
    }

    public void FireBullet()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform selectedSpawnPoint = spawnPoints[i];

            GameObject bullet = ObjectPool.Instance.GetCloud();
            if (bullet != null)
            {

                bullet.transform.position = selectedSpawnPoint.position;
                bullet.SetActive(true);
                bullet.transform.SetParent(parentObject.transform);
            }
        }
    }
}
