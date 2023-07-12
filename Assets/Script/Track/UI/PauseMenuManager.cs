using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject pauseMenu; // Pause men�s�n�n referans�
    public GameObject optionsPanel; // Pause men�s�n�n referans�
    public bool isPaused; // Oyunun duraklat�l�p duraklat�lmad���n� kontrol etmek i�in flag
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
                ResumeGame(); // E�er oyun duraklat�lm��sa Esc tu�una bas�nca oyunu devam ettir
               
            }
            else
            {
                PauseGame(); // E�er oyun devam ediyorsa Esc tu�una bas�nca oyunu duraklat
              
            }
        
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Pause men�s�n� a�
        Time.timeScale = 0f; // Oyun zaman �l�e�ini 0 olarak ayarla, oyun durur
        Cursor.visible = true; // Fare imleci g�r�n�r hale gelsin
        Cursor.lockState = CursorLockMode.None; // Fare imleci ekranda serbest�e hareket edebilsin
        isPaused = true; // Oyun duraklat�ld�
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Pause men�s�n� kapat
        optionsPanel.SetActive(false);
        Time.timeScale = 1f; // Oyun zaman �l�e�ini 1 olarak ayarla, oyun devam etsin
        Cursor.visible = false; // Fare imleci gizlensin
        Cursor.lockState = CursorLockMode.Locked; // Fare imleci ekranda sabitlensin
        isPaused = false; // Oyun devam ediyor
    }

    public void GoToOptions()

    {
        pauseMenu.SetActive(false);
        optionsPanel.SetActive(true); // Options panelini a�
    }
    public void GoToPauseMenu()
    {
        pauseMenu.SetActive(true);
        optionsPanel.SetActive(false); // Options panelini kapa
    }


    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Main Menu sahnesine ge�i� yap

    }
}
