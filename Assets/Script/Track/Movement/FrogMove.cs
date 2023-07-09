using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMove : MonoBehaviour
{
    public int speed;

    public float maxJump, minJump;
    public float currentJump;

    private Rigidbody2D rb;

    public int damage = 1;

    public float maxXPos;
    public Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
     

        if(transform.position.x<maxXPos)
        {
            Destroy(gameObject);
        }

        anim.SetFloat("velocityY", rb.velocity.y);
    }
    private void Jump()
    {

        currentJump = Random.Range(minJump, maxJump);
        // Sadece sola doðru zýplamasý için yatay kuvvet uygula
        rb.velocity = new Vector2(-speed, currentJump);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Platforma temas ettiðinde
        if (collision.gameObject.CompareTag("Platform"))
        {
            Jump();
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().getDamage(damage);
            Destroy(gameObject);
        }
    }

    

 

   
}
