using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

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

        bg.transform.DOMove(startPos, 1).SetUpdate(true);
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(5f);


        bg.transform.DOMove(otherPos, 1).SetUpdate(true);
        yield return new WaitForSeconds(1f);
        bg.SetActive(false);
    }
}
