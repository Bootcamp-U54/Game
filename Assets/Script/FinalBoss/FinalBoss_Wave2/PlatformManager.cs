using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformManager : MonoBehaviour
{


   public float collisionTime = 0f;  // Temas süresi
    public bool isColliding = false;  // Temas durumu
    public Sprite currentSprite;

    private void Start()
    {
        currentSprite= GetComponent<SpriteRenderer>().sprite; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = true;
            collisionTime = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
          isColliding = false;
     
    }

    private void Update()
    {
        if (isColliding)
        {
            collisionTime += Time.deltaTime;  
            
        }

        if (collisionTime >= 1.5)
        {
            StartCoroutine(Platform());
            collisionTime = 0f;
            isColliding = false;
        }
       
    }
   IEnumerator Platform()
    {
        Camera.main.GetComponent<Camera>().DOShakePosition(0.1f, 0.1f, fadeOut: true);
        //  gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        //gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().sprite = currentSprite;
        GetComponent<CapsuleCollider2D>().enabled = true;
       
    }

}
