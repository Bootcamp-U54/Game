using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMng : MonoBehaviour
{
    public int health;
    public bool canGetDamage;


    public int damage;
    public bool canMove = true;
    public Transform target;
    public float speed;
    public float rotationSpeed;
    public Animator anim;

    private bool setPlayerPrefs = false;


    void Start()
    {
        setPlayerPrefs = true;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(canMove==true)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);

            Vector3 targetPosition = target.transform.position;
            Vector3 direction = targetPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (direction.x < 0)
            {
                // Eðer hedef obje solda ise, 180 derece ekleyerek ters yöne döndürme
                angle += 180f;


            }

            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Mathf.Sign(target.transform.position.x - this.gameObject.transform.position.x) == 1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<PlayerController>().getDamage(damage);
            canMove = false;
            GetComponent<BoxCollider2D>().enabled = false;
            anim.SetTrigger("Die");
        }

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
                canMove = false;
                GetComponent<BoxCollider2D>().enabled = false;

                if (setPlayerPrefs == true)
                {
                    int a = 0;
                    if (PlayerPrefs.HasKey("GhostCount") == true)
                    {
                        a = PlayerPrefs.GetInt("GhostCount");
                    }
                    else
                    {
                        PlayerPrefs.SetInt("GhostCount", 0);
                        a = PlayerPrefs.GetInt("GhostCount");
                    }

                    a++;
                    PlayerPrefs.SetInt("GhostCount", a);
                    Debug.LogWarning("Toplam Kýrýlan Tren Sayýsý : " + PlayerPrefs.GetInt("GhostCount"));

                    checkAchivement();

                    setPlayerPrefs = false;
                }

                anim.SetTrigger("Die");
            }
        }
    }

    public void checkAchivement()
    {
        int b = PlayerPrefs.GetInt("GhostCount");

        if (b > 10)
        {
            if (PlayerPrefs.GetInt("GhostDead1") == 0)
            {
                PlayerPrefs.SetInt("GhostDead1", 1);
                GameObject.Find("AchievementNotification").GetComponent<AchievementNotification>().getAchivement("GhostDead1");
            }
        }
        if (b > 25)
        {
            if (PlayerPrefs.GetInt("GhostDead2") == 0)
            {
                PlayerPrefs.SetInt("GhostDead2", 1);
                GameObject.Find("AchievementNotification").GetComponent<AchievementNotification>().getAchivement("GhostDead2");
            }
        }
        if (b > 50)
        {
            if (PlayerPrefs.GetInt("GhostDead3") == 0)
            {
                PlayerPrefs.SetInt("GhostDead3", 1);
                GameObject.Find("AchievementNotification").GetComponent<AchievementNotification>().getAchivement("GhostDead3");
            }
        }
    }

    public void dest()
    {
        Destroy(this.gameObject);
    }


}
