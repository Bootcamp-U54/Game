using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossWave3FireMng : MonoBehaviour
{
    [Header("Public")]
    public float damageDuration;
    public int fireDamage;


    [Space(10)]
    [Header("Private")]
    public bool fireIsOpen = false;
    public bool playerInFire = false;
    public GameObject player;
    public float currentDamageDuration;


    private void Update()
    {
        if(fireIsOpen ==true && playerInFire==true)
        {
            if(currentDamageDuration>=damageDuration)
            {
                player.GetComponent<PlayerController>().getDamage(fireDamage);
                currentDamageDuration = 0;
            }
            currentDamageDuration += Time.deltaTime;
          
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            playerInFire = true;
            player = collision.gameObject.gameObject;
            currentDamageDuration = damageDuration;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInFire = false;
            player = null;
        }
    }
}
