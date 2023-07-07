using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class TrapMechanics : MonoBehaviour
{

    public GameObject targetObject; // �arpma sonucu yukar� f�rlat�lacak hedef obje
    public GameObject frogMec;
    public float upwardForce = 10f; // Yukar� do�ru uygulanacak kuvvet miktar�

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
