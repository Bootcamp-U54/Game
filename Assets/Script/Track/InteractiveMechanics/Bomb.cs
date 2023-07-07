using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float countdown = 2f; // Patlama için geri sayým süresi
    public GameObject explosionEffect; // Patlama efekti için kullanýlacak obje
    public float shakeMagnitude = 0.1f; // Sallanma büyüklüðü
    public Color shakeColor = Color.red; // Sallanma sýrasýnda kullanýlacak renk

    private bool hasExploded = false; // Patlamanýn gerçekleþip gerçekleþmediðini kontrol etmek için
    private SpriteRenderer spriteRenderer; // Bomba sprite'ýnýn renderer bileþeni

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Bomba sprite'ýnýn renderer bileþenini al
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Eðer karakter ile temas olduysa
        {
            StartCoroutine(ShakeAndExplode(gameObject)); // Sallanma ve patlama coroutine'unu baþlat
        }
    }

    private IEnumerator ShakeAndExplode(GameObject player)
    {
        if (!hasExploded)
        {
            hasExploded = true;

            // Bomba sallanma efekti için
            Vector3 originalPosition = transform.position;

            float elapsed = 0f;
            while (elapsed < countdown)
            {
                // Rastgele bir sallanma yönlü kuvvet uygula
                Vector3 randomOffset = Random.insideUnitCircle * shakeMagnitude;
                transform.position = originalPosition + randomOffset;

                // Bomba rengini deðiþtir
                spriteRenderer.color = shakeColor;

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Bomba konumunu orijinal pozisyona geri yükle
            transform.position = originalPosition;

            // Patlama efekti oluþtur
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            // Bombayý yok et
            Destroy(gameObject);
        }
    }
}