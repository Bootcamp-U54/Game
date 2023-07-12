using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditMng : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Sprite[] allSprite;
    public Image[] allImage;

    public float textDuration;
    public Vector3 textPosition;
    void Start()
    {
        text.transform.DOLocalMove(textPosition, textDuration).SetEase(Ease.Linear).OnComplete(()=>SceneManager.LoadScene(0));
        for (int a = 0; a < allImage.Length; a++)
        {
            allImage[a].transform.DOScale(0, 0f);
        }
       // StartCoroutine(go());
    }

    IEnumerator go()
    {
        float waitDuration = textDuration / allSprite.Length;

        for (int i = 0; i < allSprite.Length; i++)
        {
            Image selectImage = allImage[Random.Range(0, allImage.Length)];
            
            selectImage.sprite = allSprite[i];
            selectImage.SetNativeSize();
            selectImage.transform.DOScale(1, 1);
            yield return new WaitForSeconds(waitDuration-1);

            for (int a = 0; a < allImage.Length; a++)
            {
                allImage[a].transform.DOScale(0, 1);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
