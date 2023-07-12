using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CatMng : MonoBehaviour
{
    [Header("Cat Value")]
    public float healt;
    bool isRight = true;
    public Transform player;
    public bool canGetDamage = true;
    public float speed = 1;
    public DeadMng deadMng;
    [Space(10)]
    [Header("Cat Object")]
    public GameObject[] arms;
    public Transform[] armsPos;

    [Space(10)]
    [Header("Cat Head Anim")]
    public Transform catRightEye, catLeftEye;
    public Sprite[] catEye;
    public GameObject catHead;

    [Space(10)]
    [Header("Partical Effect")]

    public ParticleSystem rightHandEffect;
    public ParticleSystem leftHandEffect;
    public ParticleSystem[] obstaclePartical;

    private ParticleSystem.EmissionModule rightHandEffectEmmision;
    private ParticleSystem.EmissionModule leftHandEffectEmmision;

    [Space(10)]
    [Header("Obstacle")]
    public Transform allObstacle;
    public Transform deletedObstacle;

    public GameObject obstacle;
    public Transform[] obstacleSpawnPos;
    public float spawnDuration;

    public Vector3 obstaceDestroyPos;
    public int obstacleDamage;

    public Sprite[] obstacleSprite;

    public string achievementName;

    public AudioSource pawSfx;



    void Start()
    {
        for (int i = 0; i < obstaclePartical.Length; i++)
        {
            obstaclePartical[i].Stop();
        }

        rightHandEffectEmmision = rightHandEffect.emission;
        leftHandEffectEmmision = leftHandEffect.emission;

        leftHandEffectEmmision.rateOverTime = 0;
        rightHandEffectEmmision.rateOverTime = 0;

        deadMng.bossMaxHealt = healt;
        deadMng.bossCurrentHealt = healt;
        StartCoroutine(Mng());
    }

   
    void Update()
    {
        lookPlayer();
    }

    public void lookPlayer()
    {

        if((catLeftEye.position.x < player.position.x) && (player.position.x < catRightEye.position.x))
        {
            //Normal bakýþ
            Debug.Log("Normal");
            catHead.GetComponent<SpriteRenderer>().sprite = catEye[1];
            
        }
        else  if ((catLeftEye.position.x < player.position.x) && (player.position.x > catRightEye.position.x))
        {
            //Sað
            Debug.Log("Sað");
            catHead.GetComponent<SpriteRenderer>().sprite = catEye[2];
        }
        else if ((catLeftEye.position.x > player.position.x) && (player.position.x < catRightEye.position.x))
        {
            //Sol
            Debug.Log("Sol");
            catHead.GetComponent<SpriteRenderer>().sprite = catEye[0];
        }
    }
    IEnumerator Mng()
    {
        while(healt>10)
        {
            setDirection();

            if(isRight==true)
            {
                catHead.transform.DOLocalMoveY(4.5f, 1f*speed);
                arms[0].transform.DOMove(armsPos[0].position, 1f * speed);
                yield return new WaitForSeconds(1f * speed);

                arms[0].transform.DOMoveY(-3.7f, 0.2f * speed);
                catHead.transform.DOLocalMoveY(3f, 0.2f * speed);
                arms[0].GetComponent<CatArmMng>().canDmg = true;
                Camera.main.GetComponent<Camera>().DOShakePosition(0.2f * speed, 0.3f, fadeOut: true);
                pawSfx.Play();
                yield return new WaitForSeconds(1f * speed);

                catHead.transform.DOLocalMoveY(3.75f, 1f * speed);

                rightHandEffectEmmision.rateOverTime = 50;
                arms[0].transform.DOMove(new Vector3(30, -3.7f, 0), 1f * speed);
                yield return new WaitForSeconds(1f * speed);
                rightHandEffectEmmision.rateOverTime = 0;
            }
            else
            {
                catHead.transform.DOLocalMoveY(4.5f, 1f * speed);
                arms[1].transform.DOMove(armsPos[1].position, 1f * speed);
                yield return new WaitForSeconds(1f * speed);

                arms[1].transform.DOMoveY(-2.5f, 0.2f * speed);
                catHead.transform.DOLocalMoveY(3f, 0.2f * speed);
                arms[1].GetComponent<CatArmMng>().canDmg = true;
                Camera.main.GetComponent<Camera>().DOShakePosition(0.2f * speed, 0.3f, fadeOut: true);
                pawSfx.Play();
                yield return new WaitForSeconds(1f * speed);

                catHead.transform.DOLocalMoveY(3.75f, 1f * speed);
                leftHandEffectEmmision.rateOverTime = 50;
                arms[1].transform.DOMove(new Vector3(-40, -2.5f, 0), 2f * speed);
                yield return new WaitForSeconds(2f * speed);
                leftHandEffectEmmision.rateOverTime = 0;
            }
            
            yield return new WaitForSeconds(1 * speed);


        }
        StartCoroutine(spawnMng());
        for (int i = 0; i < obstaclePartical.Length; i++)
        {
            obstaclePartical[i].Play();
        }
        while (healt > 0)
        {
            setDirection();

            if (isRight == true)
            {
                catHead.transform.DOLocalMoveY(4.5f, 1f * speed);
                arms[0].transform.DOMove(armsPos[0].position, 1f * speed);
                yield return new WaitForSeconds(1f * speed);

                arms[0].transform.DOMoveY(-3.7f, 0.2f * speed);
                catHead.transform.DOLocalMoveY(3, 0.2f * speed);
                arms[0].GetComponent<CatArmMng>().canDmg = true;

                randomSpawnObj();
        
                yield return new WaitForSeconds(1f * speed);

                catHead.transform.DOLocalMoveY(3.75f, 1f * speed);

               

                rightHandEffectEmmision.rateOverTime = 50;
                arms[0].transform.DOMove(new Vector3(30, -3.7f, 0), 1f * speed);
                yield return new WaitForSeconds(1f * speed);
                rightHandEffectEmmision.rateOverTime = 0;
            }
            else
            {
                catHead.transform.DOLocalMoveY(4.5f, 1f * speed);
                arms[1].transform.DOMove(armsPos[1].position, 1f * speed);
                yield return new WaitForSeconds(1f * speed);

                arms[1].transform.DOMoveY(-2.5f, 0.2f * speed);
                catHead.transform.DOLocalMoveY(3, 0.2f * speed);
                arms[1].GetComponent<CatArmMng>().canDmg = true;
                randomSpawnObj();
               
                yield return new WaitForSeconds(1f * speed);

                catHead.transform.DOLocalMoveY(3.75f, 1f * speed);
                leftHandEffectEmmision.rateOverTime = 50;
                arms[1].transform.DOMove(new Vector3(-40, -2.5f, 0), 2f * speed);
                yield return new WaitForSeconds(2f * speed);
                leftHandEffectEmmision.rateOverTime = 0;
            }

            yield return new WaitForSeconds(1 * speed);


        }
    }

    public void setDirection()
    {
        if(player.transform.position.x>0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
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
                deadCat();
            }
            deadMng.bossCurrentHealt = healt;
        }
    }

    public void deadCat()
    {
        GameObject.Find("NextScaneTrigger").GetComponent<TriggerNextScane>().levelAchievement = achievementName;
        GameObject.Find("NextScaneTrigger").GetComponent<TriggerNextScane>().FadeInAndActivatePanel();
    }

    IEnumerator spawnMng()
    {
        while(true)
        {
            spawnObj();
            Camera.main.GetComponent<Camera>().DOShakePosition(spawnDuration, 0.3f, fadeOut: false);
            yield return new WaitForSeconds(spawnDuration);
        }
    }

    public void randomSpawnObj()
    {
        int random = Random.Range(2, 5);
        for (int i = 0; i < random; i++)
        {
            spawnObj();
        }
    }

    public void spawnObj()
    {
        GameObject spawnedObj = null;

        if(deletedObstacle.childCount>0)
        {
            spawnedObj = deletedObstacle.GetChild(0).gameObject;
        }
        else
        {
            spawnedObj = Instantiate(obstacle, Vector3.zero, Quaternion.identity);
            Debug.LogWarning("Spawnlandý");
        }

        
        float randomX = Random.Range(obstacleSpawnPos[0].position.x, obstacleSpawnPos[1].position.x);
        spawnedObj.transform.position = new Vector3(randomX, obstacleSpawnPos[0].position.y, obstacleSpawnPos[0].position.z);

        spawnedObj.GetComponent<ObstacleMng>().damage = obstacleDamage;
        spawnedObj.GetComponent<ObstacleMng>().destroyPos = obstaceDestroyPos;
        spawnedObj.GetComponent<ObstacleMng>().destroyObstacle = deletedObstacle;

        spawnedObj.GetComponent<SpriteRenderer>().sprite = obstacleSprite[Random.Range(0, obstacleSprite.Length)];

        spawnedObj.transform.Rotate(new Vector3(0, 0, Random.Range(0,360)));

        float scale = Random.Range(0.8f, 1.2f);
        spawnedObj.transform.localScale = new Vector3(scale, scale, scale);

        spawnedObj.transform.SetParent(allObstacle);
        spawnedObj.SetActive(true);
    }
}
