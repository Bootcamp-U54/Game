using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject pauseMenu; // Pause menüsünün referansý
    public GameObject optionsPanel; // Pause menüsünün referansý
    public bool isPaused; // Oyunun duraklatýlýp duraklatýlmadýðýný kontrol etmek için flag
    public bool canOpen = true;
 

    private void Start()
    {
      
        Cursor.visible = false; // Fare imleci gizlensin
        Cursor.lockState = CursorLockMode.Locked; // Fare imleci ekranda sabitlensin
       
    }
    private void Update()
    {
        PlayerController targetScript = targetObject.GetComponent<PlayerController>();

        if (Input.GetKeyDown(KeyCode.Escape) &&canOpen==true&& targetScript.deathSfxIsPlay==false)
        {
            if (isPaused )
            {
                ResumeGame(); // Eðer oyun duraklatýlmýþsa Esc tuþuna basýnca oyunu devam ettir
               
            }
            else
            {
                PauseGame(); // Eðer oyun devam ediyorsa Esc tuþuna basýnca oyunu duraklat
              
            }
        
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Pause menüsünü aç
        Time.timeScale = 0f; // Oyun zaman ölçeðini 0 olarak ayarla, oyun durur
        Cursor.visible = true; // Fare imleci görünür hale gelsin
        Cursor.lockState = CursorLockMode.None; // Fare imleci ekranda serbestçe hareket edebilsin
        isPaused = true; // Oyun duraklatýldý
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Pause menüsünü kapat
        optionsPanel.SetActive(false);
        Time.timeScale = 1f; // Oyun zaman ölçeðini 1 olarak ayarla, oyun devam etsin
        Cursor.visible = false; // Fare imleci gizlensin
        Cursor.lockState = CursorLockMode.Locked; // Fare imleci ekranda sabitlensin
        isPaused = false; // Oyun devam ediyor
    }

    public void GoToOptions()

    {
        pauseMenu.SetActive(false);
        optionsPanel.SetActive(true); // Options panelini aç
    }
    public void GoToPauseMenu()
    {
        pauseMenu.SetActive(true);
        optionsPanel.SetActive(false); // Options panelini kapa
    }


    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Main Menu sahnesine geçiþ yap

    }
}
