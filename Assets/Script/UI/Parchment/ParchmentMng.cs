using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class ParchmentMng : MonoBehaviour
{
    public bool canOpen = false;
    public bool isActive;
    public SpriteRenderer spriteRender;
    public Vector2 closeSize, OpenSize;

    public TextMeshProUGUI text;
    public string account;
    private bool isWrite = false;

    public int sceneIndex;
    public int currentIndex;

    public string playerPrefsText;



    void Start()
    {
        sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        spriteRender.gameObject.transform.DORotate(new Vector3(0, 0, 90), 0);
        spriteRender.DOFade(0, 0);

        if(PlayerPrefs.HasKey(playerPrefsText))
        {
            if(PlayerPrefs.GetInt(playerPrefsText) ==0)
            {
                PlayerPrefs.SetInt(playerPrefsText, 1);
                getParchment();
            }
        }
     
    }
    void Update()
    {

        if (isActive == true)
        {
            if (canOpen == true)
            {
                spriteRender.size = Vector2.Lerp(spriteRender.size, OpenSize, 2f * Time.deltaTime);

            }
            else
            {
                spriteRender.size = Vector2.Lerp(spriteRender.size, closeSize, 2f * Time.deltaTime);
            }
        }

    }


    public void getParchment()
    {
       

        if (sceneIndex == 7 && PlayerPrefs.GetInt("Key") != 1)
        {
            StartCoroutine(go());
            currentIndex++;
            PlayerPrefs.SetInt("Key", currentIndex);
        }
        else if (sceneIndex == 9 && PlayerPrefs.GetInt("Key") == 1)
        {
            StartCoroutine(go());
            currentIndex = PlayerPrefs.GetInt("Key");
            currentIndex++;
            PlayerPrefs.SetInt("Key", currentIndex);
        }


    }


    IEnumerator go()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().canMove = false;
        isActive = true;

        spriteRender.DOFade(1, 1).From(0).SetUpdate(true);
        yield return new WaitForSeconds(1f);

        spriteRender.gameObject.transform.DORotate(new Vector3(0, 0, 0), 1).SetUpdate(true);
        yield return new WaitForSeconds(1f);

        canOpen = true;

        yield return new WaitForSeconds(2f);

        StartCoroutine(writeText());
        yield return new WaitUntil(() => isWrite = true);
        yield return new WaitUntil(() => Input.anyKeyDown);

        text.DOFade(0, 1).SetUpdate(true);
        canOpen = false;
        yield return new WaitForSeconds(2f);

        spriteRender.gameObject.transform.DORotate(new Vector3(0, 0, 90), 1).SetUpdate(true);
        yield return new WaitForSeconds(1f);

        spriteRender.DOFade(0, 1).From(1).SetUpdate(true);
        yield return new WaitForSeconds(1f);
        isActive = false;
        GameObject.Find("Player").GetComponent<PlayerController>().canMove = true;






    }


    IEnumerator writeText()
    {
        foreach (char a in account)
        {
            text.text += a.ToString();
            yield return new WaitForSeconds(0.02f);
        }
        isWrite = true;
    }

    // Update is called once per frame

}
