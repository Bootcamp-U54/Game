using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{


    public Transform target;
    public float speed;
    public float rotationSpeed;

    private void Start()
    {
            target = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
        Vector3 targetPosition = target.transform.position;
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (direction.x < 0)
        {
            angle += 180f;
        }

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Mathf.Sign(target.transform.position.x - this.gameObject.transform.position.x) == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        //flip();
    }

    public void flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x = Mathf.Sign(target.transform.position.x - this.gameObject.transform.position.x);
        transform.localScale = scaler;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}


