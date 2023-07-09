using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
public class BookManager : MonoBehaviour
{
    public TextMeshProUGUI[] page;
    public GameObject bookCanvas;
    public float fadeDuration = 2f;

    private int currentIndex = 0;
    private bool isTyping = false;
    private float typingSpeed;
    private bool isPaused = false;

    private void Start()
    {
        for (int i = 1; i < page.Length; i++)
        {
            page[i].DOFade(0f, 0f).SetUpdate(true);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                bookCanvas.SetActive(true);
                StartTypewriterEffect(page[currentIndex].text);
                Time.timeScale = 0f;
                isPaused = true;

            }
        }
    }

    public void OnClickStoryText()
    {
        StartTypewriterEffect(page[currentIndex].text);
    }
    public void OnClickNextPage()
    {
        if (!isTyping && currentIndex >= 0 && currentIndex + 1 < page.Length)
        {
            page[currentIndex].DOFade(0f, fadeDuration).SetUpdate(true);
            page[currentIndex + 1].DOFade(1f, fadeDuration).SetUpdate(true);
            currentIndex++;
            StartTypewriterEffect(page[currentIndex].text);
        }
    }

    public void OnClickBackpage()
    {
        if (!isTyping && currentIndex > 0)
        {
            page[currentIndex].DOFade(0f, fadeDuration).SetUpdate(true);
            page[currentIndex - 1].DOFade(1f, fadeDuration).SetUpdate(true);
            currentIndex--;
            StartTypewriterEffect(page[currentIndex].text);
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
        TextMeshProUGUI currentPage = page[currentIndex];
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
        bookCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;
    }
}
