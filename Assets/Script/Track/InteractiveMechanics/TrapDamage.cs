using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damage = 3;
    public bool canDestroy = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().getDamage(damage);
            if(canDestroy)
            {
                Destroy(gameObject);

            }
       
        }
    }
}
