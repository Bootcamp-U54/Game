using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBullet : Bullet
{
    
        private void Update()
        {

            MoveBullet();
            UpdateBullet();
        }
        protected override void MoveBullet()
        {

            transform.Translate(Vector2.down * speed * Time.deltaTime);

        }
    
}
