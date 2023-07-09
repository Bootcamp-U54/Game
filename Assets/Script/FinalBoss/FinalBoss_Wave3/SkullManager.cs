using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public float force;


    public int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = player.transform.position - transform.position; rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        //float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg; transform.rotation = Quaternion.Euler(0, 0, rot);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            destroyObject();
        }

        if (collision.gameObject.tag == "Player")
        {

            collision.gameObject.GetComponent<PlayerController>().getDamage(damage);
            destroyObject();
        }
    }

    public void destroyObject()
    {
        Destroy(this.gameObject);
    }

   
}
