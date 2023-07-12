using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Image = UnityEngine.UI.Image;

public class BookManager : MonoBehaviour
{
    public GameObject targetObject;
    TextMeshProUGUI currentPage;
    public TextMeshProUGUI[] storyPage;
    public Image[] trackPage, controlerPage;
    public GameObject bookCanvas;
    public float fadeDuration = 2f;
    private int currentIndexStory;
    
    private int catBorder = 1;
    private int skeletorBorder = 7;

    private int currentIndexTrack, currentIndexController = 0;
    private bool isTyping = false;
    public float typingSpeed;
    private bool isPaused = false;

    int ScaneIndex;

    [Header("BookAnim")]
    public GameObject Book;
    public Transform startTransform;
    public Transform backTransform;
    public float animationDuration = 1f;


    private void Start()
    {
        ScaneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        #region CurrentIndexGeneator
        if (ScaneIndex == 3 || ScaneIndex == 4)
        {
            currentIndexStory = 0;

        }

        else if (ScaneIndex == 5 || ScaneIndex == 6)
        {
            currentIndexStory = 2;
            for (int i = 0; i < currentIndexStory; i++)
            {
                storyPage[i].DOFade(0f, 0f).SetUpdate(true);
            }



        }
        else if (ScaneIndex >= 7)
        {
            currentIndexStory = 8;

            for (int i = 0; i < currentIndexStory; i++)
            {
                storyPage[i].DOFade(0f, 0f).SetUpdate(true);
            }
        }

        else if (ScaneIndex >= 7)
            currentIndexStory = 7;

        #endregion

        for (int i = 1; i < storyPage.Length; i++)
        {
            storyPage[i].DOFade(0f, 0f).SetUpdate(true);

        }
        for (int i = 1; i < trackPage.Length; i++)
        {
            trackPage[i].DOFade(0f, 0f).SetUpdate(true);

        }
        for (int i = 1; i < controlerPage.Length; i++)
        {
            controlerPage[i].DOFade(0f, 0f).SetUpdate(true);

        }

    }


    private void Update()
    {
        Debug.Log(currentIndexStory);
        PlayerController targetScript = targetObject.GetComponent<PlayerController>();
        if (Input.GetKeyDown(KeyCode.B)&&targetScript.deathSfxIsPlay==false)
        {
            if (isPaused)
            {
                BackShowPanel();
                ResumeGame();
            }
            else
            {

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ShowPanel();
                storyPage[currentIndexStory].DOFade(1f, 0f).SetUpdate(true);
                StartCoroutine(TypeText(storyPage[currentIndexStory].text));
                Time.timeScale = 0f;
                isPaused = true;

            }
        }
    }

    public void OnClickNextPage(int index)
    {

        #region StoryGenerator 
        if (ScaneIndex == 3 || ScaneIndex == 4)
        {
            if (!isTyping && currentIndexStory >= 0 && currentIndexStory < catBorder && index == 0)
            {

                storyPage[currentIndexStory].DOFade(0f, fadeDuration).SetUpdate(true);
                storyPage[currentIndexStory + 1].DOFade(1f, fadeDuration).SetUpdate(true);
                currentIndexStory++;
                StartTypewriterEffect(storyPage[currentIndexStory].text);

            }
        }

        else if (ScaneIndex == 5 || ScaneIndex == 6)
        {
            if (!isTyping && currentIndexStory >= 0 && currentIndexStory < skeletorBorder && index == 0)
            {

                storyPage[currentIndexStory].DOFade(0f, fadeDuration).SetUpdate(true);
                storyPage[currentIndexStory + 1].DOFade(1f, fadeDuration).SetUpdate(true);
                currentIndexStory++;
                StartTypewriterEffect(storyPage[currentIndexStory].text);

            }
        }

        else if (ScaneIndex >= 7)
        {
            if (!isTyping && currentIndexStory >= 0 && currentIndexStory + 1 < storyPage.Length && index == 0)
            {

                storyPage[currentIndexStory].DOFade(0f, fadeDuration).SetUpdate(true);
                storyPage[currentIndexStory + 1].DOFade(1f, fadeDuration).SetUpdate(true);
                currentIndexStory++;
                StartTypewriterEffect(storyPage[currentIndexStory].text);

            }
        }

        #endregion

        if (!isTyping && currentIndexTrack >= 0 && currentIndexTrack + 1 < trackPage.Length && index == 1)
        {
            trackPage[currentIndexTrack].DOFade(0f, fadeDuration).SetUpdate(true);
            trackPage[currentIndexTrack + 1].DOFade(1f, fadeDuration).SetUpdate(true);
            currentIndexTrack++;

        }

        if (!isTyping && currentIndexTrack >= 0 && currentIndexTrack + 1 < controlerPage.Length && index == 2)
        {
            controlerPage[currentIndexTrack].DOFade(0f, fadeDuration).SetUpdate(true);
            controlerPage[currentIndexTrack + 1].DOFade(1f, fadeDuration).SetUpdate(true);
            currentIndexController++;

        }



    }
    public void OnClickBackpage(int index)
    {
        if (!isTyping && currentIndexStory > 0 && index == 0)
        {
            storyPage[currentIndexStory].DOFade(0f, fadeDuration).SetUpdate(true);
            storyPage[currentIndexStory - 1].DOFade(1f, fadeDuration).SetUpdate(true);
            currentIndexStory--;
            StartTypewriterEffect(storyPage[currentIndexStory].text);
        }
        if (!isTyping && currentIndexTrack > 0 && index == 1)
        {
            trackPage[currentIndexTrack].DOFade(0f, fadeDuration).SetUpdate(true);
            trackPage[currentIndexTrack - 1].DOFade(1f, fadeDuration).SetUpdate(true);
            currentIndexTrack--;
        }
        if (!isTyping && currentIndexController > 0 && index == 2)
        {
            controlerPage[currentIndexController].DOFade(0f, fadeDuration).SetUpdate(true);
            controlerPage[currentIndexController - 1].DOFade(1f, fadeDuration).SetUpdate(true);
            currentIndexController--;
        }


    }





    private void StartTypewriterEffect(string text)
    {
        if (!isTyping)
        {
            isTyping = true;
            StartCoroutine(TypeText(text));

        }
    }

    private IEnumerator TypeText(string text)
    {

        currentPage = storyPage[currentIndexStory];
        currentPage.text = "";

        foreach (char character in text)
        {
            currentPage.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void ResumeGame()
    {

        BackShowPanel();
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;

    }



    public void ShowPanel()
    {
        Book.SetActive(true);
        Book.transform.DOMove(startTransform.position, animationDuration).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void BackShowPanel()
    {
        Book.SetActive(false);
        Book.transform.DOMove(backTransform.position, animationDuration).SetEase(Ease.OutBack).SetUpdate(true);
    }

}

