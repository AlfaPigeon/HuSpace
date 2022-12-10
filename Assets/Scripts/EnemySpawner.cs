using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnerSettingsSO _spawnerSettings;

    private float counter;
    private int spawnedCount;
    
    //Sahnedeki düşmanların listesi gerekirse diye tuttum. Sistemin çalışması için gerekli değil.
    public List<Enemy> enemyList = new List<Enemy>();

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= _spawnerSettings.waveTime)
        {
            SpawnWave();
            counter = 0;
            spawnedCount = 0;
        }
    }

    private void SpawnWave()
    {
        for (int i = 0; i < _spawnerSettings.spawnCountPerWave; i++)
        {
            if (_spawnerSettings.randomEnemySpawn)
                SpawnEnemy(Random.Range(0, _spawnerSettings.enemyArray.Length));
            else
                SpawnEnemy(_spawnerSettings.spawningEnemyIndex);
        }
    }

    private Vector3 CalculatePosition()
    {
        int angle;
        
        if (_spawnerSettings.randomPositionSpawn)
        {
            angle = Random.Range(0, 360);
            float x = Mathf.Cos(angle) * _spawnerSettings.circleRadius;
            float z = Mathf.Sin(angle) * _spawnerSettings.circleRadius;

            return new Vector3(x, _spawnerSettings.yOffset, z);
        }
        else
        {
            angle = _spawnerSettings.spawnFromAngle + _spawnerSettings.spaceBetween * spawnedCount;
            float x = Mathf.Cos(Mathf.Repeat(angle, 360)) * _spawnerSettings.circleRadius;
            float z = Mathf.Sin(Mathf.Repeat(angle, 360)) * _spawnerSettings.circleRadius;
            
            spawnedCount++;
            
            return new Vector3(x, _spawnerSettings.yOffset, z);
        }
    }

    private void SpawnEnemy(int enemyIndex)
    {
        Enemy spawningEnemy = Instantiate(_spawnerSettings.enemyArray[enemyIndex], CalculatePosition(),
            Quaternion.identity, transform);
        
        enemyList.Add(spawningEnemy);
    }
}