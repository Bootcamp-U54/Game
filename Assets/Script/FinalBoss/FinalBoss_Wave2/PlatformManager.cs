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

    private void Start()
    {
        currentSprite= GetComponent<SpriteRenderer>().sprite;
        currentCollisionTime = collisionTime;
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
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);

        GetComponent<SpriteRenderer>().sprite = currentSprite;
        GetComponent<CapsuleCollider2D>().enabled = true;

       
    }

}
