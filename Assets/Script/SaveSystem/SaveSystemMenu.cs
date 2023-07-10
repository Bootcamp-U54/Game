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
    void Start()
    {
        if(test==true)
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

        blackImage.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
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

  
}
