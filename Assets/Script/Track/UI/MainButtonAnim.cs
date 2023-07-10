using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonAnim : MonoBehaviour
{

    public GameObject[] buttons;
    public List<Vector3> buttonsStartPos;


    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonsStartPos.Add(buttons[i].transform.position);
            buttons[i].transform.DOMoveY(buttons[i].transform.position.y + 1000,0);
        }

        StartCoroutine(ShowPanel());

    }

    IEnumerator ShowPanel()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.DOMove(buttonsStartPos[i], 1f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
