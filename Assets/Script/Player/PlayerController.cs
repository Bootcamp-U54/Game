using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Property")]
    public float health;
    public bool canGetDamage;
    BoxCollider2D boxCollider2d;
    Animator animator;
    Rigidbody2D rb;


    [Space(10)]
    [Header("Run")]
    public int speed;
    public bool canMove = true;
    public float moveInput;
    public bool faceRight = true;

    [Space(10)]
    [Header("Jump")]
    public int jumpspeed;
    public float jumpArea;
    [SerializeField] private LayerMask platformLayerMask;
    public bool canJump = true;
    [Space(10)]
    public float hangTime = 0.2f; //Platformdan dusse bile ufak bir ziplama suresi veriyor
    private float hangCounter;
    [Space(10)]
    public float jumpBufferLenght = 0.1f; //Yere inince oyuncu hemen ziplamak isterse ziplamasini kolaylasitiriyor.
    private float jumpBufferCount;

    [Space(10)]
    [Header("Cam Manage")]
    public Transform camTarget;
    public float aheadAmount, aheadSpeed;

    [Space(10)]
    [Header("Dash")]
    public float DashPower = 10;
    public bool isdashing;
    bool candash = true;
    public DashPool dashPool;
    public float dashCoolDown = 1;

    float normalgravity;
    IEnumerator dashCoroutine;

    [Space(10)]
    [Header("SFX")]
    public AudioSource stepSound;


    [Space(10)]
    [Header("SoarDown")]
    public float normalGravity;
    public float soarGravity;
    public bool isSoar;
    public bool canSoar;
    public ParticleSystem soarEffect_1;
    public ParticleSystem soarEffect_2;
    
    private ParticleSystem.EmissionModule soarEmmision_1;
    private ParticleSystem.EmissionModule soarEmmision_2;

    [Space(10)]
    [Header("Bend System")]
    public bool isBend;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        normalgravity = rb.gravityScale;

        soarEmmision_1 = soarEffect_1.emission;
        soarEmmision_1.rateOverTime = 0f;

        soarEmmision_2 = soarEffect_2.emission;
        soarEmmision_2.rateOverTime = 0f;

    }

    private void FixedUpdate()
    {
        if(canMove==true && isBend==false)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        if (moveInput > 0 || moveInput < 0)
        {
            animator.SetBool("isRunning", true);

            #region Ayak Sesi
            /*
            if (stepSound.isPlaying == false)
            {
                stepSound.Play();
                stepSound.pitch = Random.Range(1.3f, 1.5f);
            }
            */
            #endregion
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


        if (faceRight == true && moveInput < 0)
        {
            Flip();
        }
        else if (faceRight == false && moveInput > 0)
        {
            Flip();
        }

        if (isdashing)
        {
            float direction;
            if (faceRight)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            rb.AddForce(new Vector2(direction * DashPower, 0), ForceMode2D.Impulse);
        }
    
    }

    public void Update()
    {


        if(canMove==true)
        {
            moveInput = Input.GetAxis("Horizontal");
        }
        else
        {
            moveInput = 0;
        }

        animator.SetBool("isJump", !IsGrounded());
        animator.SetFloat("yVelocity", rb.velocity.y);

        #region coyota time 
        if (IsGrounded()) // Oyuncunun platformdan d��t�kden sonra da bir s�re z�plamas�na izin verir.
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
        #endregion

        #region JumpBuffer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCount = jumpBufferLenght;

        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }
        #endregion

        #region JumpSystem
        if (jumpBufferCount >= 0 && hangCounter > 0f && isBend==false &&canMove==true)
        {
            Jump();
            jumpBufferCount = 0;
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        #endregion

        #region Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && candash == true)
        {

            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dashing(.1f, dashCoolDown);
            StartCoroutine(dashCoroutine);

        }
        #endregion

        #region SoarDown
        if (Input.GetKeyDown(KeyCode.Space) && hangCounter<0 &&canSoar==true)
        {
            isSoar = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isSoar = false;
        }

        if(isSoar==false)
        {
            rb.gravityScale = normalgravity;

            soarEmmision_1.rateOverTime = 0f;
            soarEmmision_2.rateOverTime = 0f;

        }
        else
        {
            if(rb.velocity.y < 0)
            {
                rb.gravityScale = soarGravity;

                soarEmmision_1.rateOverTime = 35f;
                soarEmmision_2.rateOverTime = 35f;

            }

            if(IsGrounded())
            {
                isSoar = false;
            }
        }
        #endregion
         
        #region BendSystem

        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
           
            if (hangCounter > 0)
            {
                isBend = true;
                rb.velocity = Vector2.zero;
                animator.SetBool("isBend", isBend);
            }
           
        }

        if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            isBend = false;
            animator.SetBool("isBend", isBend);
        }

        #endregion

    }


    public bool IsGrounded()  //Karakterin yerde olup olmadigini kontrol eder.
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, jumpArea, platformLayerMask);

        return raycastHit2d.collider != null;

    }

    private void Jump() // Ziplama kodu
    {
        canJump = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
    }


    private void Flip() // Karakterin gorselinin donmesini saglar
    {
        faceRight = !faceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
  
    IEnumerator Dashing(float dashDuration, float dashcooldown)
    {

        isdashing = true;
        candash = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        dashPool.getDashImage();
        yield return new WaitForSeconds(dashDuration);
        isdashing = false;
        rb.gravityScale = normalgravity;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashcooldown);
        candash = true;
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
                animator.SetTrigger("Dead");
                canMove = false;
                candash = false;
                canSoar = false;
                GetComponent<PlayerAttackController>().canAttack = false;
                rb.bodyType = RigidbodyType2D.Static;
                boxCollider2d.enabled = false;
            }
        }
    }


}