using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class SaveSystemScene : MonoBehaviour
{
    public GameObject saveIcon;
    public Camera renderCam;
    public Image parchment;
    private void Start()
    {
        saveIcon.GetComponent<Image>().DOFade(0, 0);
        parchment.DOFade(0, 0);
        checkScene();

    }
    public void checkScene()
    {
        if (PlayerPrefs.GetInt("Save") < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("Save", SceneManager.GetActiveScene().buildIndex);
            Debug.LogWarning("Yeni level kaydedildi");
         
            StartCoroutine(saveIconOpen());
        }
        else
        {
            Debug.LogWarning("Kayýtlý level açýldý");
        }

        Debug.LogWarning(PlayerPrefs.GetString("Name"));
    }

    IEnumerator saveIconOpen()
    {
        saveIcon.GetComponent<Image>().DOFade(1, 1);
        parchment.DOFade(1, 1);
        saveIcon.GetComponent<Image>().sprite = screenshot();

        yield return new WaitForSeconds(5f);
        saveIcon.GetComponent<Image>().DOFade(0, 1);
        parchment.DOFade(0, 1);
    }

    public Sprite screenshot()
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

        renderCam.transform.position = Camera.main.transform.position;
        Camera mainCamera = renderCam; // Kameranýn referansýný alýn
        mainCamera.targetTexture = renderTexture; // RenderTexture'ý kamera hedefi olarak ayarlayýn

        mainCamera.Render(); // Kamerayý render et
        RenderTexture.active = renderTexture; // RenderTexture'ý etkinleþtir
        Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false); // Boþ bir Texture2D oluþtur
        screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0); // RenderTexture'dan pikselleri oku
        screenshot.Apply(); // Texture2D'yi güncelle

        Sprite screenshotSprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f));

        return screenshotSprite;
    }
}
