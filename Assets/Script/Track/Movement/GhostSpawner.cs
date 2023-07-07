using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject parentObject;
    private void Update()
    {
        FireBullet();
    }
    public void FireBullet()
    {
      
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform selectedSpawnPoint = spawnPoints[i];

            GameObject bullet = ObjectPool.Instance.GetGhost();
            if (bullet != null)
            {
                bullet.transform.position = selectedSpawnPoint.position;
                bullet.transform.rotation = Quaternion.Euler(0,0,0);
                bullet.transform.SetParent(parentObject.transform);
                bullet.SetActive(true);
            }
        }
    }
}
