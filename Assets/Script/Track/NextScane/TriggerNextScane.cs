using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerNextScane : MonoBehaviour
{
   
    public Image blackImage; // Siyah Image referans�
    public GameObject otherPanel; // Di�er panelin referans�
    public string levelAchievement;

    public bool isCheat=false;
    public bool cheatMode;
    private void Start()
    {
        blackImage.color = Color.black;
        blackImage.DOFade(0f, 0f);
        if (PlayerPrefs.GetInt("Cheat")==1)
        {
            cheatMode = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)&&cheatMode==true)
        {
            isCheat = true;
            NextScane();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FadeInAndActivatePanel();
        }
    }
   
    public void FadeInAndActivatePanel()
    {
        blackImage.DOFade(1f, .5f).OnComplete(NextScane);
    }
    public  void NextScane()
    {
        if(isCheat==false)
        {
            if (levelAchievement != "")
            {
                if (PlayerPrefs.GetInt(levelAchievement) == 0)
                {
                    PlayerPrefs.SetInt(levelAchievement, 1);
                    GameObject.Find("AchievementNotification").GetComponent<AchievementNotification>().getAchivement(levelAchievement);
                }

            }
        }
      
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Mevcut sahnenin index numaras�n� al
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings; // Bir sonraki sahnenin index numaras�n� hesapla
        SceneManager.LoadScene(nextSceneIndex);
    }

}
