using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float speed = 10f; // Mermi h�z�
    public float lifetime = 0.5f; // Mermi �mr� (saniye)
    protected float timer = 0f; // Mermi ge�en s�re
    protected abstract void MoveBullet();
    private void OnEnable()
    {
        timer = 0f; // Mermi etkinle�tirildi�inde s�reyi s�f�rla
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

  
 
}
