using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidMng : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().getDamage((int)collision.GetComponent<PlayerController>().health);
        }
    }
}
