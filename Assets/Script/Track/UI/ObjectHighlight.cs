using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHighlight : MonoBehaviour
{

    private Image image;
    private Outline outline;
    private bool isBlinking = false;
    private float blinkInterval = 0.5f; // Parlama aral��� (saniye)

    private void Start()
    {
        image = GetComponent<Image>();

        // Outline bile�enini ekleyin
        outline = gameObject.AddComponent<Outline>();

        // Efekt rengini ayarlay�n
        outline.effectColor = Color.red;

        // Efekt mesafesini ayarlay�n (�izgi geni�li�i)
        outline.effectDistance = new Vector2(4f, 4f);

        // G�rsel alfa kullan�m�n� etkinle�tirin
        outline.useGraphicAlpha = true;

        // Yan�p s�nme i�lemini ba�lat
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
