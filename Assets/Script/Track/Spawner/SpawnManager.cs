using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager: MonoBehaviour
{
    public GameObject  prefab;
    public Transform spawnPoint;
    public float spawnInterval = 2f;

    private float spawnTimer = 0f;

    public int health;
    public bool canGetDamage = true;
    public bool canSpawn = true;

    public float maxXPos;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && canSpawn==true)
        {
            Spawn();
            spawnTimer = 0f;
        }
    }

    private void Spawn()
    {
        GameObject newFrog = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        newFrog.GetComponent<FrogMove>().maxXPos = maxXPos;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            getDamage(collision.gameObject.GetComponent<BulletManager>().damage);
            Destroy(collision.gameObject);
        }

    }



    public void getDamage(int dmg)
    {
        if (canGetDamage == true)
        {
            this.gameObject.GetComponent<damageAnim>().startAnim();
            if (health >= dmg)
            {
                health -= dmg;
            }
            else
            {
                health = 0;
            }

            if (health <= 0)
            {
                canSpawn = false;
                gameObject.SetActive(false);
            }
        }
    }


  
}
