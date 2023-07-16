using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using Button = UnityEngine.UI.Button;

public class SaveSystemMenu : MonoBehaviour
{
    public bool test;

    public Button PlayGameButton;
    public GameObject panel;
    public Vector3 panelStartPos;

    public Image blackImage;
    public GameObject areYouSourePanel;
    public GameObject setNamePanel;

    public TMP_InputField nameInput;

    public Toggle cheatToggle;
    public GameObject cheatPanel;
    public bool cheatMode;
    
    void Start()
    {
        panelStartPos = panel.transform.localScale;
        panel.transform.localScale = new Vector3(0, 0, 0);
        if (test==true)
        {
            PlayerPrefs.DeleteKey("Save");
        }

        if(PlayerPrefs.HasKey("Save")==true)
        {
            PlayGameButton.interactable = true;
        }
        else
        {
            PlayGameButton.interactable = false;
        }
        areYouSourePanel.transform.DOScale(Vector3.zero, 0);
        setNamePanel.transform.DOScale(Vector3.zero, 0);
        cheatPanel.transform.DOScale(0, 0);

        if(PlayerPrefs.HasKey("Cheat")==true)
        {
            if(PlayerPrefs.GetInt("Cheat")==1)
            {
                cheatToggle.isOn = true;
            }

            if (PlayerPrefs.GetInt("Cheat") == 0)
            {
                cheatToggle.isOn = false;
            }
        }

    }
    public void newGameAreYouSureOpen()
    {
        if(PlayerPrefs.HasKey("Save") == false)
        {
            showSetNamePanel();
        }
        else
        {
            areYouSourePanel.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1);
        }
       
    }
    public void newGameAreYouSureClose()
    {
        areYouSourePanel.transform.DOScale(Vector3.zero, 1);
    }
    public void newGame() //Yeni save a�ar
    {
        PlayerPrefs.DeleteKey("Save");
        PlayerPrefs.DeleteKey("Key");
        PlayerPrefs.SetInt("DashParchment", 0);
        PlayerPrefs.SetInt("SoarParchment", 0);

        blackImage.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2));
    }

    public void playGame() //Save de bulunan oyunu ba�lat�r
    {
        blackImage.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(PlayerPrefs.GetInt("Save")));
    }

    public void showSetNamePanel() //�sim ayarlama panelini a�ar
    {
        setNamePanel.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1);
    }

    public void setName() //�smi net belirler
    {
        if(nameInput.text!="")
        {
            PlayerPrefs.SetString("Name", nameInput.text);
           
        }
        else
        {
            PlayerPrefs.SetString("Name", "Ryota");
        }

        newGame();
    }

    public void openScene(string a)
    {
        blackImage.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(a));
    }

    public void changeCheatMode()
    {
        cheatMode = cheatToggle.isOn;
        if(cheatMode==true)
        {
            PlayerPrefs.SetInt("Cheat", 1);
            cheatPanelMode(true);
            Debug.Log("Hileler aktif");
        }
        else
        {
            PlayerPrefs.SetInt("Cheat", 0);
            Debug.Log("Hileler kapal�");
        }
    }

    public void cheatPanelMode(bool a)
    {
        if(a==true)
        {
            cheatPanel.transform.DOScale(0.5f, 1f);
        }
        else
        {
            cheatPanel.transform.DOScale(0, 1f);
        }
    }
    public void Quit()
    {

        Application.Quit();

    }

    public void TryAgain()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }


    public void openPanel()
    {
        
        panel.transform.DOScale(panelStartPos, 1f);
    }

    public void closePanel()
    {
        panel.transform.DOScale(Vector3.zero, 1f);
       
    }

}
