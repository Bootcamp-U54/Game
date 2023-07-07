using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMove : MonoBehaviour
{
    public int maxJumpCount;
    public int speed;
    public int jump;

    private int jumpCount = 0;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        Explode();
    }
    private void Jump()
    {
        // Sadece sola do�ru z�plamas� i�in yatay kuvvet uygula
        rb.velocity = new Vector2(-speed, jump);
        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Platforma temas etti�inde
        if (collision.gameObject.CompareTag("Platform"))
        {
            Jump();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Platform temas�n� kaybetti�inde
        if (collision.gameObject.CompareTag("Platform"))
        {

        }
    }

    private void Explode()
    {
        if (jumpCount == maxJumpCount)
        {
            Destroy(gameObject);
        }
    }
}
