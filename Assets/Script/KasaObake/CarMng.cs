using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMng : MonoBehaviour
{
    public int damage;

    [Header("Okunan Degerler")]
    public int currentWallHit;


    [Header("Girilen Degerler")]
    public BoxCollider2D boxCollider2d;
    public Animator anim;
    public float jumpArea;
    public LayerMask platformLayerMask, wallLayerMask;
    public Rigidbody2D rb;
    public bool canMove = true;
    

    [Space(10)]
    [Header("Random Degerler")]
    public float minSpeed;
    public float maxspeed;
    public int wallHitMin, wallHitMax;

    [Space(10)]
    [Header("Belirlenmiş (Girdi Yapma)")]
    public float currentSpeed;
    public int yon;
    public int WallHit;

    [Header("Partical")]
    
    public ParticleSystem smokeEffect;
    private ParticleSystem.EmissionModule smokeEmmision;







    private void Start()
    {
        smokeEmmision = smokeEffect.emission;
        anim = GetComponent<Animator>();
        yon = 1;
        currentSpeed = Random.Range(minSpeed, maxspeed);
        if(Random.Range(0,2)==1)
        {
            Flip();
        }

        WallHit = Random.Range(wallHitMin, wallHitMax+1);
    }

    private void Update()
    {
        if (IsGrounded()&&canMove)
        {
            rb.velocity = new Vector2(yon * currentSpeed, rb.velocity.y);
            smokeEmmision.rateOverTime = 50;

        }
        else
        {
            smokeEmmision.rateOverTime = 0;
        }

        anim.SetBool("isWalk",IsGrounded());

        if (IsWall())
        {   
            if (currentWallHit > WallHit)
            {
                canMove = false;

                anim.SetTrigger("Dest");
            }
            else
            {
                Flip();
            }
        }
    }

    public bool IsGrounded()  //Karakterin yerde olup olmadigini kontrol eder.
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, jumpArea, platformLayerMask);

        return raycastHit2d.collider != null;

    }

    public bool IsWall()  //Karakterin duvara gelip gelmediğini kontrol eder.
    {
        if(yon ==1)
        {
            RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center + new Vector3((boxCollider2d.bounds.size.x / 2 + 0.1f), 0, 0), boxCollider2d.bounds.size, 0f, Vector2.right, jumpArea, wallLayerMask);

            Color rayColor;
            if (raycastHit2d.collider != null)
            {
                rayColor = Color.red;
            }
            else
            {
                rayColor = Color.green;
            }
            Debug.DrawRay(boxCollider2d.bounds.center + new Vector3((boxCollider2d.bounds.size.x / 2 + 0.1f), 0, 0), Vector2.right * (boxCollider2d.bounds.size.y + jumpArea), rayColor);


            return raycastHit2d.collider != null;
        }
        else
        {
            RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center + new Vector3(-(boxCollider2d.bounds.size.x / 2 + 0.1f), 0, 0), boxCollider2d.bounds.size, 0f, Vector2.left, jumpArea, wallLayerMask);

            Color rayColor;
            if (raycastHit2d.collider != null)
            {
                rayColor = Color.red;
            }
            else
            {
                rayColor = Color.green;
            }
            Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(-(boxCollider2d.bounds.size.x / 2 + 0.1f), 0, 0), Vector2.left * (boxCollider2d.bounds.size.y + jumpArea), rayColor);


            return raycastHit2d.collider != null;
        }
        

    }

    private void Flip() // Karakterin gorselinin donmesini saglar
    {
        //faceRight = !faceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        yon *= -1;
        currentWallHit++;
    }

    public void dest()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<PlayerController>().getDamage(damage);
            rb.bodyType = RigidbodyType2D.Static;
            boxCollider2d.isTrigger = true;
            anim.SetTrigger("Dest");
        }
    }
}
