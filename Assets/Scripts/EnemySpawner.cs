using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public WaveSO[] waves;
    [SerializeField] private WaveSO currentWave;
    public int currentWaveIndex = -1;

    private float counter;
    private int spawnedCount;

    public bool spawnEnabled = true;

    public int minSpawnRadius;
    public int maxSpawnRadius;
    public int yOffset;
    public float waveTime;

    //Sahnedeki düşmanların listesi gerekirse diye tuttum. Sistemin çalışması için gerekli değil.
    public List<Enemy> spawnedEnemies = new List<Enemy>();

    private void Start()
    {
        counter = waveTime;
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= waveTime && spawnEnabled)
        {
            StartNextWave();
            counter = 0;
            spawnedCount = 0;
        }
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Length - 1) { currentWaveIndex++; Debug.Log("Wave " + currentWaveIndex + " started"); }

        else
        {
            currentWaveIndex = -1;
            spawnEnabled = false;
            Debug.Log("Waves Finished");
            return;
        }

        currentWave = waves[currentWaveIndex];

        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            if (currentWave.randomEnemySpawn)
                SpawnEnemy(Random.Range(0, currentWave.enemyArray.Length));

            else
                SpawnEnemy(currentWave.spawningEnemyIndex);
        }
    }

    private Vector3 CalculatePosition()
    {
        int angle;
        
        if (currentWave.randomPositionSpawn)
        {
            //Max
            angle = Random.Range(0, 360);
            float randomPosition = Random.Range(minSpawnRadius, maxSpawnRadius);
            float x = Mathf.Cos(angle) * randomPosition;
            float z = Mathf.Sin(angle) * randomPosition;

            return new Vector3(x, yOffset, z);
        }
        else
        {
            angle = currentWave.spawnFromAngle + currentWave.spaceBetween * spawnedCount;
            float randomPosition = Random.Range(minSpawnRadius, maxSpawnRadius);
            float x = Mathf.Cos(Mathf.Repeat(angle, 360)) * randomPosition;
            float z = Mathf.Sin(Mathf.Repeat(angle, 360)) * randomPosition;
            
            spawnedCount++;
            
            return new Vector3(x, yOffset, z);
        }
    }

    private void SpawnEnemy(int enemyIndex)
    {
        Enemy spawningEnemy = Instantiate(currentWave.enemyArray[enemyIndex], CalculatePosition(),
            Quaternion.identity, transform);
        
        spawnedEnemies.Add(spawningEnemy);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minSpawnRadius);
        Gizmos.color = Color.blue   ;
        Gizmos.DrawWireSphere(transform.position, maxSpawnRadius);
        Gizmos.color = Color.white;
    }
}