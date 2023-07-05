using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Header("Girilen Veriler")]
    public float speed;
    public int damage;

    [Space(10)]
    [Header("Okunan Veriler")]
    public Vector2 yon;

    void Start()
    {
        Destroy(this.gameObject, 2f);

        #region fixRotation

        if (yon.x ==1 && yon.y==0)
        {
            //sað
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (yon.x == 1 && yon.y == 1)
        {
            //sað yukarý
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
        }
        else if (yon.x == 0 && yon.y == 1)
        {
            //yukarý
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if (yon.x == -1 && yon.y == 1)
        {
            //yukarý sol
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 145));
        }
        else if (yon.x == -1 && yon.y == 0)
        {
            //sol
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        #endregion

    }

   

    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
    }
}
