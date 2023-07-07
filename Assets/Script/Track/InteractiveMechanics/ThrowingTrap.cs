using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingTrap : MonoBehaviour, IBulletSpawner
{
    public Transform spawnPoints;
    public GameObject parentObject;


    // Update is called once per frame
    void Update()
    {
        FireBullet();
    }
    public void FireBullet()
    {
        Transform selectedSpawnPoint = spawnPoints;
        GameObject bullet = ObjectPool.Instance.GetTrap();
        if (bullet != null)
        {
            bullet.transform.position = selectedSpawnPoint.position;
            bullet.SetActive(true);
            bullet.transform.SetParent(parentObject.transform);
        }

    }

}
