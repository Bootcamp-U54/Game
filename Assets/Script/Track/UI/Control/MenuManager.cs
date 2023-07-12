using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject bookMenu;
 

    private bool isBookMenuOpen = false;
    private bool isPauseMenuOpen = false;
  

    private void Update()
    {
    

       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isBookMenuOpen)
            {
                // Kitap menüsü açýksa, kapat
                CloseBookMenu();
            }
            else
            {
                // Ayarlar menüsünü aç veya kapat
                OpenSettingsMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if(isPauseMenuOpen)
            {
                CloseSettingsMenu();
            }
            else
            {
                OpenBookMenu();
            }
          
        }
     
    }

    private void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        isPauseMenuOpen = true;
    }
    private void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
        isPauseMenuOpen = false;
    }


    private void OpenBookMenu()
    {
        bookMenu.SetActive(true);
        isBookMenuOpen = true;
     
    }

    private void CloseBookMenu()
    {
        bookMenu.SetActive(false);
        isBookMenuOpen = false;
      
    }
}
