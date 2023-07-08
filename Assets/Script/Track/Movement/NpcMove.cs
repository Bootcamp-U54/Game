using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMove : Move
{
    public Animator anim;
    public int damage = 1;
    public BoxCollider2D boxCollider2d;
    public bool canMove = true;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(canMove)
        {
            MoveObjectBetweenPoints(true);
        }
  

    }

    public void dest()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().getDamage(damage);
            boxCollider2d.isTrigger = true;
            canMove = false;
            anim.SetTrigger("Dest");
        }
    }


}
