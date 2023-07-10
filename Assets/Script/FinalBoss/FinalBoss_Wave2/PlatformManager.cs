using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformManager : MonoBehaviour
{


    public float collisionTime;
    public float currentCollisionTime;
    public bool isColliding = false;  
    public Sprite currentSprite;

    public float openY, closeY;

    private void Start()
    {
        currentSprite= GetComponent<SpriteRenderer>().sprite;
        currentCollisionTime = collisionTime;
        openY = transform.position.y;
        closeY = transform.position.y - 10;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void Update()
    {

        if(isColliding==true)
        {
            currentCollisionTime -= Time.deltaTime;

            if(currentCollisionTime<=0)
            {
                StartCoroutine(Platform());
            }
        }
    }
   IEnumerator Platform()
    {

        isColliding = false;
        currentCollisionTime = collisionTime;

        Camera.main.GetComponent<Camera>().DOShakePosition(0.1f, 0.1f, fadeOut: true);

        StartCoroutine(closePlatform());
        yield return new WaitForSeconds(1f+0.5f);

        StartCoroutine(openPlatform());


    }

    IEnumerator closePlatform()
    {

        transform.DOMoveY(closeY, 1f);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator openPlatform()
    {
        transform.DOMoveY(openY, 1f);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().sprite = currentSprite;
        GetComponent<BoxCollider2D>().enabled = true;

    }

}


