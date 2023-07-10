using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class SaveSystemScene : MonoBehaviour
{
    public GameObject saveIcon;
    private void Start()
    {
        saveIcon.GetComponent<Image>().DOFade(0, 0);
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
        yield return new WaitForSeconds(5f);
        saveIcon.GetComponent<Image>().DOFade(0, 1);
    }
}
