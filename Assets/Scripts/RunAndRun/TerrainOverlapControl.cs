using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainOverlapControl : MonoBehaviour {
    private TerrainSpawnControl tsc;
    private List<GameObject> spawnedList;
    private bool isTerrainMoveAvailable = true;
    private bool isTerrainOverlapped = false;
    private float overlapDistance = 0.0f;

    private void Awake(){
        spawnedList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            spawnedList.Add(child.gameObject);
        }
    }

    private void Start(){
        tsc = GameObject.Find("GameControl").GetComponent<TerrainSpawnControl>();
    }

    private void Update(){
        if(!GameControl.isGameEnd){
            if(isTerrainOverlapped){
                MoveTerrainOnOverlap();
            }
        }
    }

    private void MoveTerrainOnOverlap(){
        foreach(GameObject terrain in spawnedList)
        {
            terrain.transform.position += new Vector3(overlapDistance, 0.0f, 0.0f);
        }
        tsc.terrainSpawnPoint += new Vector3(overlapDistance, 0.0f, 0.0f);
        overlapDistance = 0.0f;
        isTerrainOverlapped = false;
    }

    public bool GetIsTerrainMoveAvailable()
    {
        return isTerrainMoveAvailable;
    }
    public void SetIsTerrainMoveAvailable(bool b)
    {
        isTerrainMoveAvailable = b;
    }
    public bool GetIsTerrainOverlapped()
    {
        return isTerrainOverlapped;
    }
    public void SetIsTerrainOverlapped(bool b)
    {
        isTerrainOverlapped = b;
    }
    public float GetOverlapDistance()
    {
        return overlapDistance;
    }
    public void SetOverlapDistance(float f)
    {
        overlapDistance = f;
    }
    public void AddElementOnSpawnedList(GameObject item)
    {
        spawnedList.Add(item);
    }
    public void DeleteElementOnSpawnedList(GameObject item)
    {
        spawnedList.Remove(item);
    }
}