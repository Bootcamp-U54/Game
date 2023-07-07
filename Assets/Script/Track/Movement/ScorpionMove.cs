using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionMove : MonoBehaviour, IBulletSpawner
{
    public Transform[] spawnPoints;
    public GameObject objectToActivate;
    private bool isActive = false;
    public GameObject parentObject;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        float randomInterval = Random.Range(1.2f, 5f);
        InvokeRepeating("ToggleObjectActivation", randomInterval, 2f);
        parentObject = GameObject.Find("Scorpion");
    }


    public void FireBullet()
    {
        int[] rotations = new int[] { -30, 0, 30 };
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform selectedSpawnPoint = spawnPoints[i];

            GameObject bullet = ObjectPool.Instance.GetScorpion();
            if (bullet != null)
            {
                bullet.transform.position = selectedSpawnPoint.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, rotations[i]);
                bullet.SetActive(true);
                bullet.transform.SetParent(parentObject.transform);

            }
        }
    }
    void ToggleObjectActivation()
    {
        isActive = !isActive;
        objectToActivate.SetActive(isActive);

       if (isActive)
        {
            FireBullet();
        }
        else
        {
            Invoke("DeactivateObject", 1f);
        }
    }

    void DeactivateObject()
    {
        objectToActivate.SetActive(false);
    }








}
