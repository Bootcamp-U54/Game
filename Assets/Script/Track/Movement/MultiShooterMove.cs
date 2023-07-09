using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShooterMove : MonoBehaviour
{
    public float speed = 5f;
    public float height = 5f;
    public float gravity = 9.8f;
    public Transform[] spawnPoints; // Prefab'ın fırlatılacağı nokta
    public GameObject parentObject;

    public bool canGetDamage = true;
    public int health;

    void Update()
    {
        spawnPoints=GetComponentsInChildren<Transform>();
        FireBullet();
    }
    public void FireBullet()
    {
      
        int[] rotations = new int[] { 30, 20, 10 };
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform selectedSpawnPoint = spawnPoints[i];

            GameObject bullet = ObjectPool.Instance.GetScorpion();
            if (bullet != null)
            {
                bullet.transform.position = selectedSpawnPoint.position;

                // Açıyı belirlemek için Quaternion.Euler kullanın
                Quaternion rotation = Quaternion.Euler(0f, 0f, rotations[i]);
                bullet.transform.rotation = rotation;

                bullet.SetActive(true);
                bullet.transform.SetParent(parentObject.transform);

                // Bombayı fırlatma işlemi
                float horizontalspeed = speed;
                float verticalspeed = Mathf.Sqrt(2f * gravity * height);
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                Vector2 hareket = rotation * new Vector2(-horizontalspeed, verticalspeed); // Açıyı da hesaba katın
                bulletRigidbody.velocity = hareket;
                GetComponent<Animator>().SetTrigger("Fire");
            }
        }
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
                GetComponent<Animator>().SetBool("Death", true);
            }
        }
    }

   public void dest()
    {
        Destroy(gameObject);
    }

}
