using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
public class InfoMng : MonoBehaviour
{
    public string[] allAchivementAccount;
    public GameObject[] allAchivementName;
    public TextMeshProUGUI accountText;
    public float writeDuration;
    public Image AchivementImage;

    public AchievementMng mng;
    public GameObject accountParent;
    private Vector3 startPos;
    

    Coroutine writeText;
    private void Start()
    {
        accountText.text = "";
        AchivementImage = null;

        startPos = accountParent.transform.position;

        accountParent.transform.position = new Vector3(accountParent.transform.position.x+1000, accountParent.transform.position.y, accountParent.transform.position.z);
        accountParent.transform.DOMoveX(startPos.x, 1);
    }
    public void Enter(string name,bool isLocked)
    {
        
        int value = 0;
        for (int i = 0; i < allAchivementName.Length; i++)
        {
            if(allAchivementName[i].name == name)
            {
                 value = i;
            }
        }
        if(isLocked==false)
        {
            writeText = StartCoroutine(writeAccount(allAchivementAccount[value]));
        }
        else
        {
            writeText = StartCoroutine(writeAccount("????????????????????????"));
        }
      

    }

    public void Exit()
    {
        StopCoroutine(writeText);
        accountText.text = "";
    }

    IEnumerator writeAccount(string account)
    {
        foreach (char a in account)
        {
            accountText.text += a.ToString();
            yield return new WaitForSeconds(writeDuration);
        }
    }
}
