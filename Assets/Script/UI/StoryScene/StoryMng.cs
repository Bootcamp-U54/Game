using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class StoryMng : MonoBehaviour
{
    public GameObject[] page;

    public TextMeshProUGUI text;
    public Image draw;

    public float writeDuration;
    public float fillAmountValue;
    public bool fillImage = false;

    private void Start()
    {
        resetAll();
        StartCoroutine(pageMng());
    }

    public void resetAll()
    {
        for (int i = 0; i < page.Length; i++)
        {
            page[i].SetActive(false);
        }
    }
    IEnumerator pageMng()
    {
        for (int i = 0; i < page.Length; i++)
        {
            
            text = page[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            draw = page[i].transform.GetChild(1).gameObject.GetComponent<Image>();

            string write = text.text;
            text.text = "";

            draw.fillAmount = 0;
            fillImage = true;
            page[i].SetActive(true);
            foreach (char a in write)
            {
                text.text += a.ToString();
                yield return new WaitForSeconds(writeDuration);
            }

            yield return new WaitUntil(() => Input.anyKeyDown);

            text.DOFade(0, 1f);
            draw.DOFade(0, 1f);
            yield return new WaitForSeconds(1);

            resetAll();
        }
       
        
    }
    
    private void Update()
    {
        if(fillImage==true && draw.fillAmount<1)
        {
            draw.fillAmount += fillAmountValue * Time.deltaTime;
        }
    }
}
