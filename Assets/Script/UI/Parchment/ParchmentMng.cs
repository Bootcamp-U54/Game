using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ParchmentMng : MonoBehaviour
{
    public bool canOpen = false;
    public bool isActive;
    public SpriteRenderer spriteRender;
    public Vector2 closeSize, OpenSize;



    void Start()
    {
        spriteRender.gameObject.transform.DORotate(new Vector3(0, 0, 90), 0);
        spriteRender.DOFade(0, 0);
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

        if(Input.GetKeyDown(KeyCode.P) &&isActive==false)
        {
            getParchment();
        }

    }

    public void getParchment()
    {
        StartCoroutine(go());
    }


    IEnumerator go()
    {

        isActive = true;
    
        spriteRender.DOFade(1, 1).From(0);
        yield return new WaitForSeconds(1f);

        spriteRender.gameObject.transform.DORotate(new Vector3(0, 0, 0), 1);
        yield return new WaitForSeconds(1f);

        canOpen = true;
        yield return new WaitForSeconds(2f);
        
        yield return new WaitUntil(() => Input.anyKeyDown);


        canOpen = false;
        yield return new WaitForSeconds(2f);

        spriteRender.gameObject.transform.DORotate(new Vector3(0, 0, 90), 1);
        yield return new WaitForSeconds(1f);

        spriteRender.DOFade(0, 1).From(1);
        yield return new WaitForSeconds(1f);
        isActive = false;
    }

    // Update is called once per frame
   
}
