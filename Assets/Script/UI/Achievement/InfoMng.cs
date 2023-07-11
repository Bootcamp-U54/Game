using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InfoMng : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string[] allAchivementAccount;
    public TextMeshProUGUI accountText;
    public float writeDuration;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PlayerPrefs.HasKey(eventData.pointerEnter.name)==true)
        {

        }
   
    }

    public void OnPointerExit(PointerEventData eventData)
    {
 
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
