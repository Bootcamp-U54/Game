using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossParallax : MonoBehaviour
{
    public float speed;
    public float parallaxMultipler;
 
    public Vector3 cameraTransform;
    private Vector3 previousCameraPosition;

    public float startPosY;

    public int spriteCount;
    private float loopX;

    void Start()
    {

        loopX = startPosY + (21 * spriteCount);


    }
    private void Update()
    {
        //cameraTransform += new Vector3(cameraTransform.x, cameraTransform.y, cameraTransform.z);
        cameraTransform.x = -speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        float deltaX = (cameraTransform.x - previousCameraPosition.x) * parallaxMultipler;
        float deltaY = (cameraTransform.y - previousCameraPosition.y) * parallaxMultipler;
        float moveAmount = cameraTransform.x * (1 - parallaxMultipler);
        transform.Translate(new Vector3(deltaX, deltaY, 0));

        if(gameObject.transform.position.y>loopX)
        {
            transform.position = new Vector3(transform.position.x, startPosY, transform.position.z);
        }


    }


   
}
