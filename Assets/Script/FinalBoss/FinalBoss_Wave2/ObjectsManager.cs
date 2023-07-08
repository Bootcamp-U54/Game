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
    public float health;

    public float yPos;
    public float rnd;

    public GameObject Obje;
    public GameObject Obje2;


    public GameObject[] exclamation;
    public GameObject player;

   
    void Start()
    {
        exclamation[1].SetActive(false);
        StartCoroutine(go());
    }


    IEnumerator go()
    {

        while (health > 0)
        {

          
            rnd = Random.Range(-1, 10);
            Debug.Log(rnd);

            if (rnd <=5)
            {
                ExclamationMove(rnd);
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ExclamationChange());
                yield return new WaitForSeconds(1f);
                Obje.transform.position = new Vector3(13, yPos, 0);
                Obje.transform.DOMoveX(2f, 0.5f);
                Camera.main.GetComponent<Camera>().DOShakePosition(0.1f, 0.2f, fadeOut: true);
                yield return new WaitForSeconds(0.5f);
                Obje.transform.DOMove(new Vector3(13, yPos, 0), 1f);
             
            }
            else
            {
                ExclamationMove(rnd);
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ExclamationChange());
                yield return new WaitForSeconds(1f);
                Obje2.transform.position = new Vector3(-13, yPos, 0);
                Obje2.transform.DOMoveX(-2f, 0.5f);
                Camera.main.GetComponent<Camera>().DOShakePosition(0.1f, 0.2f, fadeOut: true);
                yield return new WaitForSeconds(0.5f);
                Obje2.transform.DOMove(new Vector3(-13, yPos, 0), 1f);
             
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
            exclamationPosition.x = 8.3f;
          
            exclamation[0].transform.position= exclamationPosition;
            
        }
        else
        {
            exclamation[1].SetActive(false);
            exclamationPosition.x = -8.3f;
          
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

}
