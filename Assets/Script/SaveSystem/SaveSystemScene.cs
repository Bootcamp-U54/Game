using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystemScene : MonoBehaviour
{
    public void checkScene()
    {
        if (PlayerPrefs.GetInt("Save") < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("Save", SceneManager.GetActiveScene().buildIndex);
            Debug.LogWarning("Yeni level kaydedildi");
        }
        else
        {
            Debug.LogWarning("Kayýtlý level açýldý");
        }
    }
}
