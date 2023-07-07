using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    public GameObject eaglePrefab;
    public GameObject cloudPrefab;
    public GameObject scorpionPrefab;
    public GameObject trapPrefab;
    public GameObject ghostPrefab;

    public int eagleCount;
    public int cloudCount;
    public int scorpionCount;
    public int trapCount;
    public int ghostCount;

    private List<GameObject> eaglePool;
    private List<GameObject> cloudPool;
    private List<GameObject> scorpionPool;
    private List<GameObject> trapPool;
    private List<GameObject> ghostPool;

    private void Awake()

    {
        Instance = this;
    }

    private void Start()
    {
        eaglePool = CreatePool(eaglePrefab, eagleCount);
        cloudPool = CreatePool(cloudPrefab, cloudCount);
        scorpionPool = CreatePool(scorpionPrefab, scorpionCount);
        trapPool = CreatePool(trapPrefab, trapCount);
        ghostPool = CreatePool(ghostPrefab, ghostCount);
    }

    private List<GameObject> CreatePool(GameObject prefab, int count)
    {
        List<GameObject> pool = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }

        return pool;
    }

    public GameObject GetEagle()
    {
        for (int i = 0; i < eaglePool.Count; i++)
        {
            if (!eaglePool[i].activeInHierarchy)
            {
                eaglePool[i].SetActive(true);
                return eaglePool[i];
            }
        }

        return null; // Eðer havuzdaki tüm býçaklar kullanýlýyorsa, yeni bir býçak oluþturulabilir veya null dönebilirsiniz.
    }

    public GameObject GetCloud()
    {
        for (int i = 0; i < cloudPool.Count; i++)
        {
            if (!cloudPool[i].activeInHierarchy)
            {
                cloudPool[i].SetActive(true);
                return cloudPool[i];
            }
        }

        return null; // Eðer havuzdaki tüm kýlýçlar kullanýlýyorsa, yeni bir kýlýç oluþturulabilir veya null dönebilirsiniz.
    }
      public GameObject GetScorpion()
    {
        for (int i = 0; i < scorpionPool.Count; i++)
        {
            if (!scorpionPool[i].activeInHierarchy)
            {
                scorpionPool[i].SetActive(true);
                return scorpionPool[i];
            }
        }

        return null; // Eðer havuzdaki tüm kýlýçlar kullanýlýyorsa, yeni bir kýlýç oluþturulabilir veya null dönebilirsiniz.
    }
     public GameObject GetTrap()
    {
        for (int i = 0; i < trapPool.Count; i++)
        {
            if (!trapPool[i].activeInHierarchy)
            {
                trapPool[i].SetActive(true);
                return trapPool[i];
            }
        }

        return null; // Eðer havuzdaki tüm kýlýçlar kullanýlýyorsa, yeni bir kýlýç oluþturulabilir veya null dönebilirsiniz.
    }
     public GameObject GetGhost()
    {
        for (int i = 0; i < ghostPool.Count; i++)
        {
            if (!ghostPool[i].activeInHierarchy)
            {
                ghostPool[i].SetActive(true);
                return ghostPool[i];
            }
        }

        return null; // Eðer havuzdaki tüm kýlýçlar kullanýlýyorsa, yeni bir kýlýç oluþturulabilir veya null dönebilirsiniz.
    }

    public void ReturnToPool(GameObject obj)
    {

        obj.SetActive(false);
    }


}