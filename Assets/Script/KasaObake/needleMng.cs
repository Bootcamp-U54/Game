using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class needleMng : MonoBehaviour
{
    public Rigidbody2D rb;
    public float rotateSpeed;
    public float speed;
    public Transform target;
    public int damage;
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = 1;
        Vector2 direction = (Vector2)target.transform.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<PlayerController>().getDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
