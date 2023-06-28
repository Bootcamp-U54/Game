using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{

    [Header("Girilen Veriler")]
    public bool canAttack = true;
    public float duration;
    public Transform[] AllspawnPos;
    public GameObject bullet;
    public Animator anim;

    [Space(10)]
    [Header("Okunan Veriler")]
    public Vector2 vec;
    public float currentDuration;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {

        vec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        anim.SetFloat("xAxis", vec.x);
        anim.SetFloat("yAxis", vec.y);



        if (Input.GetKey(KeyCode.X))
        {
            if(currentDuration>duration)
            {
                currentDuration = 0;
                anim.SetTrigger("Fire");
                Debug.Log("ss");
            }
        }
        currentDuration += Time.deltaTime;
    }

    public void fireAnim()
    {

        if ((vec.x == 1 && vec.y == 0) || (vec.x == -1 && vec.y == 0) || (vec.x == 0 && vec.y == 0))
        {
            fire(AllspawnPos[0]);
        }
        else if ((vec.x == 1 && vec.y == 1) || (vec.x == -1 && vec.y == 1))
        {
            fire(AllspawnPos[1]);
        }
        else if (vec.x == 0 && vec.y == 1)
        {
            fire(AllspawnPos[2]);
        }
    }

    public void fire(Transform spawnPos)
    {
        

        GameObject a = Instantiate(bullet, spawnPos.position, Quaternion.identity);

        if (vec!=Vector2.zero)
        {
            a.GetComponent<BulletManager>().yon = vec;
        }
        else
        {
            if(GetComponent<PlayerController>().faceRight==true)
            {
                a.GetComponent<BulletManager>().yon = new Vector2(1, 0);
            }
            else
            {
                a.GetComponent<BulletManager>().yon = new Vector2(-1, 0);
            }
        }
       
    }
}
