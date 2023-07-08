using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostSpawner : MonoBehaviour
{

    public Transform[] ghostSpawnPos;
    public Transform ghostParent;

    public int ghostCount;
    public GameObject ghost;
    public GameObject player;

    public Transform target;
    public float speed;
    public GameObject platform;
    public bool isSpawned;
 

    public Transform[] platformMaxPos;
    
    private void Start()
    {
        isSpawned = false;
        StartCoroutine(spawnGhost());
        if (SceneManager.GetActiveScene().name == "Parkur_1")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    IEnumerator spawnGhost()
    {
        isSpawned = true;
        for (int i = 0; i < ghostCount; i++)
        {
            GameObject a = Instantiate(ghost, new Vector3(Random.Range(ghostSpawnPos[0].position.x, ghostSpawnPos[1].position.x), ghostSpawnPos[0].position.y, ghostSpawnPos[0].position.z), Quaternion.identity);
            a.GetComponent<GhostMng>().target = player.transform;
            a.transform.SetParent(ghostParent);
            yield return new WaitForSeconds(2f);
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Parkur_1")
        {
            if (ghostParent.childCount == 0 && isSpawned==true)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        if (SceneManager.GetActiveScene().name=="Parkur_2")
        {
            if (ghostParent.childCount == 0 && isSpawned == true)
            {
               platform.GetComponent<PlatformMove>().enabled = false;
               platform.transform.position = Vector3.MoveTowards(platform.transform.position, target.position, speed * Time.deltaTime);
            }
        }

       

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Parkur_2")
        {
            if (isSpawned == true && ghostParent.childCount != 0)
            {
                player.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, platformMaxPos[0].position.x, platformMaxPos[1].position.x), player.transform.position.y, player.transform.position.z);
            }
        }
        
    }
}
