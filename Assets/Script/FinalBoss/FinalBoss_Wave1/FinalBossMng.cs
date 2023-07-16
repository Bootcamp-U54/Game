using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;


public class FinalBossMng : MonoBehaviour
{
    public Light2D globalLight;
    public bool lightIsOn = true;
    public bool patrollingStart = false;

    public Transform player;
    public Light2D playerLight;
    public Animator anim;
    public bool canGetDamage = false;

    [Header("Arrow")]
    public GameObject arrow;
    public float arrowForce;
    public Transform arrows;
    public Sprite arrowSprite;
    
    [Header("Shock Waves")]
    public GameObject shockWave;
    public LayerMask playerLayer;
    public float shockDistance;
    public int shockWaveDamage;
    public float healt;

    public Transform[] pos;
    public float yPos;

    [Header("Portal")]
    public GameObject portal;

    [Space(10)]
    [Header("Death Slider Manager")]
    public DeadMng deadMng;

    [Space(10)]
    [Header("SFX")]
    public AudioSource darkSfx;
    public AudioSource screamSFX;
    void Start()
    {
        deadMng.bossMaxHealt = healt;
        deadMng.bossCurrentHealt = healt;
        anim = GetComponent<Animator>();
        StartCoroutine(manager());

    }


    void Update()
    {
        if(lightIsOn)
        {
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, 1, 5f*Time.deltaTime);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, 0, 5f * Time.deltaTime);

            darkSfx.Stop();
        }
        else
        {
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, 0, 5f * Time.deltaTime);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, 1, 5f * Time.deltaTime);

            if(darkSfx.isPlaying==false)
            {
                darkSfx.Play();
            }
        }
    }

    public float getDuration(float pos,bool isX)
    {
        float duration;
        if (isX==true)
        {
            duration = Mathf.Abs(this.gameObject.transform.position.x - pos) * 0.2f;
        }
        else
        {
            duration = Mathf.Abs(this.gameObject.transform.position.y - pos) * 0.2f;
        }
        Debug.Log(duration);
        return duration;
    }

    public void shockWaveAttack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(shockWave.transform.position, shockDistance, playerLayer);
        foreach (var box in hit)
        {
            box.GetComponent<PlayerController>().getDamage(shockWaveDamage);

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(shockWave.transform.position, shockDistance);
    }

    IEnumerator patrollingSystem(int count)
    {
        patrollingStart = true;
        anim.SetBool("isWalk", true);

        for (int i = 0; i < count; i++)
        {
            float a = Random.Range(pos[0].position.x, pos[1].position.x);
            float duration = getDuration(a, true);
            this.gameObject.transform.DOMoveX(a, duration).SetEase(Ease.Linear);

            if (transform.position.x > a)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            yield return new WaitForSeconds(duration);


            GameObject b = Instantiate(arrow, transform.position, Quaternion.identity);
            b.GetComponent<fanMng>().player = player.gameObject;
            b.GetComponent<fanMng>().force = arrowForce;
            b.GetComponent<fanMng>().healtDuration = 0;
            b.transform.SetParent(arrows);
            b.GetComponent<SpriteRenderer>().sprite = arrowSprite;


        }
        anim.SetBool("isWalk", false);
        patrollingStart = false;

    }

  


    IEnumerator manager()
    {
        yield return new WaitForSeconds(3f);
        while(healt >0)
        {

            transform.DOMoveY(pos[0].position.y, getDuration(pos[0].position.y,false)); //Yukarý çýkar
            yield return new WaitForSeconds(getDuration(pos[0].position.y, false));

            lightIsOn = false; // Iþýklarý kapatýr

            StartCoroutine(patrollingSystem(Random.Range(2,10))); //Sað sol yaparak ateþ eder
            yield return new WaitUntil(() => patrollingStart==false);

            lightIsOn = true;
            transform.DOMoveY(yPos, getDuration(yPos,false)); // Yere iner
            yield return new WaitForSeconds(getDuration(yPos, false));

            anim.SetBool("isRage", true);

            Camera.main.GetComponent<CamController>().target = transform;
            yield return new WaitForSeconds(0.5f);
            Camera.main.GetComponent<Camera>().DOShakePosition(1, 0.3f, fadeOut: true);
            screamSFX.Play();

            //shockWave.transform.DOScale(shockDistance*2, 0.5f);
            Camera.main.GetComponent<CamController>().target = player;
            yield return new WaitForSeconds(0.5f);

            shockWave.SetActive(true);
            shockWave.GetComponent<Animator>().SetTrigger("ShockWave");
            
            yield return new WaitForSeconds(0.15f);
            shockWaveAttack();
            yield return new WaitForSeconds(0.2f);
            anim.SetBool("isRage", false);
            shockWave.SetActive(false);

            canGetDamage = true;
            yield return new WaitForSeconds(4);
            canGetDamage = false;

        }

        lightIsOn = true;
        player.gameObject.GetComponent<PlayerController>().canMove = false;
        Camera.main.GetComponent<CamController>().target = portal.transform; //Kamera portala bakar
        yield return new WaitForSeconds(2f);

        portal.SetActive(true); //Portal görünür
        portal.GetComponent<Animator>().SetTrigger("Open"); //Portal açýlma animasyonu çalýþýr
        yield return new WaitForSeconds(2f);

        Camera.main.GetComponent<CamController>().target = transform;//Kamera bossa bakar


        transform.DOMoveY(portal.transform.position.y, getDuration(portal.transform.position.y, false)).SetEase(Ease.Linear); //Boss portala gider
        yield return new WaitForSeconds(getDuration(portal.transform.position.y, false));

        transform.DOMoveX(portal.transform.position.x, getDuration(portal.transform.position.x, true)).SetEase(Ease.Linear); //Boss portala gider
       
        if (transform.position.x > portal.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        yield return new WaitForSeconds(getDuration(portal.transform.position.x, true));

        GetComponent<SpriteRenderer>().DOFade(0, 1f);
        GetComponent<Animator>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(1);

        Camera.main.GetComponent<CamController>().target = player;//Kamera bplayera bakar
        player.gameObject.GetComponent<PlayerController>().canMove = true;
        portal.GetComponent<PortalMng>().canTp = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
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
                deadBoss();
            }
            deadMng.bossCurrentHealt = healt;
        }
    }

    public void deadBoss()
    {
        
    }
}
