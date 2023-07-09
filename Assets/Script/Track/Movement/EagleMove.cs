using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMove : Move, IBulletSpawner
{
    public Transform[] spawnPoints;
    public GameObject parentObject;
    private void Update()
    {
        MoveObjectBetweenPoints(true);
        FireBullet();
    }
    public void FireBullet()
    {
        int[] rotations = new int[] { -30, 0, 30 };
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform selectedSpawnPoint = spawnPoints[i];

            GameObject bullet = ObjectPool.Instance.GetEagle();

            if (bullet != null)
            {

                bullet.transform.position = selectedSpawnPoint.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, rotations[i]);
                bullet.SetActive(true);
                bullet.transform.SetParent(parentObject.transform);
                if(transform.localScale.x==1)
                {
                    bullet.GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    bullet.GetComponent<SpriteRenderer>().flipX = true;
                }
                
            }
        }
    }





}
