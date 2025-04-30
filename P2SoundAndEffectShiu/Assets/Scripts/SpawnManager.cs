using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private int obstacleIndex;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript =
            GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        obstacleIndex = Random.Range(0, 3);
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }

        repeatRate = Random.Range(1f, 2.5f);
        startDelay = Random.Range(1f, 2.5f);
        CancelInvoke();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }



    // Update is called once per frame
    void Update()
    {
 
    }

   
}
