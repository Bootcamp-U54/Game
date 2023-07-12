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



    void Start()
    {
        spriteRender.gameObject.transform.DORotate(new Vector3(0, 0, 90), 0);
        spriteRender.DOFade(0, 0);
      

        getParchment();
    }
    void Update()
    {

        if (isActive == true)
        {
            if (canOpen == true)
            {
                spriteRender.size = Vector2.Lerp(spriteRender.size, OpenSize, 2f*Time.deltaTime);

            }
            else
            {
                spriteRender.size = Vector2.Lerp(spriteRender.size, closeSize, 2f*Time.deltaTime);
            }
        }

    }


    public void getParchment()
    {
        StartCoroutine(go());
    }


    IEnumerator go()
    {

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
