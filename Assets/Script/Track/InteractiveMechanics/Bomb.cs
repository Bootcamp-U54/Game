using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bomb : MonoBehaviour
{
    public float countdown = 2f; // Patlama i�in geri say�m s�resi
    public GameObject explosionEffect; // Patlama efekti i�in kullan�lacak obje
    public float shakeMagnitude = 0.1f; // Sallanma b�y�kl���
    public Color shakeColor = Color.red; // Sallanma s�ras�nda kullan�lacak renk

    private bool hasExploded = false; // Patlaman�n ger�ekle�ip ger�ekle�medi�ini kontrol etmek i�in
    private SpriteRenderer spriteRenderer; // Bomba sprite'�n�n renderer bile�eni


    public GameObject player;
    public int damage = 3;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Bomba sprite'�n�n renderer bile�enini al
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // E�er karakter ile temas olduysa
        {
            StartCoroutine(ShakeAndExplode()); // Sallanma ve patlama coroutine'unu ba�lat
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            player = null;
        }
    }

    private IEnumerator ShakeAndExplode()
    {
        if (!hasExploded)
        {
            hasExploded = true;

            // Bomba sallanma efekti i�in
            Vector3 originalPosition = transform.position;

            float elapsed = 0f;
            while (elapsed < countdown)
            {
                // Rastgele bir sallanma y�nl� kuvvet uygula
                Vector3 randomOffset = Random.insideUnitCircle * shakeMagnitude;
                transform.position = originalPosition + randomOffset;

                // Bomba rengini de�i�tir
                spriteRenderer.color = shakeColor;

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Bomba konumunu orijinal pozisyona geri y�kle
            transform.position = originalPosition;

            // Patlama efekti olu�tur
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            if(player !=null)
            {
                player.GetComponent<PlayerController>().getDamage(damage);
            }

            // Bombay� yok et
            Camera.main.DOShakePosition(0.5f, 3f);
            Destroy(gameObject);

        }
    }
}