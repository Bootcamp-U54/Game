using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Transform targetPoint1; // Ýlk hedef nokta
    public Transform targetPoint2; // Ýkinci hedef nokta
    public float speed = 2f; // Hareket hýzý

    private Vector3 currentTarget; // Þu anki hedef nokta
    private bool movingToTarget1 = false; // Ýlk hedefe hareket ediliyor mu?
    private bool characterOnPlatform = false; // Karakter platformda mý?

    private void Update()
    {
        if (characterOnPlatform)
        {
            if (movingToTarget1)
            {
                MoveToTarget(targetPoint1.position);
              
            }
            else
            {
                MoveToTarget(targetPoint2.position);
            }
        }
    }

    private void MoveToTarget(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == target)
        {
            movingToTarget1 = !movingToTarget1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
            GameObject platform = gameObject;
            characterOnPlatform = true;
        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            collision.collider.transform.SetParent(null);
         characterOnPlatform = true;
    }

    private void OnDrawGizmos()
    {
        if (targetPoint1 != null && targetPoint2 != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(targetPoint1.position, targetPoint2.position);

            float distance = Vector3.Distance(targetPoint1.position, targetPoint2.position);
            Vector3 labelPosition = (targetPoint1.position + targetPoint2.position) / 2f;
            Handles.Label(labelPosition, distance.ToString("F2") + " units");
        }
    }
}
