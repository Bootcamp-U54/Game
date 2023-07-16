using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

using Random = UnityEngine.Random;
public class ObjectsManager : MonoBehaviour
{
    public float maxTime;
    public float currentTime;
    private Coroutine cor;
    private bool canNextSeen = true;

    public float yPos;
    public float rnd;

    public GameObject Obje;
    public GameObject Obje2;


    public GameObject[] exclamation;
    public GameObject player;

    [Space(10)]
    [Header("Death Slider Manager")]
    public DeadMng deadMng;
    void Start()
    {
        currentTime = maxTime;
        deadMng.bossMaxHealt = maxTime;
       
        exclamation[1].SetActive(false);
        cor = StartCoroutine(go());
    }
    private void Update()
    {
        currentTime -= Time.deltaTime;
        deadMng.bossCurrentHealt = currentTime;
        
        if (currentTime<0&&canNextSeen== true && player.GetComponent<PlayerController>().health > 0)
        {
            StopCoroutine(cor);
            GameObject.Find("NextScaneTrigger").GetComponent<TriggerNextScane>().FadeInAndActivatePanel();
            canNextSeen = false;
        }
    }

    IEnumerator go()
    {

        while (currentTime > 0)
        {

          
            rnd = Random.Range(-1, 10);
            Debug.Log(rnd);

            if (rnd <=5)
            {
                ExclamationMove(rnd);
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ExclamationChange());
                yield return new WaitForSeconds(1f);
                Obje.GetComponent<SwordMng>().canDmg = true;
                Obje.transform.position = new Vector3(30, yPos, 0);
                Obje.transform.DOMoveX(2f, 0.5f);
                Camera.main.GetComponent<Camera>().DOShakePosition(0.1f, 0.2f, fadeOut: true);
                yield return new WaitForSeconds(0.5f);
                Obje.transform.DOMove(new Vector3(30, yPos, 0), 1f);
             
            }
            else
            {
                ExclamationMove(rnd);
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ExclamationChange());
                yield return new WaitForSeconds(1f);
                Obje2.GetComponent<SwordMng>().canDmg = true;
                Obje2.transform.position = new Vector3(-30, yPos, 0);
                Obje2.transform.DOMoveX(-2f, 0.5f);
                Camera.main.GetComponent<Camera>().DOShakePosition(0.1f, 0.2f, fadeOut: true);
                yield return new WaitForSeconds(0.5f);
                Obje2.transform.DOMove(new Vector3(-30, yPos, 0), 1f);
             
            }


        }

        yield return new WaitForSeconds(3);
      
    }


    public void ExclamationMove(float random)
    {
        Vector3 exclamationPosition = exclamation[0].transform.position;
        exclamationPosition.y = player.transform.position.y;
        exclamation[0].transform.position = exclamationPosition;

        if (random <=5 )
        {
            exclamation[1].SetActive(false);
            exclamationPosition.x = 13.6f;
          
            exclamation[0].transform.position= exclamationPosition;
            
        }
        else
        {
            exclamation[1].SetActive(false);
            exclamationPosition.x = -13.6f;
          
            exclamation[0].transform.position = exclamationPosition;
        } 
    }
    IEnumerator ExclamationChange()
    {
        Vector3 latestPos = exclamation[0].transform.position;
        yield return new WaitForSeconds(0.5f);
        exclamation[1].transform.position = latestPos;
        exclamation[1].SetActive(true);
        yPos = exclamation[1].transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().getDamage(3);
        }
    }

}
