using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHighlight : MonoBehaviour
{

    private Image image;
    private Outline outline;
    private bool isBlinking = false;
    private float blinkInterval = 0.5f; // Parlama aralýðý (saniye)

    private void Start()
    {
        image = GetComponent<Image>();

        // Outline bileþenini ekleyin
        outline = gameObject.AddComponent<Outline>();

        // Efekt rengini ayarlayýn
        outline.effectColor = Color.red;

        // Efekt mesafesini ayarlayýn (çizgi geniþliði)
        outline.effectDistance = new Vector2(4f, 4f);

        // Görsel alfa kullanýmýný etkinleþtirin
        outline.useGraphicAlpha = true;

        // Yanýp sönme iþlemini baþlat
        StartBlinking();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StopBlinking();
        }
    }
    private void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            InvokeRepeating("ToggleBlink", 0f, blinkInterval);
        }
    }

    private void ToggleBlink()
    {
        outline.enabled = !outline.enabled;
    }
    private void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            CancelInvoke("ToggleBlink");
            outline.enabled = false;
        }
    }
}
