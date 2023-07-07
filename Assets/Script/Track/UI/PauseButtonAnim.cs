using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonAnim : MonoBehaviour
{
    public Button resumetButton;
    public Button optionsButton;
    public Button mainMenuButton;

    public Transform resumeTransform;
    public Transform mainMenuTransform;

    public float animationDuration = 1f;
    void Start()
    {
        optionsButton.transform.DOScale(0f, 0f);
        ShowPanel();
     

    }

    public void ShowPanel()
    {


        //resume
        resumetButton.gameObject.SetActive(true);
        resumetButton.transform.DOMove(resumeTransform.position, animationDuration).SetEase(Ease.OutBack);

        //options
        optionsButton.transform.DOScale(1f, animationDuration);


        //mainMenu
        mainMenuButton.gameObject.SetActive(true);
        mainMenuButton.transform.DOMove(mainMenuTransform.position, animationDuration).SetEase(Ease.OutBack);


    }
}
