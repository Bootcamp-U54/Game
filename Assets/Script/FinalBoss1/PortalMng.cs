using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalMng : MonoBehaviour
{
    public bool canTp = false;
    public Image blackImage;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player" && canTp==true)
        {
            StartCoroutine(startTp(collision.gameObject));
        }
    }

    IEnumerator startTp(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        player.GetComponent<SpriteRenderer>().DOFade(0, 1f);
        yield return new WaitForSeconds(1);

        anim.SetTrigger("Close");

    }

    public void loadScene()
    {
        anim.enabled = false;
        GetComponent<SpriteRenderer>().sprite = null;
        blackImage.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(1));
    }
}
