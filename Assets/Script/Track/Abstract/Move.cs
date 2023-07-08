using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour
{
    public Transform startPoint; // Baþlangýç noktasý
    public Transform endPoint; // Bitiþ noktasý
    public float moveSpeed = 5f; // Hareket hýzý
    public float arrivalThreshold = 0.1f; // Mesafe eþik deðeri
    protected bool movingToEnd = true; // Baþlangýçta bitiþ noktasýna doðru hareket edilsin mi?

    protected void MoveObjectBetweenPoints(bool isNpc)//Haraket
    {
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint.position) <= arrivalThreshold)
            {
                movingToEnd = false;
                if(isNpc)
                {
                    flip();
                }
                
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPoint.position) <= arrivalThreshold)
            {
                movingToEnd = true;
                if (isNpc)
                {
                    flip();
                }
            }
        }
    }

    public void flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }


}
