using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : Move, IBulletSpawner
{
    public Transform[] spawnPoints; // Spawn noktalarý
    public float fireRate = 1f; // Mermi atma hýzý
    private float fireTimer = 0f; // Mermi atma zamanlayýcýsý
    public GameObject parentObject;
    private void Update()
    {
        MoveObjectBetweenPoints();

        // Mermi atma zamanlayýcýsýný güncelle
        fireTimer += Time.deltaTime;

        // Eðer mermi atma zamaný geldiyse, tüm spawn noktalarýndan mermileri ayný anda at
        if (fireTimer >= fireRate)
        {
           FireBullet();
            fireTimer = 0f; // Zamanlayýcýyý sýfýrla
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
