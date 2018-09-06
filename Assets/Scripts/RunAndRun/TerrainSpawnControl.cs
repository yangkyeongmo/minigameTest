using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawnControl : MonoBehaviour {

    public GameObject terrainParent;
    public GameObject terrain;
    public Vector3 terrainSpawnPoint;
    public float heightDelta;

    private float spawnedTerrainHeight = -5.0f;
    private float previousSpawnedTerrainHeight = -5.0f;
    private int frames = 0;
    private int spawningFrame = 0;
    private GameObject prevSpawnedObject;

    private void Start(){
        spawningFrame = (int)(1 * -0.08f / TerrainControl.moveSpeed / Time.fixedDeltaTime);
    }

    private void Update(){
        if(!GameControl.isGameEnd && GameControl.isGameStart){
            SpawnTerrainWithTimeInterval();
        }
    }

    private void SpawnTerrainWithTimeInterval()
    {
        if(frames != spawningFrame)
        {
            frames++;
        }
        else
        {
            frames = 0;
            SpawnTerrain();
        }
    }

    private void SpawnTerrain()
    {
        spawnedTerrainHeight = Random.Range(0.0f, 1.0f) < 0.5f ? previousSpawnedTerrainHeight + 0.8f * heightDelta : previousSpawnedTerrainHeight - 0.8f * heightDelta;
        previousSpawnedTerrainHeight = spawnedTerrainHeight;
        GameObject spawnedObject = Instantiate(terrain, new Vector3(terrainSpawnPoint.x, spawnedTerrainHeight, 0), Quaternion.identity);
        spawnedObject.transform.parent = terrainParent.transform;
        if (prevSpawnedObject == null)
        {
            prevSpawnedObject = spawnedObject;       
        }
        else
        {
            if(FindChildWithTag(prevSpawnedObject.transform, "Enemy") == null)
            {
                spawnedObject.GetComponent<TerrainControl>().spawnObstacle = true;
            }
            else
            {
                spawnedObject.GetComponent<TerrainControl>().spawnObstacle = false;
            }
            prevSpawnedObject = spawnedObject;
        }
    }

    private Transform FindChildWithTag(Transform parent, string tag)
    {
        if(parent != null && (tag != null || tag != "")){
            Transform[] children = parent.GetComponentsInChildren<Transform>();
            foreach(Transform child in children)
            {
                if (child.CompareTag(tag))
                {
                    return child;
                }
            }
        }
        return null;
    }
}