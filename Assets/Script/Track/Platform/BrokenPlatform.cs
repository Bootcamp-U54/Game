using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrokenPlatform : Move
{
    public float breakDelay = 3f;
    public GameObject trapMec;

    private void Update()
    {
        MoveObjectBetweenPoints(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if(collision.transform.position.y>gameObject.transform.position.y)
            {
                collision.collider.transform.SetParent(transform);
                GameObject platform = gameObject;
                StartCoroutine(DestroyPlatformDelayed(platform, 3f));
                trapMec.SetActive(true);
            }
          
        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            collision.collider.transform.SetParent(null);
    }

    private IEnumerator DestroyPlatformDelayed(GameObject platform, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(platform);
    }
}
