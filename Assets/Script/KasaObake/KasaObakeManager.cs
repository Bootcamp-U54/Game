using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KasaObakeManager : MonoBehaviour
{
   
    [Header("Kasa Obake")]
    public Transform[] pos;
    public GameObject player;
    public int healt;
    public bool canGetDamage = true;
    public Animator anim;
    public ParticleSystem changeEffect;

    [Header("Tren Spawn")]
    public int maxTrain;
    public Transform[] trainSpawnPos;
    public GameObject train;
    public float trainDuration;
    public Transform trains;
    public ParticleSystem[] trainSpawnPartical;

    [Header("Foto Spawn")]
    public Transform photoSpawnPos;
    public GameObject photo;
    public float photoDuration;
    public Camera renderCam;
    public Transform photos;

    [Header("Fan Spawn")]
    public float fireTime;
    public float currentTime;
    public bool canAttack;
    public GameObject fan;
    public float fanForce;
    public float fanHealtDuration;
    public Transform fans;

    [Header("Needle Spawn")]
    public GameObject needle;
    public Transform[] needlePos;
    public float needleSpeed;
    public float needleRotateSpeed;
    public int maxNeedle;

    public bool canSpawnNeedle;
    public float needleSpawnDuration;
    public float currentNeedleSpawnDuration;

    public Transform needles;

    [Header("Patrolling System")]
    public Transform[] patrollingTrans;
    private Coroutine patrolCoroutine;
    private Tween moveTween;

    [Header("Smoke Spawn")]
    public ParticleSystem smokeCoffeSystem;

    [Space(10)]
    [Header("Death Slider Manager")]
    public DeadMng deadMng;

    [Space(10)]
    [Header("Death Slider Manager")]
    public AudioSource TransformationSfx;
    public AudioSource CameraSfx;


    void Start()
    {
        changeEffect.Stop(); //Deðiþim efektini baþta oynamasýn diye durduruyor
        smokeCoffeSystem.Stop();
        anim = GetComponent<Animator>();
        deadMng.bossMaxHealt = healt;
        deadMng.bossCurrentHealt = healt;
        StartCoroutine(manager());//Bossun manager kodunu çalýþtýrýyor
        for (int i = 0; i < trainSpawnPartical.Length; i++) //Tren spawn effektlerini baþta durduruyor.
        {
            trainSpawnPartical[i].Stop();
        }
        
    }
    void Update()
    {
        if(canAttack) // Yelpaze fýrlatýyor
        {
            if(currentTime>fireTime)
            {
                fire();
                currentTime = 0;
            }
            currentTime += Time.deltaTime;
        }

       
    }
    public void fire()
    {
        GameObject a = Instantiate(fan, transform.position, Quaternion.identity);
        a.GetComponent<fanMng>().player = player;
        a.GetComponent<fanMng>().force = fanForce;
        a.GetComponent<fanMng>().healtDuration = fanHealtDuration;
        a.transform.SetParent(fans);
    }
    public void spawnTrain()
    {
        if (trains.transform.childCount < maxTrain)
        {
            int b = Random.Range(0, trainSpawnPos.Length);
            GameObject a = Instantiate(train, trainSpawnPos[b].position, Quaternion.identity);
            trainSpawnPartical[b].Play();
            a.transform.SetParent(trains);
        }
    }

    public Sprite screenshot()
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

        Camera mainCamera = renderCam; // Kameranýn referansýný alýn
        mainCamera.targetTexture = renderTexture; // RenderTexture'ý kamera hedefi olarak ayarlayýn

        mainCamera.Render(); // Kamerayý render et
        RenderTexture.active = renderTexture; // RenderTexture'ý etkinleþtir
        Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false); // Boþ bir Texture2D oluþtur
        screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0); // RenderTexture'dan pikselleri oku
        screenshot.Apply(); // Texture2D'yi güncelle

        Sprite screenshotSprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f));

        return screenshotSprite;
    }

    public void cleanObj()
    {
        for (int i = 0; i < trains.transform.childCount; i++)//aktif kalan trenleri öldürür
        {
            trains.transform.GetChild(i).gameObject.GetComponent<CarMng>().anim.SetTrigger("Dest");
        }

        for (int i = 0; i < photos.childCount; i++)
        {
            Destroy(photos.GetChild(i).gameObject);
        }
        for (int i = 0; i < fans.childCount; i++)
        {
            Destroy(fans.GetChild(i).gameObject);
        }
        for (int i = 0; i < needles.childCount; i++)
        {
            Destroy(needles.GetChild(i).gameObject);
        }

    }
    IEnumerator patrollingSystem()
    {
        while(true)
        {
            float a = Random.Range(patrollingTrans[0].position.x, patrollingTrans[1].position.x);
            float duration = Mathf.Abs(this.gameObject.transform.position.x - a)*0.2f;
            moveTween = this.gameObject.transform.DOMoveX(a, duration).SetEase(Ease.Linear);
            yield return new WaitForSeconds(duration);

        }
    }

    IEnumerator manager()
    {

        yield return new WaitForSeconds(3f);

        #region Aþama  1
        anim.SetBool("isOpen", true);

        transform.DOMove(pos[1].position, 1f);
        yield return new WaitForSeconds(1f);
        transform.DOMove(pos[2].position, 1f);
        yield return new WaitForSeconds(1f);

        patrolCoroutine = StartCoroutine(patrollingSystem());

        while(healt > 30) //Tren atma kýsmý
        {
            spawnTrain();
            yield return new WaitForSeconds(trainDuration);
        }
        #endregion

        cleanObj();
        StopCoroutine(patrolCoroutine);
        moveTween.Kill();

        transform.DOMove(pos[1].position, 1f);
        yield return new WaitForSeconds(1f);

        anim.SetBool("isOpen", false);

        transform.DOMove(pos[0].position, 1f);
        yield return new WaitForSeconds(1f);

        #region Aþama  2

        changeEffect.Play();
        anim.SetBool("isCamera", true);
        TransformationSfx.Play();

        while (healt>20) //Fotoðraf makinesi çalýþýyor
        {
            GameObject a = Instantiate(photo, photoSpawnPos.position, Quaternion.identity);
            a.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = screenshot();
            a.transform.SetParent(photos);
            CameraSfx.Play();
            yield return new WaitForSeconds(photoDuration);
        }
        changeEffect.Play();
        anim.SetBool("isCamera", false);
        TransformationSfx.Play();
        #endregion

        anim.SetBool("isOpen", true);
        transform.DOMove(pos[1].position, 1f);
        yield return new WaitForSeconds(1f);
        transform.DOMove(pos[2].position, 1f);
        yield return new WaitForSeconds(1f);

        #region Aþama 3

        patrolCoroutine = StartCoroutine(patrollingSystem());

        while (healt > 10) //Tren atma kýsmý
        {
            spawnTrain();
            canAttack = true;
            yield return new WaitForSeconds(trainDuration);
        }
        #endregion


        cleanObj();
        canAttack = false;
        StopCoroutine(patrolCoroutine);
        moveTween.Kill();

        #region Aþama 4 
        changeEffect.Play();
        anim.SetBool("isKettle", true);
        TransformationSfx.Play();

        yield return new WaitForSeconds(1);
        patrolCoroutine = StartCoroutine(patrollingSystem());

        while (healt>0)
        {

            if(maxNeedle > needles.transform.childCount)
            {
                GameObject a = Instantiate(needle, new Vector3(Random.Range(needlePos[0].position.x, needlePos[1].position.x), needlePos[0].position.y, needlePos[0].position.z), Quaternion.identity);

                needleMng m = a.GetComponent<needleMng>();
                m.speed = needleSpeed;
                m.rotateSpeed = needleRotateSpeed;
                m.target = player.transform;
                a.transform.SetParent(needles);
            }
            
       
            yield return new WaitForSeconds(needleSpawnDuration);
        }
        #endregion

        changeEffect.Play();
        anim.SetBool("isKettle", false);
        TransformationSfx.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.gameObject.tag=="Bullet")
        {
            getDamage(collision.gameObject.GetComponent<BulletManager>().damage);
            Destroy(collision.gameObject);
        }
        
    }

    public void getDamage(int dmg)
    {
        if (canGetDamage == true)
        {
            this.gameObject.GetComponent<damageAnim>().startAnim();
            if (healt >= dmg)
            {
                healt -= dmg;
            }
            else
            {
                healt = 0;
            }

            if (healt <= 0)
            {
                deadKasaObake();
            }
            deadMng.bossCurrentHealt = healt;
        }
    }

    public void deadKasaObake()
    {
        GameObject.Find("NextScaneTrigger").GetComponent<TriggerNextScane>().FadeInAndActivatePanel();
    }

    public void smokeEffect()
    {
        smokeCoffeSystem.Play();
    }
}
