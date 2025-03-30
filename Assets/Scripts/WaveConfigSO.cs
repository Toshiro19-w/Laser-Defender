using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Config", menuName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs; 
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariace = 0f; // Biến thể thời gian spawn
    [SerializeField] float minSpawnTime = 0.2f;
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public int GetEnemyCount(){
        return enemyPrefabs.Count;
    }
    // Lấy tất cả các waypoint từ prefab đường đi
    public List<Transform> GetWaypoints(){
        List<Transform> waypoints = new();
        foreach(Transform child in pathPrefab){
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariace, 
                                        timeBetweenEnemySpawns + spawnTimeVariace);
        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue); // Đảm bảo thời gian spawn không nhỏ hơn minSpawnTime
    }
}
