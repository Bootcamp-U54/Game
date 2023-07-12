using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class DiscordMng : MonoBehaviour
{
    [Header("Value")]
    public bool isOpenDiscordBool;
    public string url;
    [Header("Object")]
    public TMP_InputField discordLink;
    public Toggle isOpenDiscordToggle;
    [Header("How")]
    public GameObject panel;
    public Vector3 panelStartPos;
    void Start()
    {
        panelStartPos = panel.transform.localScale;
        panel.transform.localScale = new Vector3(0, 0, 0);

        if(PlayerPrefs.HasKey("CanUseDiscord"))
        {
            if(PlayerPrefs.GetInt("CanUseDiscord")==1)
            {
                isOpenDiscordToggle.isOn = true;
                discordLink.interactable = true;
            }
            else
            {
                isOpenDiscordToggle.isOn = false;
                discordLink.interactable = false;
            }
        }
        else
        {
            isOpenDiscordToggle.isOn = false;
            discordLink.interactable = false;
        }
       
    }


    public void changeDiscordBool()
    {
        isOpenDiscordBool = isOpenDiscordToggle.isOn;
        discordLink.interactable = isOpenDiscordBool;
        if(isOpenDiscordBool==true)
        {
            PlayerPrefs.SetInt("CanUseDiscord", 1);
        }
        else
        {
            PlayerPrefs.SetInt("CanUseDiscord", 0);
        }
        Debug.Log("Discord aktifliði :" + isOpenDiscordBool);
    }

    public void save()
    {
        PlayerPrefs.SetString("webHook", discordLink.text);
    }
    public void testMsg()
    {
        if(PlayerPrefs.GetInt("CanUseDiscord")==1)
        {
            sendDiscordMsg();
        }
      
    }


    public void sendDiscordMsg()
    {
        string link = PlayerPrefs.GetString("webHook");
        url = link;
        string msg = "Down Of Fate deneme Mesajý";

        StartCoroutine(sendMsgIE(link, msg, (succes) =>
        {
            if (succes)
            {
                Debug.Log("Good");
            }
        }
        ));
    }

    IEnumerator sendMsgIE(string link, string message, System.Action<bool> action)
    {
        WWWForm form = new WWWForm();
        form.AddField("content", message);
        using (UnityWebRequest www = UnityWebRequest.Post(link, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                action(false);
            }
            else
            {
                action(true);
            }
        }
    }

    public void back()
    {
        SceneManager.LoadScene(0);
    }

    public void openPanel()
    {
        panel.transform.DOScale(panelStartPos, 1f);
    }

    public void closePanel()
    {
        panel.transform.DOScale(Vector3.zero, 1f);
    }
}
