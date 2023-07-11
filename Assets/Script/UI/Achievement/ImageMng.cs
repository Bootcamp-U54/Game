using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageMng : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InfoMng mng;
    public Image img;
    public bool isLocked;
    private void Start()
    {
        img = transform.GetChild(0).gameObject.GetComponent<Image>();

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mng.Enter(gameObject.name,isLocked);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mng.Exit();
    }
}
