using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Networking;

public class AchievementNotification : MonoBehaviour
{
    [Header("Canvas Object")]
    public Image img;
    public TextMeshProUGUI text;

    public GameObject bg;
    private Vector3 startPos;
    private Vector3 otherPos;

    [Header("Achivement")]
    public string[] allAchivementId;
    public Sprite[] allAchivementSprite;
    public string[] allAchivementAccount;
    public string[] allAchivementDiscordMsg;
    void Start()
    {
        startPos = bg.transform.position;
        bg.transform.position = new Vector3(bg.transform.position.x + 1000, bg.transform.position.y, bg.transform.position.z);
        otherPos = bg.transform.position;
        bg.SetActive(false);
    }

    public void getAchivement(string achivementId)
    {

        StartCoroutine(getAchivementIE(achivementId));
    }

    IEnumerator getAchivementIE(string achivementId)
    {
        bg.SetActive(true);

        int id = 0;
        for (int i = 0; i < allAchivementId.Length; i++)
        {
            if (achivementId == allAchivementId[i])
            {
                id = i;
            }
        }

        img.sprite = allAchivementSprite[id];
        text.text = allAchivementAccount[id];

        if (PlayerPrefs.GetInt("CanUseDiscord") == 1)
        {
            sendDiscordMsg(id);
        }
        bg.transform.DOMove(startPos, 1).SetUpdate(true);
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(5f);


        bg.transform.DOMove(otherPos, 1).SetUpdate(true);
        yield return new WaitForSeconds(1f);
        bg.SetActive(false);
    }

    public void sendDiscordMsg(int id)
    {
        string webHookMsg = PlayerPrefs.GetString("webHook");
        string msg = "**"+PlayerPrefs.GetString("Name")+"** "+ allAchivementDiscordMsg[id];

        StartCoroutine(sendMsgIE(webHookMsg, msg, (succes) =>
          {
              if (succes)
              {
                  Debug.Log("Good");
              }
          }
        ));
    }

    IEnumerator sendMsgIE(string link , string message,System.Action<bool> action)
    {
        WWWForm form = new WWWForm();
        form.AddField("content", message);
        using (UnityWebRequest www = UnityWebRequest.Post(link,form))
        {
            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(www.error);
                action(false);
            }
            else
            {
                action(true);
            }
        }
    }
}
