using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakePlatform : MonoBehaviour
{
    public float shakeDuration = 4f;
    public float shakeIntensity = 0.1f;
    private bool isShaking = false;
    private Vector3 initialPosition;

    public AudioSource sfx;


    private void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (isShaking)
        {
            ShakePlatforms();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {

            collision.collider.transform.SetParent(transform);
            ShakeAndRemovePlatform();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            collision.collider.transform.SetParent(null);
    }
    private void ShakePlatforms()
    {
        transform.position = initialPosition + Random.insideUnitSphere * shakeIntensity;
    }

    private void ShakeAndRemovePlatform()
    {
        isShaking = true;
        Invoke("RemovePlatform", shakeDuration);
    }

    private void RemovePlatform()
    {
        sfx.Play();
        Destroy(gameObject);
    }
}
