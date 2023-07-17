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
    public Image[] trackPage;
    public GameObject bookCanvas;
    public float fadeDuration = 2f;
    private int currentIndexStory;

    private int catBorder = 1;
    private int skeletorBorder = 7;

    private int currentIndexTrack;
    private bool isTyping;
    public float typingSpeed;
    private bool isPaused = false;

    int ScaneIndex;
    public bool bookIsOpen = false;
    public PauseMenuManager pauseMenuMng;

    [Header("BookAnim")]
    public GameObject Book;
    public Transform startTransform;
    public Transform backTransform;
    public float animationDuration = 1f;


    private void Start()
    {
  

        ScaneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        #region CurrentIndexGeneator
        if (ScaneIndex == 4 || ScaneIndex == 5)
        {
            currentIndexStory = 0;

        }

        else if (ScaneIndex == 6 || ScaneIndex == 7)
        {
            currentIndexStory = 2;
            for (int i = 0; i < currentIndexStory; i++)
            {
                storyPage[i].DOFade(0f, 0f).SetUpdate(true);
            }
          



        }
        else if (ScaneIndex >= 8)
        {
            currentIndexStory = 7;

            for (int i = 0; i < currentIndexStory; i++)
            {
                storyPage[i].DOFade(0f, 0f).SetUpdate(true);
            }
         
        }

        #endregion

        for (int i = 1; i < storyPage.Length; i++)
        {
            storyPage[i].DOFade(0f, 0f).SetUpdate(true);

        }
        for (int i = 1; i < trackPage.Length; i++)
        {
            trackPage[i].DOFade(0f, 0f).SetUpdate(true);

        }


    }


    private void Update()
    {
        Debug.Log(currentIndexStory);
        PlayerController targetScript = targetObject.GetComponent<PlayerController>();
        if (Input.GetKeyDown(KeyCode.B) && targetScript.deathSfxIsPlay == false && pauseMenuMng.pauseMenuIsOpen == false)
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
                StartTypewriterEffect(storyPage[currentIndexStory].text);
                Time.timeScale = 0f;
                isPaused = true;
                bookIsOpen = true;

            }
        }
    }

    public void OnClickNextPage(int index)
    {

        #region StoryGenerator 
        if (ScaneIndex == 4 || ScaneIndex == 5)
        {
            if (!isTyping && currentIndexStory >= 0 && currentIndexStory < catBorder && index == 0)
            {

                storyPage[currentIndexStory].DOFade(0f, fadeDuration).SetUpdate(true);
                storyPage[currentIndexStory + 1].DOFade(1f, fadeDuration).SetUpdate(true);
                currentIndexStory++;
                StartTypewriterEffect(storyPage[currentIndexStory].text);
             


            }
        }

        else if (ScaneIndex == 6 || ScaneIndex == 7)
        {
            if (!isTyping && currentIndexStory >= 0 && currentIndexStory < skeletorBorder && index == 0)
            {

                storyPage[currentIndexStory].DOFade(0f, fadeDuration).SetUpdate(true);
                storyPage[currentIndexStory + 1].DOFade(1f, fadeDuration).SetUpdate(true);
                currentIndexStory++;
                StartTypewriterEffect(storyPage[currentIndexStory].text);

            }
        }

        else if (ScaneIndex >= 8)
        {
            if (!isTyping && currentIndexStory >= 0 && currentIndexStory+1 < storyPage.Length && index == 0)
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
        bookIsOpen = false;
        Book.SetActive(false);
        Book.transform.DOMove(backTransform.position, animationDuration).SetEase(Ease.OutBack).SetUpdate(true);
    }

}

