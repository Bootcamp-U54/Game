using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AchievementMng : MonoBehaviour
{
    public bool gameTest;
    public bool isAllOpen;

    public string[] allAchievementName;
    public Image[] allAchievementImage;

    public Sprite[] gradeSprite;
    public int[] allAchievementGrade;

    public List<Vector3> startScale;

    public InfoMng infoMng;
    public Sprite closeSprite;
    private Coroutine openAchivementCor;

    private void Awake()
    {
        if(gameTest)
        {
            if(isAllOpen)
            {
                for (int i = 0; i < allAchievementName.Length; i++)
                {

                    PlayerPrefs.SetInt(allAchievementName[i], 1);
              
                }
            }
            else
            {
                for (int i = 0; i < allAchievementName.Length; i++)
                {

                    PlayerPrefs.SetInt(allAchievementName[i], 0);

                }

                PlayerPrefs.SetInt("TrainCount", 0);
                PlayerPrefs.SetInt("GhostCount", 0);
            }
        }

        for (int i = 0; i < allAchievementName.Length; i++)
        {
            if(PlayerPrefs.HasKey(allAchievementName[i])==false)
            {
                PlayerPrefs.SetInt(allAchievementName[i], 0);
            }
        }

        for (int i = 0; i < allAchievementImage.Length; i++)
        {
            allAchievementImage[i].gameObject.name = allAchievementName[i];
            allAchievementImage[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = gradeSprite[allAchievementGrade[i]-1];
        }
    }
    void Start()
    {
        for (int i = 0; i < allAchievementImage.Length; i++)
        {
            allAchievementImage[i].gameObject.transform.DOScale(0, 0);
        }


        for (int i = 0; i < allAchievementName.Length; i++)
        {
            if(PlayerPrefs.GetInt(allAchievementName[i])==0)
            {
                allAchievementImage[i].transform.GetChild(0).gameObject.SetActive(false);
                allAchievementImage[i].transform.GetChild(1).gameObject.SetActive(false);
                allAchievementImage[i].sprite = closeSprite;
                allAchievementImage[i].gameObject.GetComponent<ImageMng>().isLocked = true;
                allAchievementImage[i].gameObject.transform.localScale = new Vector3(1f, 0.5f, 1f);
             
            }
            
            startScale.Add(allAchievementImage[i].transform.localScale);
        }

       openAchivementCor= StartCoroutine(openAchievementImage(true));
    }

    IEnumerator openAchievementImage(bool a)
    {
        if(a)
        {
            for (int i = 0; i < allAchievementImage.Length; i++)
            {
                allAchievementImage[i].gameObject.transform.DOScale(startScale[i], 1);
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            infoMng.changeObjectPos(false);
            for (int i = 0; i < allAchievementImage.Length; i++)
            {
                allAchievementImage[i].gameObject.transform.DOScale(0, 0.5f);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.4f);
            SceneManager.LoadScene(0);
        }
        
    }

    public void goMenu()
    {
        StopCoroutine(openAchivementCor);
        StartCoroutine(openAchievementImage(false));
    }


}
