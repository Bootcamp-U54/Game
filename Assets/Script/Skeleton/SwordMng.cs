using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMng : MonoBehaviour
{
    public int damage;
    public bool canDmg;

    public AudioSource swordSaplama;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if(canDmg==true)
            {
                collision.gameObject.GetComponent<PlayerController>().getDamage(damage);
                if(swordSaplama!=null)
                {
                    swordSaplama.Play();
                }
                canDmg = false;
            }
            
        }
    }
}
