using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartSceneMng : MonoBehaviour
{
    public GameObject black;
    public TextMeshProUGUI text;

    private bool canSkip = false;
    void Start()
    {
        text.DOFade(0f, 0f);
        StartCoroutine(go());
    }

    private void Update()
    {
        if (Input.anyKeyDown &&canSkip)
        {
            black.GetComponent<Image>().DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(1));
        }
    }

    IEnumerator go()
    {
        black.GetComponent<Image>().DOFade(0, 1f).From(1);
        yield return new WaitForSeconds(2f);
        text.DOFade(1f,1f);
        canSkip = true;
    }
    
}
