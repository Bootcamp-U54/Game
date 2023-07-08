using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float speed = 10f; // Mermi h�z�
    public float lifetime = 0.5f; // Mermi �mr� (saniye)
    protected float timer = 0f; // Mermi ge�en s�re

    public int damage = 1;

    public Sprite currenSprite;
    protected abstract void MoveBullet();

    private void Start()
    {
        currenSprite = GetComponent<SpriteRenderer>().sprite;
    }
    private void OnEnable()
    {
        timer = 0f; // Mermi etkinle�tirildi�inde s�reyi s�f�rla
        if(currenSprite!=null)
        {
            GetComponent<SpriteRenderer>().sprite = currenSprite;
        }
        GetComponent<BoxCollider2D>().enabled = true;
    }
 
    protected  void UpdateBullet()
    {
        // Mermi �mr�n� kontrol et
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            gameObject.SetActive(false); // Mermiyi devre d��� b�rak
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.GetComponent<PlayerController>().getDamage(damage);
            GetComponent<SpriteRenderer>().sprite = null;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
