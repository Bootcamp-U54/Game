using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingPlatformManagement : MonoBehaviour
{
    public GameObject platform; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           platform.SetActive(true);
        }
    }
}
