using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float speed = 10f; // Mermi hýzý
    public float lifetime = 0.5f; // Mermi ömrü (saniye)
    protected float timer = 0f; // Mermi geçen süre

    public int damage = 1;
    protected abstract void MoveBullet();
    private void OnEnable()
    {
        timer = 0f; // Mermi etkinleþtirildiðinde süreyi sýfýrla
    }
 
    protected  void UpdateBullet()
    {
        // Mermi ömrünü kontrol et
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            gameObject.SetActive(false); // Mermiyi devre dýþý býrak
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.GetComponent<PlayerController>().getDamage(damage);
            ObjectPool.Instance.ReturnToPool(gameObject);
        }
    }

}
