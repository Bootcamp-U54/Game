using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxMultipler;
    public bool isLoop = true;
    public Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private float startPosition,spriteWidth;
    private float startPosyY;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        startPosyY = cameraTransform.position.y;
        previousCameraPosition = cameraTransform.position;
        startPosition = transform.position.x;
        spriteWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;

    }

    private void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * parallaxMultipler;
        float deltaY = (cameraTransform.position.y - previousCameraPosition.y) * parallaxMultipler;
        float moveAmount = cameraTransform.position.x * (1 - parallaxMultipler);
       
          transform.Translate(new Vector3(deltaX, deltaY, 0));


       
       
        previousCameraPosition = cameraTransform.position;
        
        if(isLoop==true)
        {
            if (moveAmount > startPosition + spriteWidth)
            {
                transform.Translate(new Vector3(spriteWidth, 0, 0));
                startPosition += spriteWidth;
             
            }
            else if (moveAmount < startPosition - spriteWidth)
            {
                transform.Translate(new Vector3(-spriteWidth, 0, 0));
                startPosition -= spriteWidth;
               
            }
        }

        
    }
}
