using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    [SerializeField] List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake()
    {
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        waveConfig = enemySpawner.GetCurrentWave();
    }
    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave(); 
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position; // đặt vị trí của đối tượng tại waypoint đầu tiên trong đường đi
    }

    void Update()
    {
        FollowPath();
    }

    // Phương thức đi theo đường đi, lấy vị trí của waypoint tiếp theo và di chuyển đến đó
    void FollowPath(){
        if(waypointIndex < waypoints.Count){
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime; 
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(transform.position == targetPosition) waypointIndex++; // nếu đã đến waypoint hiện tại thì tăng chỉ số lên 1 để đến waypoint tiếp theo
        } else {
            Destroy(gameObject); // nếu đã đến waypoint cuối cùng thì hủy đối tượng
        }
    }
}
