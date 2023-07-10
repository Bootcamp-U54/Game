using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextScane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NextScane();
        }
    }

    void NextScane()
    {
          int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Mevcut sahnenin index numarasýný al
            int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings; // Bir sonraki sahnenin index numarasýný hesapla

            SceneManager.LoadScene(nextSceneIndex); // Bir sonraki sahneyi yükle
    }
}
