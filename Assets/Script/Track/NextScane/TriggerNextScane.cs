using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerNextScane : MonoBehaviour
{
   
    public Image blackImage; // Siyah Image referansý
    public GameObject otherPanel; // Diðer panelin referansý
    private void Start()
    {
        blackImage.color = Color.black;
        blackImage.DOFade(0f, 0f);
    } 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FadeInAndActivatePanel();
        }
    }
   
    public void FadeInAndActivatePanel()
    {
        blackImage.DOFade(1f, .5f).OnComplete(NextScane);
    }
    private void NextScane()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Mevcut sahnenin index numarasýný al
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings; // Bir sonraki sahnenin index numarasýný hesapla
       SceneManager.LoadScene(nextSceneIndex); 
          
       
     
    }
}
