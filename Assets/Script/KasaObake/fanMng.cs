using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanMng : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public float force;

    public float healtDuration;

    private bool isFixed;

    public int normalDamage;
    public int trapDamage;

    public Sprite closeSprite,openSprite;

    
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = closeSprite;
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = player.transform.position - transform.position; rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg; transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Ground")
        {
            setTrap();
        }

        if(collision.gameObject.tag=="Fan")
        {
            if(isFixed==false)
            {
                Destroy(this.gameObject);
            }
          
        }

        if(collision.gameObject.tag=="Player")
        {
            if(isFixed==true)
            {
                collision.gameObject.GetComponent<PlayerController>().getDamage(trapDamage);
                
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().getDamage(normalDamage);
            }
            Destroy(this.gameObject);
        }
    }

    public void setTrap()
    {
        GetComponent<SpriteRenderer>().sprite = openSprite;
        GetComponent<Animator>().SetBool("idle", true);
        isFixed = true;
        rb.bodyType = RigidbodyType2D.Static;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        Destroy(this.gameObject, healtDuration);
    }


}
