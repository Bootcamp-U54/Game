using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Range(-1f,4f)]
    public float ScrollSpeed=0.5f;
    private float Offset;
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

 
    void Update()
    {
        Offset += (Time.deltaTime * ScrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(Offset, 0));
    }
}
