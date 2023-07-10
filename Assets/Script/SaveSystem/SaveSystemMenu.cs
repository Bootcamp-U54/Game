using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class SaveSystemMenu : MonoBehaviour
{
    public Button PlayGameButton;

    public Image blackImage;
    public GameObject areYouSourePanel;
    void Start()
    {
        if(PlayerPrefs.HasKey("Save")==true)
        {
            PlayGameButton.interactable = true;
        }
        else
        {
            PlayGameButton.interactable = false;
        }
        areYouSourePanel.transform.DOScale(Vector3.zero, 0);

    }
    public void newGameAreYouSureOpen()
    {
        areYouSourePanel.transform.DOScale(new Vector3(1,1,1), 1);
    }
    public void newGameAreYouSureClose()
    {
        areYouSourePanel.transform.DOScale(Vector3.zero, 1);
    }
    public void newGame()
    {
        PlayerPrefs.DeleteKey("Save");
        PlayerPrefs.SetInt("Save",(SceneManager.GetActiveScene().buildIndex + 1));
        blackImage.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void playGame()
    {
        blackImage.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(PlayerPrefs.GetInt("Save")));
    }
}
