using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveSystemMenu : MonoBehaviour
{
    public bool test;

    public Button PlayGameButton;

    public Image blackImage;
    public GameObject areYouSourePanel;
    public GameObject setNamePanel;

    public TMP_InputField nameInput;

    public Toggle cheatToggle;
    public GameObject cheatPanel;
    public bool cheatMode;
    
    void Start()
    {
       
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
    public void newGame() //Yeni save açar
    {
        PlayerPrefs.DeleteKey("Save");

        blackImage.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2));
    }

    public void playGame() //Save de bulunan oyunu baþlatýr
    {
        blackImage.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(PlayerPrefs.GetInt("Save")));
    }

    public void showSetNamePanel() //Ýsim ayarlama panelini açar
    {
        setNamePanel.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1);
    }

    public void setName() //Ýsmi net belirler
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
            Debug.Log("Hileler kapalý");
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

  
}
