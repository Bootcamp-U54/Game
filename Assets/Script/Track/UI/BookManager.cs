using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class BookManager : MonoBehaviour
{
    PauseMenuManager pauseMenuManager;
    public TextMeshProUGUI[] page;
    public float fadeDuration = 2f;
    public int currentIndex = 0;
    public GameObject bookCanvas;


    private void Start()
    {
        for (int i = 1; i < page.Length; i++)
        {
            page[i].DOFade(0f, 0f);
        }

        
    }
    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.B) )
        {
            Cursor.visible = true; // Fare imleci görünür hale gelsin
            Cursor.lockState = CursorLockMode.None; // Fare imleci ekranda serbestçe hareket edebilsin
            bookCanvas.SetActive(true);
        
        }
    }
    public void OnClickNextPage()
    {
        if (currentIndex >= 0 && currentIndex + 1 < page.Length)
        {
            page[currentIndex].DOFade(0f, fadeDuration);
            page[currentIndex + 1].DOFade(1f, fadeDuration);
            currentIndex++;
        }
    }
    public void OnClickBackpage()
    {
        if (currentIndex > 0)
        {
            page[currentIndex].DOFade(0f, fadeDuration);
            page[currentIndex - 1].DOFade(1f, fadeDuration);
            currentIndex--;
        }
    }
    public void ResumeGame()
    {
        bookCanvas.SetActive(false);
        Time.timeScale = 1f; // Oyun zaman ölçeðini 1 olarak ayarla, oyun devam etsin
        Cursor.visible = false; // Fare imleci gizlensin
        Cursor.lockState = CursorLockMode.Locked; // Fare imleci ekranda sabitlensin
    }
}
