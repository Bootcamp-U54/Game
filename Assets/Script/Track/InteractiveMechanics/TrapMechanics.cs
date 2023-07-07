using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class TrapMechanics : MonoBehaviour
{

    public GameObject targetObject; // Çarpma sonucu yukarý fýrlatýlacak hedef obje
    public GameObject frogMec;
    public float upwardForce = 10f; // Yukarý doðru uygulanacak kuvvet miktarý

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("evet");
            Rigidbody2D targetRb = targetObject.GetComponent<Rigidbody2D>();
            targetRb.bodyType = RigidbodyType2D.Dynamic;
            targetRb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
            gameObject.SetActive(false);
            frogMec.SetActive(true);
            
        }
     

    }















}
