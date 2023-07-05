using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photoMng : MonoBehaviour
{
    public float speed;
    public int yon;
    public int damage;
    private void Start()
    {
        Destroy(this.gameObject, 2f);
    } 
    void Update()
    {
        Vector2 vec = new Vector2(yon * speed * Time.deltaTime, 0);
        transform.Translate(vec);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().getDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
