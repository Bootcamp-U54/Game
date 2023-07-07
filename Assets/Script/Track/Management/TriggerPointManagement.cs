using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPointManagement : MonoBehaviour
{
    [SerializeField] private GameObject[] mechanical;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < mechanical.Length; i++)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                mechanical[i].SetActive(true);
            }
        }

    }
}
