using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class DeadMng : MonoBehaviour
{
    public Image Bg;
    public bool isParkur;
    public Image sliderImage;

    public Image[] allObject;
    public TextMeshProUGUI[] allText;
    public bool fillSlider = false;
    public float value;
    public TextMeshProUGUI percentText;



    [Header("Parkur")]
    public Transform startPos;
    public Transform endPos;
    public Transform player;
    [Header("Boss")]
    public float bossMaxHealt;
    public float bossCurrentHealt;
    [Header("Restart Scene")]
    public Image black;
    [Header("Only Final Boss First Scene")]
    public bool isFinalBossFirstScene = false;

    private void Start()
    {
        sliderImage.fillAmount = 0;
        for (int i = 0; i < allObject.Length; i++)
        {
            allObject[i].DOFade(0,0);
        }
        for (int i = 0; i < allText.Length; i++)
        {
            allText[i].DOFade(0, 0);
        }

    }
    public void death()
    {
        StartCoroutine(go());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //GameObject.Find("PauseMenuManager").GetComponent<PauseMenuManager>().canOpen = false;
    }

    IEnumerator go()
    {
        

        for (int i = 0; i < allObject.Length; i++)
        {
            allObject[i].DOFade(1, 1);
        }
        for (int i = 0; i < allText.Length; i++)
        {
            allText[i].DOFade(1, 1);
        }

        yield return new WaitForSeconds(1);

        if (isParkur == true)
        {
            float x = endPos.position.x - startPos.position.x;
            value =calculatePercent(x, player.position.x);
        }
        else
        {
            value =calculatePercent(bossMaxHealt, bossMaxHealt-bossCurrentHealt);
            if (isFinalBossFirstScene == true)
            {
                if(bossMaxHealt==bossCurrentHealt)
                {
                    if(PlayerPrefs.GetInt("FinalBossWave1")==0)
                    {
                        PlayerPrefs.SetInt("FinalBossWave1", 1);
                        GameObject.Find("AchievementNotification").GetComponent<AchievementNotification>().getAchivement("FinalBossWave1");
                    }
                   
                }
            }
        }
        fillSlider = true;
    }

    float calculatePercent(float fullValue,float currentValue)
    {
        Debug.Log(currentValue);
        Debug.Log(fullValue);
        float a = (currentValue / fullValue) * 100;
        Debug.Log(a);
        return a;
    }
    private void Update()
    {
        if(fillSlider==true)
        {
            sliderImage.fillAmount = Mathf.Lerp(sliderImage.fillAmount, value/100, 0.5f * Time.deltaTime);
            percentText.text = "%" + ((int)(sliderImage.fillAmount*100)).ToString();
        }
    }

    public void restartGame()
    {
        Debug.Log("Bastý");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        black.DOFade(1, 1).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
}
