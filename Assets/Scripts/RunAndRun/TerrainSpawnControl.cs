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
        Instantiate(terrain, new Vector3(terrainSpawnPoint.x, spawnedTerrainHeight, 0), Quaternion.identity).transform.parent = terrainParent.transform;
    }
}