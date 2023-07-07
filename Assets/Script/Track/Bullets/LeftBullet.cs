using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBullet : Bullet
{
    private void Update()
    {
        MoveBullet();
        UpdateBullet();
    }
    protected override void MoveBullet()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }


}
