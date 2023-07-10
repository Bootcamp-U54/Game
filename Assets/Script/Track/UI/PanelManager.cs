using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PanelManager: MonoBehaviour
{
    public Image blackImage; // Siyah Image referansý
    public GameObject otherPanel; // Diðer panelin referansý

    private void Start()
    {
        blackImage.color = Color.black;
        blackImage.DOFade(0f, 0f);
    }

 
    public void FadeInAndActivatePanel()
    {
       
        blackImage.DOFade(1f, .5f).OnComplete(GoNextScane);
    }

    private void GoNextScane()
    {
        SceneManager.LoadSceneAsync("Parkur_1");
    }
}