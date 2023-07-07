using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour
{
    public Transform startPoint; // Ba�lang�� noktas�
    public Transform endPoint; // Biti� noktas�
    public float moveSpeed = 5f; // Hareket h�z�
    public float arrivalThreshold = 0.1f; // Mesafe e�ik de�eri
    protected bool movingToEnd = true; // Ba�lang��ta biti� noktas�na do�ru hareket edilsin mi?

    protected void MoveObjectBetweenPoints()//Haraket
    {
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint.position) <= arrivalThreshold)
            {
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPoint.position) <= arrivalThreshold)
            {
                movingToEnd = true;
            }
        }
    }


}
