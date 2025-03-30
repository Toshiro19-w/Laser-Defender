using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping = true;
    WaveConfigSO currentWave;
    
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
    // Phương thức spawn các wave kẻ thù
    IEnumerator SpawnEnemyWaves(){
        do{
            foreach(WaveConfigSO wave in waveConfigs){
                currentWave = wave; // lấy wave hiện tại

                for(int i = 0; i < currentWave.GetEnemyCount(); i++){
                Instantiate(currentWave.GetEnemyPrefab(i), 
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0, 0, 180), transform); 
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime()); // đợi một khoảng thời gian trước khi spawn kẻ thù tiếp theo
                }

                yield return new WaitForSeconds(timeBetweenWaves); // đợi một khoảng thời gian trước khi spawn wave tiếp theo
            }
        }while(isLooping); // lặp lại mãi mãi
    }
}
