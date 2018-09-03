using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainControl : MonoBehaviour {

    public static float moveSpeed;

    public float moveFactor;
    public float xDestroyBoundary;
    public float probSpawn, probStaticOrReactive;
    public GameObject[] obstacle;
    public bool spawnObstacle = true;

    private GameObject characterSprite;
    private TerrainOverlapControl toc;
    private Collider2D c2;

    // Use this for initialization
    void Start () {
        moveSpeed = -moveFactor * Time.fixedDeltaTime;
        characterSprite = GameObject.Find("Character").transform.Find("Sprite").gameObject;
        toc = GameObject.Find("GameControl").GetComponent<TerrainOverlapControl>();
        c2 = transform.Find("CollideBox").GetComponent<Collider2D>();
        if(spawnObstacle){
            InstantiateObstacle();
        }
        toc.AddElementOnSpawnedList(gameObject);
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
            if (toc.GetIsTerrainMoveAvailable())
            {
                Move();
            }
        }
        if(transform.position.y + c2.bounds.size.y/2 > characterSprite.transform.position.y - 0.9f && transform.position.y - c2.bounds.size.y/2 < characterSprite.transform.position.y + 0.9f)
        {
            if (transform.position.x - c2.bounds.size.x / 2 < characterSprite.transform.position.x + 0.9f && transform.position.x - c2.bounds.size.x/2 > characterSprite.transform.position.x -0.9f)
            {
                toc.SetIsTerrainOverlapped(true);
                toc.SetOverlapDistance((characterSprite.transform.position.x + 0.9f) - (transform.position.x - c2.bounds.size.x / 2));
            }
        }
        if(transform.position.x < xDestroyBoundary + characterSprite.transform.position.x)
        {
            toc.DeleteElementOnSpawnedList(gameObject);
            Destroy(gameObject);
        }
	}

    private void Move()
    {
        transform.position += new Vector3(1.0f, 0, 0) * moveSpeed;
    }
}
