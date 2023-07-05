using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageAnim : MonoBehaviour
{
    public Material damageMaterial;
    private Material normalMaterial;
    public Coroutine damageCor;
    public float duration;
    private void Start()
    {
        normalMaterial = this.gameObject.GetComponent<SpriteRenderer>().material;
    }
    public void startAnim()
    {
        if (damageCor != null)
        {
            StopCoroutine(damageCor);
        }
        damageCor = StartCoroutine(damageEffect());
    }
    IEnumerator damageEffect()
    {
        this.gameObject.GetComponent<SpriteRenderer>().material = damageMaterial;
        yield return new WaitForSeconds(duration);
        this.gameObject.GetComponent<SpriteRenderer>().material = normalMaterial;
        damageCor = null;
    }
}
