using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class PanelAnimation : MonoBehaviour
{
    public GameObject panel;
    public Image image;
    public TextMeshProUGUI text;
    public Transform targetTransform;
    public float animationDuration = 1f;

    private CanvasGroup imageCanvasGroup;
    private Vector3 originalPosition;

    void Start()
    {
        // Paneli ve içerikleri baþlangýçta gizle
        panel.SetActive(false);
        image.gameObject.SetActive(false);
        text.gameObject.SetActive(false);

        // Resmin orijinal pozisyonunu kaydet
        originalPosition = image.transform.position;
        ShowPanel();

    }

    public void ShowPanel()
    {
        // Paneli yavaþça görünür hale getir
        panel.SetActive(true);

        // Resmi yavaþça hareket ettir
        image.gameObject.SetActive(true);
        image.transform.position = originalPosition;
        image.transform.DOMove(targetTransform.position, animationDuration).SetEase(Ease.OutBack);

        // Yazýlarý yavaþça görünür hale getir
        text.gameObject.SetActive(true);
        text.DOFade(1f, animationDuration);
    }
}
