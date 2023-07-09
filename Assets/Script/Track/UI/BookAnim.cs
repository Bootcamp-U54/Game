using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAnim : MonoBehaviour
{

   
    public GameObject Book;
    public Transform startTransform;
    public float animationDuration = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowPanel();
        }
    }
    public void ShowPanel()
    {
        Book.SetActive(true);
        Book.transform.DOMove(startTransform.position, animationDuration).SetEase(Ease.OutBack).SetUpdate(true);
    }


}
