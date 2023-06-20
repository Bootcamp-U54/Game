using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DashPool : MonoBehaviour
{
    public List<GameObject> dashImage;
    public GameObject dashPrefab;

    public Transform player;

    public bool isDashImageSpawn = false;

    public float duration;
    public float dashImageCount;

    public float betweenImage;

    private void Update()
    {
        
    }
    public void getDashImage()
    {

        StartCoroutine(getDashImageIE());
    }

    
    
    IEnumerator getDashImageIE()
    {
        if (isDashImageSpawn == false)
        {
            for (int i = 0; i < dashImageCount; i++)
            {
                if(player.gameObject.GetComponent<PlayerController>().isdashing)
                {
                    GameObject a = Instantiate(dashPrefab, player.position, Quaternion.identity);
                    a.GetComponent<SpriteRenderer>().sprite = player.gameObject.GetComponent<SpriteRenderer>().sprite;
                    dashImage.Add(a);
                    fade(a);
                    a.transform.SetParent(this.gameObject.transform);

                    yield return new WaitUntil(() => Vector2.Distance(a.transform.position, player.position) > betweenImage);
                }             
            }

            isDashImageSpawn = true;
        }
        else
        {
            for (int i = 0; i < dashImage.Count; i++)
            {
                if (player.gameObject.GetComponent<PlayerController>().isdashing)
                {
                    dashImage[i].transform.position = player.position;
                    dashImage[i].GetComponent<SpriteRenderer>().sprite = player.gameObject.GetComponent<SpriteRenderer>().sprite;
                    fade(dashImage[i]);
                    Debug.Log(i);

                }
                yield return new WaitUntil(() => Vector2.Distance(dashImage[i].transform.position, player.position) > betweenImage);
            }
        }
    }

    public void fade(GameObject a)
    {
        a.GetComponent<SpriteRenderer>().DOFade(0, duration).From(1);

        if(player.gameObject.GetComponent<PlayerController>().faceRight==true)
        {
          
            Vector3 scaler = a.transform.localScale;
            scaler.x =1;
            a.transform.localScale = scaler;
        }
        else
        {
            Vector3 scaler = a.transform.localScale;
            scaler.x = -1;
            a.transform.localScale = scaler;
        }
    }
}
