using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonAnim : MonoBehaviour
{

    public Button startButton;
    public Button quitButton;

    public Transform startTransform;
    public Transform quitTransform;


    public float animationDuration = 1f;
    private Vector3 originalPosition;


    void Start()
    {
        ShowPanel();

    }

    public void ShowPanel()
    {

        //start
        startButton.gameObject.SetActive(true);
        startButton.transform.DOMove(startTransform.position, animationDuration).SetEase(Ease.OutBack);
        //quit
        quitButton.gameObject.SetActive(true);
        quitButton.transform.DOMove(quitTransform.position, animationDuration).SetEase(Ease.OutBack);

    }
}
