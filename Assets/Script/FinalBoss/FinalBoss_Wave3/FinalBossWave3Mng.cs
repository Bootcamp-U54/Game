using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FinalBossWave3Mng : MonoBehaviour
{
    public Tween moveTween;
    public Transform[] patrollTransform;
    public Coroutine patrollCorotine;
    public Animator anim;

    public float health;
    public bool canGetDamage = true;



    public bool canAttack;
    public float currentTime;
    public float fireTime;
    public GameObject skull;
    public float skullForce;
    public Transform player;
    public Transform allSkull;
    public Transform[] FirePos;
    [Space(10)]
    [Header("Ghost")]
    public GameObject ghostPrefab;
    public Transform[] ghostSpawnPos;
    public float ghostDuration;
    public Transform allGhost;
    public int maxGhostCount;

    [Space(10)]
    [Header("Platforms")]
    public GameObject[] allPlatform;
    public Transform[] allPlatformStartPos, allPlatformEndPos;
    public ParticleSystem fireEffect_1;
    public ParticleSystem fireEffect_2;
    private ParticleSystem.EmissionModule fireEffect_1Emmision;
    private ParticleSystem.EmissionModule fireEffect_2Emmision;




    public void Start()
    {
        fireEffect_1Emmision = fireEffect_1.emission;
        fireEffect_2Emmision = fireEffect_2.emission;


        
        anim = GetComponent<Animator>();
        StartCoroutine(manager());
        StartCoroutine(ghostSpawner());

        

        for (int i = 0; i < allPlatform.Length; i++)
        {
            allPlatform[i].transform.DOMove(allPlatformStartPos[i].position, 0);
        }

    }
    IEnumerator manager()
    {
        transform.position = patrollTransform[0].position;
        patrollCorotine = StartCoroutine(patrollingSystem());

        
        while (health > 0)
        {
            canAttack = true;
            yield return new WaitForSeconds(2*Random.Range(3,5));
            canAttack = false;

            changePlatform(true);
            yield return new WaitForSeconds(2);
            StopCoroutine(patrollCorotine);
            anim.SetTrigger("Idle");
            anim.SetBool("FirePlatform", true);

           
            yield return new WaitForSeconds(5);
            anim.SetBool("FirePlatform", false);

            fireEffect_1Emmision.rateOverTime = 0; //Ateþi kapatýr
            fireEffect_2Emmision.rateOverTime = 0;
            fireEffect_1.transform.parent.gameObject.GetComponent<FinalBossWave3FireMng>().fireIsOpen = false;

            yield return new WaitForSeconds(1);
            changePlatform(false);
            patrollCorotine = StartCoroutine(patrollingSystem());
        }

     

    }

    public void changePlatform(bool a)
    {
        if(a ==true)
        {
            for (int i = 0; i < allPlatform.Length; i++)
            {
                allPlatform[i].transform.DOMove(allPlatformEndPos[i].position, 2);
            }
        }
        else
        {
            for (int i = 0; i < allPlatform.Length; i++)
            {
                allPlatform[i].transform.DOMove(allPlatformStartPos[i].position, 2);

            }
        }
    }
    public void Update()
    {
        
        if (canAttack)
        {
            if (currentTime > fireTime)
            {
                currentTime = 0;
                anim.SetTrigger("Fire");
            }
            currentTime += Time.deltaTime;
        }
        

    }
   
    IEnumerator ghostSpawner()
    {
        while(true)
        {
            float xPos = Random.Range(ghostSpawnPos[0].position.x, ghostSpawnPos[1].position.x);
            GameObject ghost = Instantiate(ghostPrefab, new Vector3(xPos, ghostSpawnPos[0].position.y, ghostSpawnPos[0].position.z), Quaternion.identity);
            ghost.GetComponent<GhostMng>().target = player;
            ghost.transform.SetParent(allGhost);
            ghost.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            yield return new WaitForSeconds(ghostDuration);
            yield return new WaitUntil(() => maxGhostCount>allGhost.childCount);
        }
    }
    public void Fire()
    {
        GameObject a = null;
        if (GetComponent<SpriteRenderer>().flipX==true)
        {
            a = Instantiate(skull, FirePos[0].position, Quaternion.identity);
        }
        else
        {
           a = Instantiate(skull, FirePos[1].position, Quaternion.identity);
        }
     
        a.GetComponent<SkullManager>().player = player.gameObject;
        a.GetComponent<SkullManager>().force = skullForce;
        a.transform.SetParent(allSkull);
        a.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
        
    }
    IEnumerator patrollingSystem()
    {
        while (true)
        {
            float a = Random.Range(patrollTransform[0].position.x, patrollTransform[1].position.x);
            float duration = Mathf.Abs(this.gameObject.transform.position.x - a) * 0.2f;
            moveTween = this.gameObject.transform.DOMoveX(a, duration).SetEase(Ease.Linear);
            if (transform.position.x > a)
            {
                GetComponent<SpriteRenderer>().flipX = true;

            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;

            }
            yield return new WaitForSeconds(duration);

        }
    }

    public void platformFire()
    {
        fireEffect_1Emmision.rateOverTime = 200;
        fireEffect_2Emmision.rateOverTime = 200;
        fireEffect_1.transform.parent.gameObject.GetComponent<FinalBossWave3FireMng>().fireIsOpen = true;
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
            if (health >= dmg)
            {
                health -= dmg;
            }
            else
            {
                health = 0;
            }

            if (health <= 0)
            {
                deadBoss();
            }
        }
    }

    public void deadBoss()
    {
        Debug.Log("dead");
    }

}
