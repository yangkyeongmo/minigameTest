using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour {

    public float moveSpeed;
    public float xDestroyBoundary;
    public float probSpawn, probStaticOrReactive;
    public GameObject[] obstacle;
    
	// Use this for initialization
	void Start () {
        InstantiateObstacle();
	}

    private void InstantiateObstacle()
    {
        float randomFactor = Random.Range(0, 10.0f);
        if (randomFactor < probSpawn)
        {
            float randomFactor2 = Random.Range(0, 10.0f);
            GameObject spawnedObstacle;
            if (randomFactor2 < probStaticOrReactive)
            {
                spawnedObstacle = Instantiate(obstacle[0], gameObject.transform.position, Quaternion.identity);
            }
            else
            {
                spawnedObstacle = Instantiate(obstacle[1], gameObject.transform.position, Quaternion.identity);
            }
            spawnedObstacle.transform.parent = gameObject.transform;
            spawnedObstacle.transform.localPosition = new Vector3(0.0f, 4.5f, 0.0f);
            print(spawnedObstacle.name + "is spawned under " + gameObject.name);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameControl.isGameEnd)
        {
            Move();
        }
        if(transform.position.x < xDestroyBoundary)
        {
            Destroy(gameObject);
        }
	}

    private void Move()
    {
        transform.position += new Vector3(1.0f, 0, 0) * moveSpeed;
    }
}
