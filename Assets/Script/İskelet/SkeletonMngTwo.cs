using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SkeletonMngTwo : MonoBehaviour
{
    [Space(10)]
    [Header("Skeleton Value")]
    public float healt;
    public GameObject player;
    public int ghostCount;

    [Space(10)]
    [Header("Skeleton")]
    public int skeletonPosRandom;
    public GameObject[] skeletonPart;
    public Transform[] skeletonPos;

    [Space(10)]
    public GameObject sword;
    public GameObject arm;

    [Space(10)]
    [Header("Blood Effect")]
    public ParticleSystem bloodEffect;
    private ParticleSystem.EmissionModule bloodEmmision;

    [Space(10)]
    [Header("Ghost")]
    public Transform ghostParent;
    public GameObject ghost;
    public Transform[] ghostSpawnPos;
    


    void Start()
    {
        bloodEmmision = bloodEffect.emission;
        
        StartCoroutine(go());
    }

    IEnumerator go()
    {
        #region Aþama 1
        while (healt > 50)
        {
            changeFade(true);

            int a = Random.Range(0, skeletonPos.Length);
            while (a == skeletonPosRandom)
            {
                a = Random.Range(0, skeletonPos.Length);
            }
            skeletonPosRandom = a;

            setSkeletonScale();
            setSkeletonPos();
            yield return new WaitForSeconds(1f);
            sword.GetComponent<Animator>().SetTrigger("Attack");
            yield return new WaitForSeconds(3f);
            changeFade(false);
            yield return new WaitForSeconds(1f);
        }
        #endregion

        sword.SetActive(false);
        arm.SetActive(false);

        gameObject.transform.position = skeletonPos[1].position;
        skeletonPosRandom = 1;
        setSkeletonScale();
        yield return new WaitForSeconds(1f);
        changeFade(true);

        #region Aþama 2
        GetComponent<Animator>().SetBool("SummonGhost", true);
        yield return new WaitForSeconds(0.5f);
        Camera.main.GetComponent<Camera>().DOShakePosition(1, 0.3f, fadeOut: true);
        for (int i = 0; i < ghostCount; i++)
        {
            GameObject a = Instantiate(ghost, new Vector3(Random.Range(ghostSpawnPos[0].position.x, ghostSpawnPos[1].position.x), ghostSpawnPos[0].position.y, ghostSpawnPos[0].position.z), Quaternion.identity);
            a.GetComponent<hayaletMng>().target = player.transform;
            a.transform.SetParent(ghostParent);
            yield return new WaitForSeconds(0.5f);
        }


        yield return new WaitUntil(() => ghostParent.childCount <= 0);
        GetComponent<Animator>().SetBool("SummonGhost", false);

        #endregion

        yield return new WaitForSeconds(2f);

        sword.SetActive(true);
        arm.SetActive(true);

        #region Aþama 3
        while (healt > 0)
        {
            changeFade(true);

            int a = Random.Range(0, skeletonPos.Length);
            while (a == skeletonPosRandom)
            {
                a = Random.Range(0, skeletonPos.Length);
            }
            skeletonPosRandom = a;

            setSkeletonScale();
            setSkeletonPos();
            yield return new WaitForSeconds(1f);
            sword.GetComponent<Animator>().SetTrigger("Attack");
            yield return new WaitForSeconds(3f);
            changeFade(false);
            yield return new WaitForSeconds(1f);
        }
        #endregion

    }

    public float calculateDuration()//Ýskeletin gideceði mesafe için süre hesaplamasý yapýyor
    {
        float duration=0;
        float durationX = Mathf.Abs(this.gameObject.transform.position.x - skeletonPos[skeletonPosRandom].position.x) * 0.1f;
        float durationY = Mathf.Abs(this.gameObject.transform.position.y - skeletonPos[skeletonPosRandom].position.y) * 0.1f;
        if(durationX>durationY)
        {
            duration = durationX;
        }
        else
        {
            duration = durationY;
        }
        return duration;

    }

    public void changeFade(bool isOn) //Ýskeletin görsel olarak kapanýp açýlmasýný saðlýyor
    {
        if(isOn)
        {
            for (int i = 0; i < skeletonPart.Length; i++)
            {
                skeletonPart[i].transform.GetComponent<SpriteRenderer>().DOFade(1, 1f).From(0);
            }
            bloodEmmision.rateOverTime = 10f;
        }
        else
        {
            for (int i = 0; i < skeletonPart.Length; i++)
            {
                skeletonPart[i].transform.GetComponent<SpriteRenderer>().DOFade(0, 1f).From(1);
            }
            bloodEmmision.rateOverTime = 0f;
        }
        
    }
    public void setSkeletonPos()//Ýskeletin pozisyonunu belirliyor
    {
        gameObject.transform.position = skeletonPos[skeletonPosRandom].position;
    }

    public void setSkeletonScale() //Ýskeletin yönünü belirliyor
    {
        if (skeletonPosRandom == 0 || skeletonPosRandom == 1)
        {
            Vector3 scaler = transform.localScale;
            scaler.x = 1f;
            transform.localScale = scaler;
        }
        else
        {
            Vector3 scaler = transform.localScale;
            scaler.x = -1f;
            transform.localScale = scaler;
        }
    }
}
