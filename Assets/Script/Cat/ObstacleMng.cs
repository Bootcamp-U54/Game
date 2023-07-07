using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMng : MonoBehaviour
{
    public Transform destroyObstacle;
    public Vector3 destroyPos;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            destroyObj();
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().getDamage(damage);
            destroyObj();
        }

    }

    public void destroyObj()
    {
        this.gameObject.transform.position = destroyPos;
        this.gameObject.SetActive(false);
        this.gameObject.transform.SetParent(destroyObstacle);
    }
}
